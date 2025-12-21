function RedeemRewardsController() {
    var self = this;

    var currentStep = 0;

    var actions = [];

    var dataObjects = [];

    self.BankAccounts = [];

    self.BillingAddress = [];

    self.PaymentReasons = [];

    self.TransactionFees = [];

    self.WalletBalance = {};

    self.SelectedBankAccount = {};

    self.SelectedBillingAddress = {};

    self.CurrentPaymentItem = {};

    self.CurrentPlacedPaymentOrder = {};

    self.CurrentOrder = null;

    self.stripe = Stripe('pk_test_51SJ2Nu32ZfCJ3T7ZWZPFz4MQCYWPWDUKriHaal61XqtP8lAbJzUIcGvbVaEnbUoZl2UiTyuTCbfLS3pOMPyJSczd00fVddwCBD');

    self.CurrentPaymentItem.SelectedBankAccount = self.SelectedBankAccount;

    self.CurrentPaymentItem.SelectedBillingAddress = self.SelectedBillingAddress;

    self.CurrentUserId = null;

    self.CurrentWalletId = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);

        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        self.CurrentUserId = getQueryStringParameter("userId");
        self.CurrentWalletId = getQueryStringParameter("walletId");

        actions.push("/UserWallet/GetUserWallet");
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

            self.WalletBalance = responses[0][0] && responses[0][0].data ? responses[0][0].data : {};
            self.BankAccounts = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];
            self.BillingAddress = responses[2][0] && responses[2][0].data ? responses[2][0].data : [];
            self.PaymentReasons = responses[3][0] && responses[3][0].data ? responses[3][0].data : [];
            self.TransactionFees = responses[4][0] && responses[4][0].data ? responses[4][0].data : [];

            // Display wallet balance
            self.displayWalletBalance();

            genarateDropdown("TransactionFee", self.TransactionFees, "Id", "Name");
            genarateDropdown("PaymentReason", self.PaymentReasons, "Id", "Name");

            // Bind the data to the form
            self.bindBankAccounts();
            self.bindBillingAddresses();

            // Set default values
            $("#TransactionFee").val(8);
            $("#TransactionFee").prop("disabled", true);
            $("#FeeCollectionMethod").val("Withdraw");

            $(".se-pre-con").hide();

            // Initial validation
            self.validateAmount();

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

        // Submit button functionality for withdrawal
        $('.submit_button').on('click', function () {
            if (self.validateStep(currentStep)) {
                self.processWithdrawal();
            }
        });

        // Real-time amount validation
        $('#withdrawal_amount').on('input', function () {
            self.validateAmount();
        });

        // Update confirmation when amount changes
        $('#withdrawal_amount, #PaymentReason, #TransactionFee, #FeeCollectionMethod').on('change', function () {
            if (currentStep === 3) {
                self.updateConfirmationStep();
            }
        });
    };

    self.displayWalletBalance = function () {
        const walletStep = $('.main').eq(0);
        if (self.WalletBalance && self.WalletBalance.CurrentBalance !== undefined) {
            const walletHtml = `
                <div class="wallet-balance-card">
                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <div>
                            <h5 class="text-muted mb-1">Available Balance</h5>
                            <h2 class="text-success fw-bold" id="currentBalanceDisplay">$${self.WalletBalance.CurrentBalance.toFixed(2)}</h2>
                        </div>
                        <i class="fas fa-wallet fa-3x text-primary"></i>
                    </div>
                    <div class="alert alert-info">
                        <i class="fas fa-info-circle me-2"></i>
                        You can withdraw up to $${self.WalletBalance.CurrentBalance.toFixed(2)} from your wallet.
                    </div>
                </div>
            `;
            $('#walletBalanceContainer').html(walletHtml);
        }
    };

    self.validateAmount = function () {
        const amount = parseFloat($('#withdrawal_amount').val()) || 0;
        const currentBalance = self.WalletBalance ? (self.WalletBalance.CurrentBalance || 0) : 0;
        const $input = $('#withdrawal_amount');
        const $errorDiv = $('#amount-error');
        const $errorMessage = $('#error-message');
        const $nextButton = $('#nextStepButton');

        // Reset styles
        $input.removeClass('is-invalid is-valid');
        $errorDiv.hide();

        let isValid = true;
        let errorMessage = '';

        if (amount <= 0) {
            isValid = false;
            errorMessage = 'Please enter a valid withdrawal amount';
        } else if (amount > currentBalance) {
            isValid = false;
            errorMessage = `Amount exceeds available balance ($${currentBalance.toFixed(2)})`;

            // Apply red border and disable next button
            $input.addClass('is-invalid');
            $errorMessage.text(errorMessage);
            $errorDiv.show();
            $nextButton.prop('disabled', true).addClass('disabled');
            return false;
        } else if (amount > 0 && amount <= currentBalance) {
            // Valid amount
            $input.addClass('is-valid');
            $nextButton.prop('disabled', false).removeClass('disabled');

            // Show success message for large amounts
            if (amount > currentBalance * 0.8) {
                $errorMessage.text(`You're withdrawing ${((amount / currentBalance) * 100).toFixed(1)}% of your balance`);
                $errorDiv.removeClass('text-danger').addClass('text-warning').show();
            } else {
                $errorDiv.hide();
            }
        }

        // Update next button state
        $nextButton.prop('disabled', !isValid);

        return isValid;
    };

    self.bindBankAccounts = function () {
        if (self.BankAccounts && self.BankAccounts.length > 0) {
            const accountsHtml = self.BankAccounts.map(account => `
                <div class="payment-option selectable-account mb-3" data-type="bank-account" data-id="${account.Id}">
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

            $('#bankAccountsContainer').html(accountsHtml);

            // Select first account by default
            $('.selectable-account').first().addClass('selected');
            if (self.BankAccounts.length > 0) {
                self.SelectedBankAccount = self.BankAccounts[0];
            }
        } else {
            $('#bankAccountsContainer').html(`
                <div class="alert alert-warning">
                    <i class="fas fa-exclamation-triangle me-2"></i>
                    No bank accounts found. Please add a bank account first.
                </div>
            `);
        }
    };

    self.bindBillingAddresses = function () {
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

            $('#billingAddressesContainer').html(addressesHtml);

            // Select first address by default
            $('.selectable-address[data-type="billing-address"]').first().addClass('selected');
            if (self.BillingAddress.length > 0) {
                self.SelectedBillingAddress = self.BillingAddress[0];
            }
        } else {
            $('#billingAddressesContainer').html(`
                <div class="alert alert-warning">
                    <i class="fas fa-exclamation-triangle me-2"></i>
                    No billing addresses found. Please add a billing address first.
                </div>
            `);
        }
    };

    self.getAddressTitle = function (address) {
        return address.Title || "Billing Address";
    };

    self.getStateName = function (stateId) {
        const stateMap = {
            24: "Telangana"
        };
        return stateMap[stateId] || "State";
    };

    self.getCountryName = function (countryId) {
        const countryMap = {
            1: "India"
        };
        return countryMap[countryId] || "Country";
    };

    self.updateConfirmationStep = function () {
        const withdrawalAmount = parseFloat($('#withdrawal_amount').val()) || 0;

        if (withdrawalAmount > 0) {
            var transactionFeeId = $("#TransactionFee").val();
            var paymentReasonId = $("#PaymentReason").val();
            var feeCollectionType = $("#FeeCollectionMethod").val();
            var transactionfees = self.TransactionFees.filter(x => x.Id == transactionFeeId)[0];
            const processingFeePercentage = transactionfees ? transactionfees.Price : 0;
            const platformFeeAmount = (withdrawalAmount * processingFeePercentage) / 100;

            let userReceives;
            let feeCollectionText;
            let totalAmount;

            if (feeCollectionType === "Yes") {
                userReceives = withdrawalAmount;
                feeCollectionText = "added to amount";
                totalAmount = withdrawalAmount + platformFeeAmount;
            } else if (feeCollectionType === "No") {
                userReceives = withdrawalAmount - platformFeeAmount;
                feeCollectionText = "deducted from amount";
                totalAmount = withdrawalAmount;
            }
            else {
                userReceives = withdrawalAmount - 0;
                feeCollectionText = "No Fee";
                totalAmount = withdrawalAmount;
            }

            // Update UI elements
            $('#usrTransferAmount').text('$' + withdrawalAmount.toFixed(2));
            $('#usrProcessingFee').text(processingFeePercentage.toFixed(2) + '%');
            $('#usrFeeCollection').text(feeCollectionText);
            $('#usrPlotformCharges').text('$' + platformFeeAmount.toFixed(2));
            $('#usrUserReceves').text('$' + userReceives.toFixed(2));
            $('#usrTotalAmount').text('$' + totalAmount.toFixed(2));
        }

        // Update bank account summary
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
            $('#bankAccountSummary').html(bankAccountHtml);
        }

        // Update billing address summary
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
            $('#billingAddressSummary').html(billingAddressHtml);
        }
    };

    self.processWithdrawal = function () {
        const withdrawalAmount = parseFloat($('#withdrawal_amount').val()) || 0;

        // Final validation
        if (!self.validateAmount()) {
            return;
        }

        var transactionFeeId = $("#TransactionFee").val();
        var paymentReasonId = $("#PaymentReason").val();
        var feeCollectionType = $("#FeeCollectionMethod").val();
        var transactionfees = self.TransactionFees.filter(x => x.Id == transactionFeeId)[0];
        const plotFormFeePercentage = transactionfees ? transactionfees.Price : 0;
        const platformFeeAmount = (withdrawalAmount * plotFormFeePercentage) / 100;

        let totalAmountToChargeCustomer;
        let totalAmountToDepositToCustomer;
        let totalPlatformFee = platformFeeAmount;

        if (feeCollectionType === "Yes") {
            totalAmountToChargeCustomer = withdrawalAmount + platformFeeAmount;
            totalAmountToDepositToCustomer = withdrawalAmount;
        } else {
            totalAmountToChargeCustomer = withdrawalAmount;
            totalAmountToDepositToCustomer = withdrawalAmount - platformFeeAmount;
        }

        var withdrawalOrderObject = {
            Id: 0,
            OrderReference: generateOrderReference(self.ApplicationUser.Id, OrderType.Withdrawal),
            UserId: self.ApplicationUser.Id,
            PaymentReasonId: paymentReasonId,
            PaymentOrderType: 'Withdrawal',
            WalletId: self.WalletBalance.Id,
            Amount: withdrawalAmount,
            TransactionFeeId: transactionFeeId,
            PlatformFeeAmount: platformFeeAmount,
            FeeCollectionMethod: feeCollectionType,
            TotalAmountToChargeCustomer: totalAmountToChargeCustomer,
            TotalAmountToDepositToCustomer: totalAmountToDepositToCustomer,
            TotalPlatformFee: totalPlatformFee,
            Currency: "INR",
            CreditCardId: null,
            BankAccountId: self.SelectedBankAccount.Id,
            BillingAddressId: self.SelectedBillingAddress.Id,
            OrderStatusId: statusConstants.Draft,
            PaymentStatusId: statusConstants.Pending,
            StripePaymentIntentId: null,
            StripePaymentChargeId: null,
            StripeDepositeIntentId: null,
            StripeDepositeChargeId: null,
        };

        var withdrawalOrder = addCommonProperties(withdrawalOrderObject);

        console.log("Withdrawal Order: " + JSON.stringify(withdrawalOrder));

        $('#submitWithdrawalButton').prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-2"></i>Processing...');

        //makeAjaxRequest({
        //    url: API_URLS.PlaceWithdrawalOrderAsync,
        //    data: withdrawalOrder,
        //    type: 'POST',
        //    successCallback: function (response) {
        //        console.info(response);
        //        self.CurrentPlacedPaymentOrder = response && response.data ? response.data : {};
        //        self.showSuccessPage();
        //    },
        //    errorCallback: function (xhr, status, error) {
        //        console.error("Error in placing withdrawal order: " + error);
        //        alert("Withdrawal failed: " + (xhr.responseJSON ? xhr.responseJSON.message : error));
        //        $('#submitWithdrawalButton').prop('disabled', false).html('Confirm Withdrawal');
        //        $(".se-pre-con").hide();
        //    }
        //});
    };

    self.showSuccessPage = function () {
        const withdrawalAmount = parseFloat($('#withdrawal_amount').val()) || 0;
        const orderReference = self.CurrentPlacedPaymentOrder.OrderReference || "N/A";

        $('.congrats').html(`
            <h2>Withdrawal Request Successful!</h2>
            <p>Your withdrawal request of <strong>$${withdrawalAmount.toFixed(2)}</strong> has been submitted successfully.</p>
            <p>Funds will be transferred to your bank account within 2-3 business days.</p>
            <div class="alert alert-success mt-3">
                <i class="fas fa-info-circle me-2"></i>
                <strong>Transaction Details:</strong><br>
                Order Reference: <strong>${orderReference}</strong><br>
                Bank: ${self.SelectedBankAccount.BankName}<br>
                Account: ****${self.SelectedBankAccount.LastFourDigits}<br>
                Amount to Receive: <strong>$${withdrawalAmount.toFixed(2)}</strong>
            </div>
            <div class="mt-4">
                <a href="@Url.Action("Index", "UserBoard")" class="btn btn-primary me-2">
                    <i class="fas fa-home me-2"></i>Return to Dashboard
                </a>
                <a href="@Url.Action("TransactionHistory", "UserBoard")" class="btn btn-outline-primary">
                    <i class="fas fa-history me-2"></i>View Transaction History
                </a>
            </div>
        `);

        const $steps = $('.main');
        const $stepContents = $('.step-number-content');
        const $progressItems = $('.progress-bar li');
        const $stepNumber = $('.step-number');

        self.goToStep(4, $steps, $stepContents, $progressItems, $stepNumber);
    };

    self.goToStep = function (stepIndex, $steps, $stepContents, $progressItems, $stepNumber) {
        $steps.eq(currentStep).removeClass('active');
        $stepContents.eq(currentStep).removeClass('active').addClass('d-none');
        $progressItems.eq(currentStep).removeClass('active');

        $steps.eq(stepIndex).addClass('active');
        $stepContents.eq(stepIndex).removeClass('d-none').addClass('active');
        $progressItems.eq(stepIndex).addClass('active');

        $stepNumber.text(stepIndex + 1);
        currentStep = stepIndex;

        if (stepIndex === 3) {
            self.updateConfirmationStep();
        }

        self.updateButtonVisibility();
    };

    self.updateButtonVisibility = function () {
        $('.back_button').hide();
        $('.next_button').hide();
        $('.submit_button').hide();

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
            isValid = self.validateAmount();
            if (!isValid) {
                const amount = parseFloat($('#withdrawal_amount').val()) || 0;
                if (amount <= 0) {
                    alert('Please enter a valid withdrawal amount');
                } else if (amount > (self.WalletBalance.CurrentBalance || 0)) {
                    alert('Amount exceeds available wallet balance');
                }
            }
        } else if (stepIndex === 1) {
            if (!self.SelectedBankAccount || !self.SelectedBankAccount.Id) {
                alert('Please select a bank account');
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

    // Event handler for bank account selection
    $(document).on('click', '.selectable-account', function () {
        $('.selectable-account').removeClass('selected');
        $(this).addClass('selected');

        const dataId = $(this).data('id');
        self.SelectedBankAccount = self.BankAccounts.find(account => account.Id === dataId) || {};

        if (currentStep === 3) {
            self.updateConfirmationStep();
        }
    });

    // Event handler for billing address selection
    $(document).on('click', '.selectable-address', function () {
        $('.selectable-address').removeClass('selected');
        $(this).addClass('selected');

        const dataId = $(this).data('id');
        self.SelectedBillingAddress = self.BillingAddress.find(address => address.Id === dataId) || {};

        if (currentStep === 3) {
            self.updateConfirmationStep();
        }
    });
}