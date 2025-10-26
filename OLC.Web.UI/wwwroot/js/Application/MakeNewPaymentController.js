function MakeNewPaymentController() {
    var self = this;

    var currentStep = 0;

    var actions = [];

    var dataObjects = [];

    self.CreditCards = [];

    self.BankAccounts = [];

    self.BillingAddress = [];

    self.SelectedCreditCard = {};

    self.SelectedBankAccount = {};

    self.SelectedBillingAddress = {};

    self.CurrentPaymentItem = {};

    self.CurrentPaymentItem.SelectedCreditCard = self.SelectedCreditCard;

    self.CurrentPaymentItem.SelectedBankAccount = self.SelectedBankAccount;

    self.CurrentPaymentItem.SelectedBillingAddress = self.SelectedBillingAddress;

    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');

        console.log(appUserInfo);

        if (appUserInfo) {

            self.ApplicationUser = appUserInfo;
        }

        actions.push("/CreditCard/GetUserCreditCards");

        actions.push("/BankAccount/GetUserBankAccounts");

        actions.push("/BillingAddress/GetUserBillingAddresses");

        dataObjects.push({ userId: self.ApplicationUser.Id });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 0 || index === 1 || index === 2) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;

            self.CreditCards = responses[0][0] && responses[0][0].data ? responses[0][0].data : [];
            self.BankAccounts = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];
            self.BillingAddress = responses[2][0] && responses[2][0].data ? responses[2][0].data : [];

            // Bind the data to the form
            self.bindCreditCards();
            self.bindBankAccounts();
            self.bindBillingAddresses();

            $(".se-pre-con").hide();
        }).fail(function () {
            console.log('One or more requests failed.');
            hideLoader();
        });

        // Step navigation
        const $steps = $('.main');
        const $stepContents = $('.step-number-content');
        const $progressItems = $('.progress-bar li');
        const $stepNumber = $('.step-number');

        // Next button functionality
        $('.next_button').on('click', function () {
            if (self.validateStep(currentStep)) {
                self.goToStep(currentStep + 1, $steps, $stepContents, $progressItems, $stepNumber);
            }
        });

        // Back button functionality
        $('.back_button').on('click', function () {
            self.goToStep(currentStep - 1, $steps, $stepContents, $progressItems, $stepNumber);
        });

        // Submit button functionality
        $('.submit_button').on('click', function () {
            if (self.validateStep(currentStep)) {
                self.processPayment();
                self.goToStep(currentStep + 1, $steps, $stepContents, $progressItems, $stepNumber);
            }
        });

        // Payment option selection
        $(document).on('click', '.payment-option', function () {
            // Remove selected class from all options in the same step
            const $parentStep = $(this).closest('.main');
            $parentStep.find('.payment-option').removeClass('selected');

            // Add selected class to clicked option
            $(this).addClass('selected');

            // Store selected data
            const dataType = $(this).data('type');
            const dataId = $(this).data('id');

            if (dataType === 'credit-card') {
                self.SelectedCreditCard = self.CreditCards.find(card => card.Id === dataId) || {};
                self.updateConfirmationStep();
            } else if (dataType === 'bank-account') {
                self.SelectedBankAccount = self.BankAccounts.find(account => account.Id === dataId) || {};
                self.updateConfirmationStep();
            } else if (dataType === 'billing-address') {
                self.SelectedBillingAddress = self.BillingAddress.find(address => address.Id === dataId) || {};
                self.updateConfirmationStep();
            }
        });
    };

    // Function to bind credit cards as selectable options
    self.bindCreditCards = function () {
        const paymentMethodStep = $('.main').eq(0); // Step 1 container

        // Remove the existing static credit card option with input fields
        $('.payment-option[data-payment="card"]').remove();

        if (self.CreditCards && self.CreditCards.length > 0) {
            console.log('Binding', self.CreditCards.length, 'credit cards');

            // Create selectable credit cards
            const creditCardsHtml = self.CreditCards.map(card => {
                const expiry = `${card.ExpiryMonth}/${card.ExpiryYear.slice(-2)}`;
                const cardIcon = self.getCardIcon(card.CardType);

                return `
                <div class="payment-option selectable-card" data-type="credit-card" data-id="${card.Id}">
                    <div class="d-flex align-items-center mb-2">
                        <i class="${cardIcon} payment-icon"></i>
                        <div>
                            <h5 class="mb-0">${card.CardType} - ${card.IssuingBank}</h5>
                            <small class="text-muted">****${card.LastFourDigits} • ${card.CardHolderName}</small>
                            <br>
                            <small class="text-muted">Expires: ${expiry}</small>
                        </div>
                    </div>
                </div>
            `;
            }).join('');

            // Add "Add New Card" option
            const newCardHtml = `
            <div class="payment-option selectable-card" data-type="credit-card" data-id="new">
                <div class="d-flex align-items-center">
                    <i class="fas fa-plus-circle payment-icon"></i>
                    <div>
                        <h5 class="mb-0">Add New Card</h5>
                        <small class="text-muted">Add a new credit/debit card</small>
                    </div>
                </div>
            </div>
        `;

            // Insert the credit cards after the text description
            paymentMethodStep.find('.text').after(creditCardsHtml + newCardHtml);

            // Select first card by default
            $('.selectable-card[data-type="credit-card"]').first().addClass('selected');
            if (self.CreditCards.length > 0) {
                self.SelectedCreditCard = self.CreditCards[0];
            }

        } else {
            console.log('No credit cards available');
            // Show message if no cards available
            const noCardsHtml = `
            <div class="payment-option" data-type="credit-card" data-id="new">
                <div class="d-flex align-items-center">
                    <i class="fas fa-credit-card payment-icon"></i>
                    <div>
                        <h5 class="mb-0">No Saved Cards</h5>
                        <small class="text-muted">Click to add a new credit/debit card</small>
                    </div>
                </div>
            </div>
        `;
            paymentMethodStep.find('.text').after(noCardsHtml);
        }

        // Handle credit card selection
        $(document).on('click', '.selectable-card[data-type="credit-card"]', function () {
            const cardId = $(this).data('id');

            if (cardId === 'new') {
                // Show new card form (you can implement this modal or form)
                alert('Add new card functionality would go here');
                // You can show a modal or form to add new card
                self.showNewCardForm();
            } else {
                // Select the existing card
                const selectedCard = self.CreditCards.find(card => card.Id == cardId);
                self.SelectedCreditCard = selectedCard || {};
                console.log('Selected credit card:', self.SelectedCreditCard);
                self.updateConfirmationStep();
            }
        });
    };

    // Helper function to get card icon based on card type
    self.getCardIcon = function (cardType) {
        const type = cardType.toLowerCase();
        if (type.includes('visa')) return 'fab fa-cc-visa';
        if (type.includes('master') || type.includes('mastercard')) return 'fab fa-cc-mastercard';
        if (type.includes('american') || type.includes('amex')) return 'fab fa-cc-amex';
        if (type.includes('discover')) return 'fab fa-cc-discover';
        return 'fas fa-credit-card';
    };

    // Function to show new card form (you can implement this as needed)
    self.showNewCardForm = function () {
        // You can implement a modal or show a form to add new card
        // For now, we'll just show an alert
        alert('Add new card form would appear here. You can implement a modal with card input fields.');
    };

    // Function to bind bank accounts as selectable options
    self.bindBankAccounts = function () {
        const bankAccountStep = $('.main').eq(1); // Step 2 container

        // Clear existing static options
        bankAccountStep.find('.payment-option[data-account]').remove();

        if (self.BankAccounts && self.BankAccounts.length > 0) {
            // Create selectable bank accounts
            const accountsHtml = self.BankAccounts.map(account => `
                <div class="payment-option selectable-account" data-type="bank-account" data-id="${account.Id}">
                    <div class="d-flex align-items-center mb-2">
                        <i class="fas fa-university payment-icon"></i>
                        <div>
                            <h5 class="mb-0">${account.BankName} - ${account.AccountType}</h5>
                            <small class="text-muted">Account ending in ${account.LastFourDigits}</small>
                            <br>
                            <small class="text-muted">${account.AccountHolderName}</small>
                        </div>
                    </div>
                    <div class="mt-2">
                        <p class="mb-0">IFSC Code: <strong>${account.IFSCCode}</strong></p>
                        <p class="mb-0">Currency: <strong>${account.Currency}</strong></p>
                    </div>
                </div>
            `).join('');

            bankAccountStep.find('.text').after(accountsHtml);

            // Select first account by default
            $('.selectable-account').first().addClass('selected');
            if (self.BankAccounts.length > 0) {
                self.SelectedBankAccount = self.BankAccounts[0];
            }
        } else {
            // Show message if no accounts available
            const noAccountsHtml = `
                <div class="alert alert-warning">
                    <i class="fas fa-exclamation-triangle me-2"></i>
                    No bank accounts found. Please add a bank account to continue.
                </div>
            `;
            bankAccountStep.find('.text').after(noAccountsHtml);
        }
    };

    // Function to bind billing addresses as selectable options
    self.bindBillingAddresses = function () {
        const billingAddressStep = $('.main').eq(2); // Step 3 container

        // Hide existing input fields
        billingAddressStep.find('.input-text').hide();

        if (self.BillingAddress && self.BillingAddress.length > 0) {
            // Create selectable addresses
            const addressesHtml = self.BillingAddress.map(address => `
                <div class="payment-option selectable-address mb-3" data-type="billing-address" data-id="${address.Id}">
                    <div class="d-flex align-items-start">
                        <i class="fas fa-home payment-icon mt-1"></i>
                        <div>
                            <h5 class="mb-1">${self.getAddressTitle(address)}</h5>
                            <p class="mb-1 text-muted">${address.AddessLineOne} ${address.AddessLineTwo}</p>
                            ${address.AddessLineThress ? `<p class="mb-1 text-muted">${address.AddessLineThress}</p>` : ''}
                            <p class="mb-1 text-muted">${address.Location}, ${self.getStateName(address.StateId)}</p>
                            <p class="mb-0 text-muted">PIN: ${address.PinCode}, ${self.getCountryName(address.CountryId)}</p>
                        </div>
                    </div>
                </div>
            `).join('');

            // Add "Add New Address" option
            const newAddressHtml = `
                <div class="payment-option selectable-address" data-type="billing-address" data-id="new">
                    <div class="d-flex align-items-center">
                        <i class="fas fa-plus-circle payment-icon"></i>
                        <div>
                            <h5 class="mb-0">Add New Address</h5>
                            <small class="text-muted">Add a new billing address</small>
                        </div>
                    </div>
                </div>
            `;

            billingAddressStep.find('.text').after(addressesHtml + newAddressHtml);

            // Handle address selection
            $(document).on('click', '.selectable-address[data-type="billing-address"]', function () {
                const addressId = $(this).data('id');
                const addressInputs = billingAddressStep.find('.input-text');

                if (addressId === 'new') {
                    // Show input fields for new address
                    addressInputs.show();
                    self.clearAddressInputs();
                    self.SelectedBillingAddress = {};
                } else {
                    // Hide inputs and select the address
                    addressInputs.hide();
                    const selectedAddress = self.BillingAddress.find(address => address.Id == addressId);
                    self.SelectedBillingAddress = selectedAddress || {};
                    self.updateConfirmationStep();
                }
            });

            // Select first address by default
            if (self.BillingAddress.length > 0) {
                $('.selectable-address[data-type="billing-address"]').first().trigger('click');
            }
        } else {
            // If no addresses, show the input fields
            billingAddressStep.find('.input-text').show();
        }
    };

    // Helper function to get address title
    self.getAddressTitle = function (address) {
        return "Billing Address"; // You can customize this based on your data
    };

    // Helper function to get state name (you'll need to implement this based on your data)
    self.getStateName = function (stateId) {
        // You might want to create a state mapping or make an API call
        const stateMap = {
            24: "Telangana"
            // Add more state mappings as needed
        };
        return stateMap[stateId] || "State";
    };

    // Helper function to get country name (you'll need to implement this based on your data)
    self.getCountryName = function (countryId) {
        // You might want to create a country mapping or make an API call
        const countryMap = {
            1: "India"
            // Add more country mappings as needed
        };
        return countryMap[countryId] || "Country";
    };

    // Clear card inputs
    self.clearCardInputs = function () {
        $('#card_number').val('');
        $('#card_name').val('');
        $('#expiry_date').val('');
        $('#cvv').val('');
    };

    // Clear address inputs
    self.clearAddressInputs = function () {
        $('#address_line1').val('');
        $('#address_line2').val('');
        $('#city').val('');
        $('#state').val('');
        $('#zip_code').val('');
        $('#country').val('Select Country');
    };

    // Update confirmation step with selected data
    self.updateConfirmationStep = function () {
        const confirmationStep = $('.main').eq(3);

        // Update payment method
        if (self.SelectedCreditCard.Id) {
            const paymentMethodHtml = `
                <h5>Payment Method</h5>
                <div class="d-flex align-items-center">
                    <i class="fab fa-cc-${self.SelectedCreditCard.CardType.toLowerCase().includes('visa') ? 'visa' : 'credit-card'} payment-icon"></i>
                    <div>
                        <p class="mb-0 fw-bold">${self.SelectedCreditCard.CardType} - ${self.SelectedCreditCard.IssuingBank}</p>
                        <p class="mb-0 text-muted">****${self.SelectedCreditCard.LastFourDigits} • ${self.SelectedCreditCard.CardHolderName}</p>
                        <p class="mb-0 text-muted">Exp: ${self.SelectedCreditCard.ExpiryMonth}/${self.SelectedCreditCard.ExpiryYear.slice(-2)}</p>
                    </div>
                </div>
            `;
            confirmationStep.find('.summary-item').eq(0).html(paymentMethodHtml);
        }

        // Update bank account
        if (self.SelectedBankAccount.Id) {
            const bankAccountHtml = `
                <h5>Bank Account</h5>
                <div class="d-flex align-items-center">
                    <i class="fas fa-university payment-icon"></i>
                    <div>
                        <p class="mb-0 fw-bold">${self.SelectedBankAccount.BankName} - ${self.SelectedBankAccount.AccountType}</p>
                        <p class="mb-0 text-muted">Account ending in ${self.SelectedBankAccount.LastFourDigits}</p>
                        <p class="mb-0 text-muted">${self.SelectedBankAccount.AccountHolderName}</p>
                        <p class="mb-0 text-muted">IFSC: ${self.SelectedBankAccount.IFSCCode} • ${self.SelectedBankAccount.Currency}</p>
                    </div>
                </div>
            `;
            confirmationStep.find('.summary-item').eq(1).html(bankAccountHtml);
        }

        // Update billing address
        if (self.SelectedBillingAddress.Id) {
            const billingAddressHtml = `
                <h5>Billing Address</h5>
                <div>
                    <p class="mb-0 fw-bold">${self.getAddressTitle(self.SelectedBillingAddress)}</p>
                    <p class="mb-0 text-muted">${self.SelectedBillingAddress.AddessLineOne} ${self.SelectedBillingAddress.AddessLineTwo}</p>
                    ${self.SelectedBillingAddress.AddessLineThress ? `<p class="mb-0 text-muted">${self.SelectedBillingAddress.AddessLineThress}</p>` : ''}
                    <p class="mb-0 text-muted">${self.SelectedBillingAddress.Location}, ${self.getStateName(self.SelectedBillingAddress.StateId)}</p>
                    <p class="mb-0 text-muted">PIN: ${self.SelectedBillingAddress.PinCode}, ${self.getCountryName(self.SelectedBillingAddress.CountryId)}</p>
                </div>
            `;
            confirmationStep.find('.summary-item').eq(2).html(billingAddressHtml);
        }

        // Update payment amount
        const transferAmount = $('#transfer_amount').val();
        if (transferAmount) {
            const processingFee = 2.50;
            const totalAmount = parseFloat(transferAmount) + processingFee;

            confirmationStep.find('.summary-item').eq(3).find('.fw-bold').eq(0).text('$' + parseFloat(transferAmount).toFixed(2));
            confirmationStep.find('.summary-item').eq(3).find('.text-primary').text('$' + totalAmount.toFixed(2));
        }
    };

    // Process payment
    self.processPayment = function () {
        const transferAmount = $('#transfer_amount').val();
        const processingFee = 2.50;
        const totalAmount = parseFloat(transferAmount) + processingFee;

        // Update success step with actual amounts
        $('.congrats strong').eq(0).text('$' + totalAmount.toFixed(2));

        // Generate random transaction ID
        const transactionId = '#TX-' + Math.floor(1000 + Math.random() * 9000);
        $('.congrats strong').eq(1).text(transactionId);

        // Here you would typically make an API call to process the payment
        console.log('Processing payment with:', {
            creditCard: self.SelectedCreditCard,
            bankAccount: self.SelectedBankAccount,
            billingAddress: self.SelectedBillingAddress,
            amount: transferAmount
        });

        // You can make your payment API call here
        /*
        $.ajax({
            url: '/Payment/ProcessPayment',
            method: 'POST',
            data: {
                CreditCardId: self.SelectedCreditCard.Id,
                BankAccountId: self.SelectedBankAccount.Id,
                BillingAddressId: self.SelectedBillingAddress.Id,
                Amount: transferAmount,
                TotalAmount: totalAmount
            },
            success: function(response) {
                // Handle success
            },
            error: function(error) {
                // Handle error
            }
        });
        */
    };

    self.goToStep = function (stepIndex, $steps, $stepContents, $progressItems, $stepNumber) {
        // Hide current step
        $steps.eq(currentStep).removeClass('active');
        $stepContents.eq(currentStep).removeClass('active').addClass('d-none');
        $progressItems.eq(currentStep).removeClass('active');

        // Show new step
        $steps.eq(stepIndex).addClass('active');
        $stepContents.eq(stepIndex).removeClass('d-none').addClass('active');
        $progressItems.eq(stepIndex).addClass('active');

        // Update step number
        $stepNumber.text(stepIndex + 1);

        currentStep = stepIndex;

        // Update confirmation step when navigating to it
        if (stepIndex === 3) {
            self.updateConfirmationStep();
        }
    };

    self.validateStep = function (stepIndex) {
        let isValid = true;

        if (stepIndex === 0) {
            // Validate credit card selection
            if (!self.SelectedCreditCard || !self.SelectedCreditCard.Id) {
                // Check if user is entering new card details
                const cardNumber = $('#card_number').val();
                const cardName = $('#card_name').val();
                const expiryDate = $('#expiry_date').val();
                const cvv = $('#cvv').val();

                if (!cardNumber || !cardName || !expiryDate || !cvv) {
                    alert('Please select a saved card or enter new card details');
                    isValid = false;
                } else {
                    // User is entering new card, create temporary object
                    self.SelectedCreditCard = {
                        CardType: self.detectCardType(cardNumber),
                        LastFourDigits: cardNumber.slice(-4),
                        CardHolderName: cardName,
                        ExpiryMonth: expiryDate.split('/')[0],
                        ExpiryYear: '20' + expiryDate.split('/')[1],
                        IssuingBank: 'Unknown Bank'
                    };
                }
            }
        } else if (stepIndex === 1) {
            // Validate bank account selection and amount
            const transferAmount = $('#transfer_amount').val();

            if (!self.SelectedBankAccount || !self.SelectedBankAccount.Id) {
                alert('Please select a bank account');
                isValid = false;
            } else if (!transferAmount || transferAmount <= 0) {
                alert('Please enter a valid transfer amount');
                isValid = false;
            }
        } else if (stepIndex === 2) {
            // Validate billing address selection
            if (!self.SelectedBillingAddress || !self.SelectedBillingAddress.Id) {
                // Check if user is entering new address
                const addressLine1 = $('#address_line1').val();
                const city = $('#city').val();
                const state = $('#state').val();
                const zipCode = $('#zip_code').val();
                const country = $('#country').val();

                if (!addressLine1 || !city || !state || !zipCode || country === 'Select Country') {
                    alert('Please select a saved address or enter a new billing address');
                    isValid = false;
                } else {
                    // User is entering new address, create temporary object
                    self.SelectedBillingAddress = {
                        AddessLineOne: addressLine1,
                        AddessLineTwo: $('#address_line2').val(),
                        AddessLineThress: '',
                        Location: city,
                        StateId: 0, // You might need to map this
                        PinCode: zipCode,
                        CountryId: 0 // You might need to map this
                    };
                }
            }
        }

        return isValid;
    };

    // Helper function to detect card type from number
    self.detectCardType = function (cardNumber) {
        const cleaned = cardNumber.replace(/\s+/g, '');
        if (/^4/.test(cleaned)) return 'Visa';
        if (/^5[1-5]/.test(cleaned)) return 'MasterCard';
        if (/^3[47]/.test(cleaned)) return 'American Express';
        if (/^6(?:011|5)/.test(cleaned)) return 'Discover';
        return 'Credit Card';
    };
}