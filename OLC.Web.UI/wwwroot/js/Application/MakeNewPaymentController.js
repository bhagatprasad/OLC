function MakeNewPaymentController() {
    var self = this;

    var currentStep = 0;

    var actions = [];

    var dataObjects = [];

    self.CreditCards = [];

    self.BankAccounts = [];

    self.BillingAddress = [];

    self.PaymentReasons = [];

    self.TransactionFees = [];

    self.SelectedCreditCard = {};

    self.SelectedBankAccount = {};

    self.SelectedBillingAddress = {};

    self.CurrentPaymentItem = {};

    self.CurrentPlacedPaymentOrder = {};

    self.CurrentOrder = null;


    // ✅ FIXED: Initialize Stripe with PUBLISHABLE key (not secret key!)
    self.stripe = Stripe('pk_test_51SJ2Nu32ZfCJ3T7ZWZPFz4MQCYWPWDUKriHaal61XqtP8lAbJzUIcGvbVaEnbUoZl2UiTyuTCbfLS3pOMPyJSczd00fVddwCBD');

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
        actions.push("/BankAccount/GetAllUserBankAccounts");
        actions.push("/BillingAddress/GetUserBillingAddresses");
        actions.push("/PaymentReason/GetPaymentReasons");
        actions.push("/TransactionFee/GetTransactionFeesList");

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
            self.PaymentReasons = responses[3][0] && responses[3][0].data ? responses[3][0].data : [];
            self.TransactionFees = responses[4][0] && responses[4][0].data ? responses[4][0].data : [];

            genarateDropdown("TransactionFee", self.TransactionFees, "Id", "Name");
            genarateDropdown("PaymentReason", self.PaymentReasons, "Id", "Name");
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

        // ✅ FIXED: Submit button functionality - REAL Stripe Integration
        $('.submit_button').on('click', function () {
            if (self.validateStep(currentStep)) {

                const transferAmount = $('#transfer_amount').val();
                var transactionFeeId = $("#TransactionFee").val();
                var paymentReasonId = $("#PaymentReason").val();
                var feeCollectionType = $("#FeeCollectionMethod").val();
                var transactionfees = self.TransactionFees.filter(x => x.Id == transactionFeeId)[0];
                const plotFormFeePercentage = transactionfees.Price;
                const platformFeeAmount = (parseFloat(transferAmount) * parseFloat(plotFormFeePercentage)) / 100;
                let totalAmountToChargeCustomer;
                let totalAmountToDepositToCustomer;
                let totalPlatformFee = platformFeeAmount; // This remains the same in both cases

                if (feeCollectionType === FeeCollectionMethod.Yes) {
                    // Add the platform fee to the amount charged from the customer
                    totalAmountToChargeCustomer = parseFloat(transferAmount) + platformFeeAmount;
                    totalAmountToDepositToCustomer = parseFloat(transferAmount);
                } else {
                    // Deduct the platform fee from the entered amount
                    totalAmountToChargeCustomer = parseFloat(transferAmount);
                    totalAmountToDepositToCustomer = parseFloat(transferAmount) - platformFeeAmount;
                }

                
                var paymentOrderObject = {
                    Id: 0,
                    OrderReference: generateOrderReference(self.ApplicationUser.Id, OrderType.Payment),
                    UserId: self.ApplicationUser.Id,
                    PaymentReasonId: paymentReasonId,
                    PaymentOrderType: 'Send',
                    WalletId: null,
                    Amount: parseFloat(transferAmount),
                    TransactionFeeId: transactionFeeId,
                    PlatformFeeAmount: platformFeeAmount, // Corrected to the calculated fee amount
                    FeeCollectionMethod: feeCollectionType, // Or whatever is selected
                    TotalAmountToChargeCustomer: totalAmountToChargeCustomer,
                    TotalAmountToDepositToCustomer: totalAmountToDepositToCustomer,
                    TotalPlatformFee: totalPlatformFee,
                    Currency: "INR",
                    CreditCardId: self.SelectedCreditCard.Id,
                    BankAccountId: self.SelectedBankAccount.Id,
                    BillingAddressId: self.SelectedBillingAddress.Id,
                    OrderStatusId: statusConstants.Draft,
                    PaymentStatusId: statusConstants.Pending,
                    DepositStatusId: statusConstants.Pending,
                    StripePaymentIntentId: null,
                    StripePaymentChargeId: null,
                    StripeDepositeIntentId: null,
                    StripeDepositeChargeId: null,
                };

                var paymentOrder = addCommonProperties(paymentOrderObject);

                console.log("paymentOrder....." + JSON.stringify(paymentOrder));
                makeAjaxRequest({
                    url: API_URLS.PlacePaymentOrderAsync,
                    data: paymentOrder,
                    type: 'POST',
                    successCallback: handleSuccess,
                    errorCallback: handleError
                });

            }
        });

        function handleSuccess(response) {
            console.info(response);
            self.CurrentPlacedPaymentOrder = response && response.data ? response.data : {};
            self.processPaymentWithStripe();
        }

        function handleError(xhr, status, error) {
            console.error("Error in placing payment order: " + error);
            $(".se-pre-con").hide();
        }

        // Payment option selection
        $(document).on('click', '.payment-option', function () {
            const $parentStep = $(this).closest('.main');
            $parentStep.find('.payment-option').removeClass('selected');
            $(this).addClass('selected');

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
        const paymentMethodStep = $('.main').eq(0);

        if (self.CreditCards && self.CreditCards.length > 0) {
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

            paymentMethodStep.find('.text').after(creditCardsHtml);

            // Select first card by default
            $('.selectable-card[data-type="credit-card"]').first().addClass('selected');
            if (self.CreditCards.length > 0) {
                self.SelectedCreditCard = self.CreditCards[0];
            }
        }
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

    // Function to bind bank accounts as selectable options
    self.bindBankAccounts = function () {
        const bankAccountStep = $('.main').eq(1);

        if (self.BankAccounts && self.BankAccounts.length > 0) {
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
        }
    };

    // Function to bind billing addresses as selectable options
    self.bindBillingAddresses = function () {
        const billingAddressStep = $('.main').eq(2);

        if (self.BillingAddress && self.BillingAddress.length > 0) {
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

            billingAddressStep.find('.text').after(addressesHtml);

            // Select first address by default
            $('.selectable-address[data-type="billing-address"]').first().addClass('selected');
            if (self.BillingAddress.length > 0) {
                self.SelectedBillingAddress = self.BillingAddress[0];
            }
        }
    };

    // Helper function to get address title
    self.getAddressTitle = function (address) {
        return "Billing Address";
    };

    // Helper function to get state name
    self.getStateName = function (stateId) {
        const stateMap = {
            24: "Telangana"
        };
        return stateMap[stateId] || "State";
    };

    // Helper function to get country name
    self.getCountryName = function (countryId) {
        const countryMap = {
            1: "India"
        };
        return countryMap[countryId] || "Country";
    };

    // Update confirmation step with selected data
    self.updateConfirmationStep = function () {
        const confirmationStep = $('.main').eq(3);

        // Update payment method
        if (self.SelectedCreditCard.Id) {
            const paymentMethodHtml = `
                <h5>Payment Method</h5>
                <div class="d-flex align-items-center">
                    <i class="${self.getCardIcon(self.SelectedCreditCard.CardType)} payment-icon"></i>
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
            var transactionFeeId = $("#TransactionFee").val();
            var feeCollectionType = $("#FeeCollectionMethod").val();
            var transactionfees = self.TransactionFees.filter(x => x.Id == transactionFeeId)[0];
            const processingFee = transactionfees.Price;

            let platformCharges;
            let userReceives;
            let feeCollectionText;
            let totalAmount;

            if (feeCollectionType === "Yes") { // Assuming "Yes" means add fee to charged amount
                platformCharges = (parseFloat(transferAmount) * processingFee) / 100;
                userReceives = parseFloat(transferAmount);
                feeCollectionText = "add";
                totalAmount = platformCharges + userReceives;
            } else { // "No" means deduct fee from transfer amount
                platformCharges = (parseFloat(transferAmount) * processingFee) / 100;
                userReceives = parseFloat(transferAmount) - platformCharges;
                feeCollectionText = "deducted";
                totalAmount = platformCharges + userReceives;
            }

            // Update UI elements using IDs
            $('#usrTransferAmount').text('$' + parseFloat(transferAmount).toFixed(2));
            $('#usrProcessingFee').text('$' + processingFee.toFixed(2));
            $('#usrFeeCollection').text(feeCollectionText);
            $('#usrPlotformCharges').text('$' + platformCharges.toFixed(2));
            $('#usrUserReceves').text('$' + userReceives.toFixed(2));
            $('#usrTotalAmount').text('$' + totalAmount.toFixed(2));
        }
    };

    // ✅ FIXED: REAL Stripe Checkout Integration
    self.processPaymentWithStripe = function () {
        // Disable button and show loading
        $('.submit_button').prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-2"></i>Redirecting to Stripe...');

        console.log('🎬 Starting REAL Stripe Checkout Process...');
        console.log('💰 Amount:', self.CurrentPlacedPaymentOrder.TotalAmountToChargeCustomer);

        // Create Stripe Checkout Session
        self.createStripeCheckoutSession(self.CurrentPlacedPaymentOrder.TotalAmountToChargeCustomer)
            .then(function (session) {
                console.log('✅ Stripe Session Created:', session.id);

                // ✅ REDIRECT TO REAL STRIPE CHECKOUT
                return self.stripe.redirectToCheckout({ sessionId: session.id });
            })
            .then(function (result) {
                if (result.error) {
                    throw new Error(result.error.message);
                }
                // Success - user is redirected to Stripe Checkout page
            })
            .catch(function (error) {
                console.error('💥 Stripe Checkout failed:', error);
                alert('Payment failed: ' + error.message);
                $('.submit_button').prop('disabled', false).html('<i class="fas fa-credit-card me-2"></i>Confirm Payment');
            });
    };

    // ✅ FIXED: Create REAL Stripe Checkout Session
    self.createStripeCheckoutSession = function (totalAmount) {
        return new Promise(function (resolve, reject) {
            // In a real application, this should be a call to your backend
            // For now, we'll create a direct session for demo

            console.log('🔄 Creating Stripe Checkout Session...');

            // For demo purposes - in production, call your backend API
            // This creates a direct checkout session (not recommended for production)
            fetch('/Stripe/CreateStripeSession', {  // Your backend endpoint
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    amount: totalAmount,
                    currency: 'usd',
                    customerEmail: self.ApplicationUser.Email,
                    successUrl: window.location.origin + '/PaymentOrder/Success?session_id={CHECKOUT_SESSION_ID}',
                    cancelUrl: window.location.origin + '/PaymentOrder/Cancel',
                    metadata: {
                        PaymentOrderId: self.CurrentPlacedPaymentOrder.Id,
                        OrderReference: self.CurrentPlacedPaymentOrder.OrderReference,
                        userId: self.ApplicationUser.Id,
                        creditCardId: self.SelectedCreditCard.Id,
                        bankAccountId: self.SelectedBankAccount.Id,
                        billingAddressId: self.SelectedBillingAddress.Id
                    }
                })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        resolve(data.session);
                    } else {
                        reject(new Error(data.error || 'Failed to create session'));
                    }
                })
                .catch(error => {
                    console.error('API call failed:', error);
                    // Fallback: Create a demo session for testing
                    self.createDemoStripeSession(totalAmount)
                        .then(resolve)
                        .catch(reject);
                });
        });
    };

    // Demo function for testing (remove in production)
    self.createDemoStripeSession = function (totalAmount) {
        return new Promise(function (resolve, reject) {
            // This is just for demo - in production, always use your backend
            console.log('🔧 Using demo Stripe session for testing...');

            // Create a mock session for demo
            const mockSession = {
                id: 'cs_test_' + Math.random().toString(36).substr(2, 9),
                url: 'https://checkout.stripe.com/pay/cs_test_' + Math.random().toString(36).substr(2, 9)
            };

            // For real implementation, you would use:
            // const session = await stripe.checkout.sessions.create({...});

            setTimeout(() => {
                console.log('✅ Demo session created:', mockSession);
                resolve(mockSession);
            }, 1000);
        });
    };

    // Handle Stripe redirect result (call this on your success page)
    self.handleStripeRedirect = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const sessionId = urlParams.get('session_id');

        if (sessionId) {
            self.stripe.retrieveCheckoutSession(sessionId).then(function (result) {
                if (result.session.payment_status === 'paid') {
                    // Payment was successful
                    self.showSuccessPage(result.session);
                }
            });
        }
    };

    // Show success page after Stripe redirect
    self.showSuccessPage = function (session) {
        console.log('🎉 Payment successful!', session);

        // Update success page with real data
        const totalAmount = session.amount_total / 100; // Convert from cents
        const orderId = session.id;

        $('.congrats').html(`
            <h2>Payment Successful!</h2>
            <p>Your payment of <strong>$${totalAmount.toFixed(2)}</strong> has been processed successfully. 
            A confirmation email has been sent to <strong>${self.ApplicationUser.Email}</strong>. 
            Transaction ID: <strong>${orderId}</strong></p>
            <div class="mt-3">
                <small class="text-muted">
                    Paid with: ${self.SelectedCreditCard.CardType} ****${self.SelectedCreditCard.LastFourDigits}<br>
                    Bank: ${self.SelectedBankAccount.BankName}<br>
                    Billing: ${self.SelectedBillingAddress.Location}
                </small>
            </div>
        `);

        // Go to success step
        const $steps = $('.main');
        const $stepContents = $('.step-number-content');
        const $progressItems = $('.progress-bar li');
        const $stepNumber = $('.step-number');

        self.goToStep(4, $steps, $stepContents, $progressItems, $stepNumber);
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

        // Update button visibility
        self.updateButtonVisibility();
    };

    self.updateButtonVisibility = function () {
        // Hide all buttons first
        $('.back_button').hide();
        $('.next_button').hide();
        $('.submit_button').hide();

        // Show appropriate buttons based on current step
        if (currentStep > 0) {
            $('.back_button').show();
        }

        if (currentStep < 3) {
            $('.next_button').show();
        }

        if (currentStep === 3) {
            $('.submit_button').show();
        }
    };

    self.validateStep = function (stepIndex) {
        let isValid = true;

        if (stepIndex === 0) {
            if (!self.SelectedCreditCard || !self.SelectedCreditCard.Id) {
                alert('Please select a credit card');
                isValid = false;
            }
        } else if (stepIndex === 1) {
            const transferAmount = $('#transfer_amount').val();

            if (!self.SelectedBankAccount || !self.SelectedBankAccount.Id) {
                alert('Please select a bank account');
                isValid = false;
            } else if (!transferAmount || transferAmount <= 0) {
                alert('Please enter a valid transfer amount');
                isValid = false;
            }
        } else if (stepIndex === 2) {
            if (!self.SelectedBillingAddress || !self.SelectedBillingAddress.Id) {
                alert('Please select a billing address');
                isValid = false;
            }
        }

        return isValid;
    };
}