function PaymentOrderDetailsController() {
    var self = this;

    self.PaymentOrderId = null;
    var actions = [];
    var dataObjects = [];
    self.PaymentOrderDetails = {};
    self.CoreStatuses = [];
    self.CoreCountries = [];
    self.CoreStates = [];
    self.CoreCities = [];
    self.DepositOrderDetails = {};

    actions.push("/PaymentOrder/GetExecutivePaymentOrderDetails");
    actions.push("/Status/GetStatuses");
    actions.push("/Country/GetCountriesList");
    actions.push("/State/GetStatesList");
    actions.push("/City/GetCitiesList");

    self.init = function () {
        $(".se-pre-con").show();
        $("#errorState").addClass("d-none");

        self.PaymentOrderId = getQueryStringParameter("paymentOrderId");
        console.log("Payment Order ID:", self.PaymentOrderId);

        if (!self.PaymentOrderId) {
            self.showErrorState("Invalid payment order ID");
            return;
        }

        dataObjects.push({ paymentOrderId: self.PaymentOrderId });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET',
                timeout: 30000
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log('Payment Orders details response:', responses);

            if (!responses[0] || !responses[0][0] || !responses[0][0].data) {
                self.showErrorState("Invalid response from server");
                return;
            }

            self.PaymentOrderDetails = responses[0][0].data;
            self.CoreStatuses = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];
            self.CoreCountries = responses[2][0] && responses[2][0].data ? responses[2][0].data : [];
            self.CoreStates = responses[3][0] && responses[3][0].data ? responses[3][0].data : [];
            self.CoreCities = responses[4][0] && responses[4][0].data ? responses[4][0].data : [];
         

            self.renderAllCards();
            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.error('One or more requests failed:', error);
            self.showErrorState("Failed to load payment details");
            $(".se-pre-con").hide();
        });
    }
    //////////////////////////
    self.renderAllCards = function () {
        const container = document.getElementById("paymentCardsContainer");
        if (!container) return;

        const cardsHTML = [
            self.generatePaymentInfoCard(),
            self.generateCreditCardCard(),
            self.generateBankAccountCard(),
            self.generateBillingAddressCard(),
            self.generatePaymentHistoryCard(),
            self.generateDepositOrderCard(),
        ].join('');

        container.innerHTML = cardsHTML;
        self.bindAllCardData();
    }

    self.generatePaymentInfoCard = function () {
        return `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card payment-card">
                    <div class="card-header bg-primary text-white d-flex align-items-center">
                        <i class="fas fa-receipt me-2"></i>
                        <h5 class="mb-0 flex-grow-1">Payment Information</h5>
                        <span id="paymentStatus" class="badge bg-light text-dark">--</span>
                    </div>
                    <div class="card-body p-0">
                        <table class="table card-table">
                            <tbody>
                                <tr><td class="fw-semibold text-muted">Order Reference</td><td><span id="orderReference">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Amount</td><td><span id="amount">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Charge Amount</td><td><span id="chargeAmount">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Deposit Amount</td><td><span id="depositAmount">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">User</td><td><span id="user">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Payment Method</td><td><span id="paymentMethod">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Credit Date</td><td><span id="creditDate">--</span></td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;
    }

    self.generateCreditCardCard = function () {
        return `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card payment-card">
                    <div class="card-header bg-info text-white">
                        <i class="fas fa-credit-card me-2"></i>
                        <h5 class="mb-0">Credit Card Used</h5>
                    </div>
                    <div class="card-body p-0">
                        <table class="table card-table">
                            <tbody>
                                <tr><td class="fw-semibold text-muted">Card Holder</td><td><span id="cardHolderName">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Card Type</td><td><span id="cardType">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Card Number</td><td><span id="cardNumberMasked">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Expiry</td><td><span id="cardExpiry">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Issuing Bank</td><td><span id="issuingBank">--</span></td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;
    }

    self.generateBankAccountCard = function () {
        return `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card payment-card">
                    <div class="card-header bg-success text-white">
                        <i class="fas fa-university me-2"></i>
                        <h5 class="mb-0">Bank Account</h5>
                    </div>
                    <div class="card-body p-0">
                        <table class="table card-table">
                            <tbody>
                                <tr><td class="fw-semibold text-muted">Account Holder</td><td><span id="bankAccountHolderName">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Bank Name</td><td><span id="bankName">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Account Number</td><td><span id="bankAccountNumber">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Account Type</td><td><span id="bankAccountType">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Branch Name</td><td><span id="bankBranchName">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">IFSC Code</td><td><span id="bankIFSC">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">SWIFT Code</td><td><span id="bankSwift">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Verification</td><td><span id="bankVerificationStatus" class="badge status-badge">--</span></td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;
    }

    self.generateBillingAddressCard = function () {
        return `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card payment-card">
                    <div class="card-header bg-warning text-white">
                        <i class="fas fa-map-marker-alt me-2"></i>
                        <h5 class="mb-0">Billing Address</h5>
                    </div>
                    <div class="card-body p-0">
                        <table class="table card-table">
                            <tbody>
                                <tr><td class="fw-semibold text-muted">Address Line 1</td><td><span id="addressLine1">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Address Line 2</td><td><span id="addressLine2">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Address Line 3</td><td><span id="addressLine3">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Location</td><td><span id="addressLocation">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">City</td><td><span id="addressCity">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">State</td><td><span id="addressState">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Country</td><td><span id="addressCountry">--</span></td></tr>
                                <tr><td class="fw-semibold text-muted">Pin Code</td><td><span id="addressPinCode">--</span></td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;
    }

    self.generatePaymentHistoryCard = function () {
        const historyRows = self.generateHistoryRows();

        return `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card payment-card">
                    <div class="card-header bg-dark text-white">
                        <i class="fas fa-history me-2"></i>
                        <h5 class="mb-0">Payment Order History</h5>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive" style="max-height: 400px;">
                            <table class="table table-sm card-table mb-0">
                                <thead class="table-light sticky-top">
                                    <tr>
                                        <th class="ps-3">ID</th>
                                        <th>Status</th>
                                        <th>Description</th>
                                        <th class="pe-3">Date</th>
                                    </tr>
                                </thead>
                                <tbody id="paymentOrderHistory">
                                    ${historyRows}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

       
    self.generateDepositOrderCard = function () {
        const depositHistoryRows = self.generateDepositHistoryRows();

        return `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card payment-card">
                    <div class="card-header bg-info text-white">
                        <i class="fas fa-credit-card me-2"></i>
                        <h5 class="mb-0">Deposit Order</h5>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive" style="max-height: 400px;">
                            <table class="table table-sm card-table mb-0">
                                <thead class="table-light sticky-top">
                                    <tr>
                                        <th class="ps-3">OrderReference</th>
                                        <th class="text-end">Actual Amount</th>
                                        <th class="text-end">Deposited Amount</th>
                                        <th class="text-end pe-3">Pending Amount</th>
                                    </tr>
                                </thead>
                                <tbody id="depositOrderHistory">
                                    ${depositHistoryRows}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

    //if (!self.PaymentOrder || !self.PaymentOrder.depositOrderHistory) {
    //    self.PaymentOrder = self.PaymentOrder || {};
    //    self.PaymentOrder.depositOrderHistory = [
    //        {
    //            OrderReference: "ORD‑2025‑001",
    //            ActualAmount: 15000,
    //            DepositedAmount: 9000,
    //            PendingAmount: 6000
    //        },
    //        {
    //            OrderReference: "ORD‑2025‑002",
    //            ActualAmount: 8000,
    //            DepositedAmount: 8000,
    //            PendingAmount: 0
    //        }
    //    ];
    //}

    self.generateHistoryRows = function () {
        if (!self.PaymentOrderDetails.paymentOrderHistory || self.PaymentOrderDetails.paymentOrderHistory.length === 0) {
            return '<tr><td colspan="6" class="text-center text-muted py-4">No history records found</td></tr>';
        }

        return self.PaymentOrderDetails.paymentOrderHistory.map(item => {
            const createdOn = item.CreatedOn ? new Date(item.CreatedOn).toLocaleDateString() : "--";
            const currentStatus = self.CoreStatuses.find(x => x.Id == item.StatusId);
            return `
                <tr>
                    <td class="ps-3">${item.Id || '--'}</td>
                    <td><span class="badge bg-secondary">${currentStatus ? currentStatus.Name : '--'}</span></td>
                    <td class="text-truncate" style="max-width: 200px;" title="${item.Description || ''}">${item.Description || '--'}</td>
                    <td class="pe-3">${createdOn}</td>
                </tr>
            `;
        }).join('');
    };
 
    self.generateDepositHistoryRows = function () {
        console.log("PaymentOrderDetails:", self.PaymentOrderDetails);

        const history = self.PaymentOrderDetails?.DepositeOrders;

        if (!history || history.length === 0) {
            return '<tr><td colspan="4" class="text-center text-muted py-4">No deposit records found</td></tr>';
        }

        return history.map(item => `
        <tr>
            <td class="ps-3">${item.OrderReference || '--'}</td>
            <td>${item.ActualDepositeAmount != null ? item.ActualDepositeAmount : '--'}</td>
            <td>${item.DepositeAmount != null ? item.DepositeAmount : '--'}</td>
            <td>${item.PendingDepositeAmount != null ? item.PendingDepositeAmount : '--'}</td>
        </tr>
    `).join('');
    };

    self.bindAllCardData = function () {
        const data = self.PaymentOrderDetails;
        if (!data) return;

        const country = data.userBillingAddress && data.userBillingAddress.CountryId
            ? self.CoreCountries.find(x => x.Id == data.userBillingAddress.CountryId)
            : {};
        const state = data.userBillingAddress && data.userBillingAddress.StateId
            ? self.CoreStates.find(x => x.Id == data.userBillingAddress.StateId)
            : {};
        const city = data.userBillingAddress && data.userBillingAddress.CityId
            ? self.CoreCities.find(x => x.Id == data.userBillingAddress.CityId)
            : {};
        
            

        // Helper functions
        const setText = (selector, value, fallback = '--') => {
            const element = document.querySelector(selector);
            if (element) {
                element.textContent = value !== undefined && value !== null && value !== '' ? value : fallback;
            }
        };

        const setStatus = (selector, status) => {
            const element = document.querySelector(selector);
            if (element && status) {
                element.textContent = status;
                const statusLower = status.toLowerCase();
                let badgeClass = 'badge status-badge bg-secondary';

                if (statusLower.includes('success') || statusLower.includes('completed') || statusLower.includes('verified')) {
                    badgeClass = 'badge status-badge bg-success';
                } else if (statusLower.includes('pending') || statusLower.includes('processing')) {
                    badgeClass = 'badge status-badge bg-warning';
                } else if (statusLower.includes('failed') || statusLower.includes('error') || statusLower.includes('rejected')) {
                    badgeClass = 'badge status-badge bg-danger';
                }

                element.className = badgeClass;
            }
        };

        // Update quick status badge
        setStatus("#quickStatus", data.paymentOrder?.OrderStatus);
        setStatus("#paymentStatus", data.paymentOrder?.OrderStatus);

        // Payment Information
        setText("#orderReference", data.paymentOrder?.OrderReference);
        setText("#amount", data.paymentOrder?.Amount ? `$${parseFloat(data.paymentOrder.Amount).toFixed(2)}` : null);
        setText("#chargeAmount", data.paymentOrder?.TotalPlatformFee ? `$${parseFloat(data.paymentOrder.TotalPlatformFee).toFixed(2)}` : null);
        setText("#depositAmount", data.paymentOrder?.TotalAmountToDepositToCustomer ? `$${parseFloat(data.paymentOrder.TotalAmountToDepositToCustomer).toFixed(2)}` : null);
        setText("#user", data.paymentOrder?.UserEmail);
        setText("#paymentMethod", data.userCreditCard?.EncryptedCardNumber);
        setText("#creditDate", data.paymentOrder?.CreatedOn ? new Date(data.paymentOrder.CreatedOn).toLocaleDateString() : null);

        // Credit Card Data
        setText("#cardHolderName", data.userCreditCard?.CardHolderName);
        setText("#cardType", data.userCreditCard?.CardType);
        setText("#cardNumberMasked", data.userCreditCard?.MaskedCardNumber);
        setText("#cardExpiry",
            data.userCreditCard?.ExpiryMonth && data.userCreditCard?.ExpiryYear
                ? `${data.userCreditCard.ExpiryMonth}/${data.userCreditCard.ExpiryYear}`
                : null
        );
        setText("#issuingBank", data.userCreditCard?.IssuingBank);

        // Bank Account Information
        setText("#bankAccountHolderName", data.paymentOrderBankAccount?.AccountHolderName);
        setText("#bankName", data.paymentOrderBankAccount?.BankName);
        setText("#bankAccountNumber", data.paymentOrderBankAccount?.AccountNumber);
        setText("#bankAccountType", data.paymentOrderBankAccount?.AccountType);
        setText("#bankBranchName", data.paymentOrderBankAccount?.BranchName);
        setText("#bankIFSC", data.paymentOrderBankAccount?.IFSCCode);
        setText("#bankSwift", data.paymentOrderBankAccount?.SWIFTCode);
        setStatus("#bankVerificationStatus", data.paymentOrderBankAccount?.VerificationStatus);

        // Billing Address Information
        setText("#addressLine1", data.userBillingAddress?.AddessLineOne);
        setText("#addressLine2", data.userBillingAddress?.AddessLineTwo);
        setText("#addressLine3", data.userBillingAddress?.AddessLineThress);
        setText("#addressLocation", data.userBillingAddress?.Location);
        setText("#addressCity", city?.Name);
        setText("#addressState", state?.Name);
        setText("#addressCountry", country?.Name);
        setText("#addressPinCode", data.userBillingAddress?.PinCode);

        //DepositOrderInformation
        setText("#orderReference", data.depositOrder?.OrderReference);
        setText("#actualAmount", data.depositOrder?.ActualAmount);
        setText("#depositedAmount", data.depositOrder?.depositedAmount);
        setText("#pendingAmount", data.depositOrder?.pendingAmount);
 
    };

    self.showErrorState = function (message) {
        $(".se-pre-con").hide();
        $(".container-fluid").addClass("d-none");
        $("#errorState").removeClass("d-none");

        if (message) {
            document.getElementById("errorMessage").textContent = message;
            console.error(message);
        }
    };
}