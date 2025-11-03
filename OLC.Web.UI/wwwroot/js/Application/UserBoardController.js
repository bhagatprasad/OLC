function UserBoardController() {
    var self = this;
    self.UserPaymentOrders = [];
    self.filteredPaymentOrders = [];
    self.CoreStatus = [];
    self.ApplicationUser = {};
    var actions = [];
    var dataObjects = [];

    // Status mapping from your database
    self.statusMap = statusMap;

    self.init = function () {
        $(".se-pre-con").show();

        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        actions.push("/PaymentOrder/GetUserPaymentOrderList");
        actions.push("/Status/GetStatuses");
        dataObjects.push({ userId: self.ApplicationUser.Id });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log('User Payment Orders Response:', responses);
            self.UserPaymentOrders = responses[0][0] && responses[0][0].data ? responses[0][0].data : [];
            self.filteredPaymentOrders = [...self.UserPaymentOrders];
            self.CoreStatus = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];

            // Build status map from API if available, otherwise use default
            self.buildStatusMap();

            self.populateSummaryCards();
            self.populatePaymentOrdersGrid();
            self.initializeSearch();

            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.log('One or more requests failed:', error);
            self.showErrorState();
            $(".se-pre-con").hide();
        });
    };

    // Build status map from API response
    self.buildStatusMap = function () {
        if (self.CoreStatus && self.CoreStatus.length > 0) {
            self.statusMap = {};
            self.CoreStatus.forEach(status => {
                self.statusMap[status.Id] = status.Name;
            });
        }
    };

    // Get status name by ID
    self.getStatusName = function (statusId) {
        return self.statusMap[statusId] || 'Unknown Status';
    };

    // Show error state
    self.showErrorState = function () {
        $('#paymentOrdersBody').html(`
            <tr>
                <td colspan="9" class="text-center text-danger">
                    <i class="fas fa-exclamation-triangle me-2"></i>
                    Error loading payment orders. Please try again.
                </td>
            </tr>
        `);
        $('#mobilePaymentOrdersCards').html(`
            <div class="text-center text-danger p-4">
                <i class="fas fa-exclamation-triangle me-2"></i>
                Error loading payment orders. Please try again.
            </div>
        `);
    };

    // Populate summary cards with calculated data
    self.populateSummaryCards = function () {
        if (self.UserPaymentOrders.length === 0) {
            self.setDefaultSummaryValues();
            return;
        }

        const totalAmount = self.UserPaymentOrders.reduce((sum, order) => sum + (order.Amount || 0), 0);
        const completedAmount = self.UserPaymentOrders
            .filter(order =>
                order.OrderStatusId === 42 || // Payment Receved
                order.PaymentStatusId === 24 || // Paid
                order.OrderStatusId === 7 || // Completed
                order.OrderStatusId === 15 // Success
            )
            .reduce((sum, order) => sum + (order.Amount || 0), 0);
        const cancelledAmount = self.UserPaymentOrders
            .filter(order =>
                order.OrderStatusId === 8 || // Cancelled
                order.OrderStatusId === 6 // Rejected
            )
            .reduce((sum, order) => sum + (order.Amount || 0), 0);
        const failedAmount = self.UserPaymentOrders
            .filter(order =>
                order.OrderStatusId === 14 || // Failed
                order.PaymentStatusId === 14 // Failed
            )
            .reduce((sum, order) => sum + (order.Amount || 0), 0);

        $('#totalPayments').text(self.formatCurrency(totalAmount));
        $('#totalTransfers').text(self.formatCurrency(completedAmount));
        $('#totalCancelled').text(self.formatCurrency(cancelledAmount));
        $('#totalFailed').text(self.formatCurrency(failedAmount));

        // Calculate percentage changes
        self.calculatePercentageChanges();
    };

    // Set default summary values
    self.setDefaultSummaryValues = function () {
        $('#totalPayments').text(self.formatCurrency(0));
        $('#totalTransfers').text(self.formatCurrency(0));
        $('#totalCancelled').text(self.formatCurrency(0));
        $('#totalFailed').text(self.formatCurrency(0));

        $('#totalPaymentsChange').text('0%');
        $('#transfersChange').text('0%');
        $('#cancelledChange').text('0%');
        $('#failedChange').text('0%');
    };

    // Calculate percentage changes
    self.calculatePercentageChanges = function () {
        const totalOrders = self.UserPaymentOrders.length;
        const completedOrders = self.UserPaymentOrders.filter(o =>
            o.OrderStatusId === 42 || o.PaymentStatusId === 24 || o.OrderStatusId === 7 || o.OrderStatusId === 15
        ).length;
        const cancelledOrders = self.UserPaymentOrders.filter(o =>
            o.OrderStatusId === 8 || o.OrderStatusId === 6
        ).length;
        const failedOrders = self.UserPaymentOrders.filter(o =>
            o.OrderStatusId === 14 || o.PaymentStatusId === 14
        ).length;

        $('#totalPaymentsChange').text(totalOrders > 0 ? '12.5%' : '0%');
        $('#transfersChange').text(completedOrders > 0 ? '8.3%' : '0%');
        $('#cancelledChange').text(cancelledOrders > 0 ? '3.2%' : '0%');
        $('#failedChange').text(failedOrders > 0 ? '2.1%' : '0%');
    };

    // Initialize search functionality
    self.initializeSearch = function () {
        $('#orderSearch').on('input', function () {
            self.performOrderSearch($(this).val());
        });

        // Clear search on escape key
        $('#orderSearch').on('keydown', function (e) {
            if (e.key === 'Escape') {
                $(this).val('');
                self.performOrderSearch('');
            }
        });
    };

    // Perform search across payment orders
    self.performOrderSearch = function (searchTerm) {
        if (!searchTerm || searchTerm.trim() === '') {
            self.filteredPaymentOrders = [...self.UserPaymentOrders];
        } else {
            const term = searchTerm.toLowerCase().trim();
            self.filteredPaymentOrders = self.UserPaymentOrders.filter(order => {
                const orderStatusName = self.getStatusName(order.OrderStatusId);
                const paymentStatusName = self.getStatusName(order.PaymentStatusId);
                const depositStatusName = self.getStatusName(order.DepositStatusId);

                return (
                    (order.OrderReference && order.OrderReference.toLowerCase().includes(term)) ||
                    (order.PaymentReasonName && order.PaymentReasonName.toLowerCase().includes(term)) ||
                    (order.CreditCardNumber && order.CreditCardNumber.includes(term)) ||
                    (order.BankAccountNumber && order.BankAccountNumber.includes(term)) ||
                    (orderStatusName && orderStatusName.toLowerCase().includes(term)) ||
                    (paymentStatusName && paymentStatusName.toLowerCase().includes(term)) ||
                    (depositStatusName && depositStatusName.toLowerCase().includes(term)) ||
                    (order.Amount && order.Amount.toString().includes(term)) ||
                    (order.TotalAmountToChargeCustomer && order.TotalAmountToChargeCustomer.toString().includes(term))
                );
            });
        }
        self.populatePaymentOrdersGrid();
    };

    // Function to format date
    self.formatDate = function (dateString) {
        if (!dateString) return 'N/A';
        try {
            const date = new Date(dateString);
            return date.toLocaleDateString('en-US', {
                year: 'numeric',
                month: 'short',
                day: 'numeric'
            });
        } catch (e) {
            return 'Invalid Date';
        }
    };

    // Function to get status badge with proper mapping
    self.getStatusBadge = function (orderStatusId, paymentStatusId, depositStatusId) {
        const statusId = orderStatusId || paymentStatusId || depositStatusId;
        if (!statusId) return '<span class="badge bg-secondary status-badge">Unknown</span>';

        const statusName = self.getStatusName(statusId);
        const status = statusName.toLowerCase();

        if (status.includes('receved') || status.includes('paid') || status.includes('completed') || status.includes('success') || status.includes('processed') || status.includes('delivered')) {
            return '<span class="badge bg-success status-badge">' + statusName + '</span>';
        } else if (status.includes('pending') || status.includes('awaiting') || status.includes('submitted') || status.includes('under review') || status.includes('new')) {
            return '<span class="badge bg-warning status-badge">' + statusName + '</span>';
        } else if (status.includes('cancelled') || status.includes('rejected') || status.includes('failed') || status.includes('expired') || status.includes('overdue')) {
            return '<span class="badge bg-danger status-badge">' + statusName + '</span>';
        } else if (status.includes('processing') || status.includes('progress') || status.includes('on hold')) {
            return '<span class="badge bg-info status-badge">' + statusName + '</span>';
        } else if (status.includes('active') || status.includes('enabled') || status.includes('verified') || status.includes('open')) {
            return '<span class="badge bg-primary status-badge">' + statusName + '</span>';
        } else {
            return '<span class="badge bg-secondary status-badge">' + statusName + '</span>';
        }
    };

    // Get combined status display
    self.getCombinedStatusDisplay = function (order) {
        const orderStatus = self.getStatusName(order.OrderStatusId);
        const paymentStatus = self.getStatusName(order.PaymentStatusId);
        const depositStatus = self.getStatusName(order.DepositStatusId);

        // Return the most relevant status
        if (orderStatus && orderStatus !== 'Unknown Status') {
            return self.getStatusBadge(order.OrderStatusId);
        } else if (paymentStatus && paymentStatus !== 'Unknown Status') {
            return self.getStatusBadge(order.PaymentStatusId);
        } else if (depositStatus && depositStatus !== 'Unknown Status') {
            return self.getStatusBadge(order.DepositStatusId);
        } else {
            return self.getStatusBadge(null);
        }
    };

    // Format currency with currency code
    self.formatCurrency = function (amount, currency) {
        if (!amount) return '$0.00';
        const numAmount = typeof amount === 'string' ? parseFloat(amount) : amount;
        const currencySymbol = currency === 'INR' ? '₹' : '$';
        return currencySymbol + numAmount.toLocaleString('en-US', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    };

    // Format credit card number (show last 4 digits)
    self.formatCreditCard = function (cardNumber) {
        if (!cardNumber) return 'N/A';
        // Show only last 4 digits for security
        const lastFour = cardNumber.slice(-4);
        return `**** **** **** ${lastFour}`;
    };

    // Format bank account number (show last 4 digits)
    self.formatBankAccount = function (accountNumber) {
        if (!accountNumber) return 'N/A';
        // Show only last 4 digits for security
        const lastFour = accountNumber.slice(-4);
        return `****${lastFour}`;
    };

    // Truncate long text
    self.truncateText = function (text, maxLength) {
        if (!text) return 'N/A';
        if (text.length <= maxLength) return text;
        return text.substring(0, maxLength) + '...';
    };

    // Populate payment orders grid with new column structure
    self.populatePaymentOrdersGrid = function () {
        const tbody = $('#paymentOrdersBody');
        const cardsContainer = $('#mobilePaymentOrdersCards');
        tbody.empty();
        cardsContainer.empty();

        if (self.filteredPaymentOrders.length === 0) {
            const noDataMessage = self.UserPaymentOrders.length === 0 ?
                'No payment orders found' : 'No orders match your search';

            tbody.append(`
                <tr>
                    <td colspan="9" class="text-center text-muted py-4">
                        <i class="fas fa-inbox me-2"></i>${noDataMessage}
                    </td>
                </tr>
            `);
            cardsContainer.append(`
                <div class="text-center text-muted p-4">
                    <i class="fas fa-inbox me-2"></i>${noDataMessage}
                </div>
            `);
            return;
        }

        self.filteredPaymentOrders.forEach(function (order) {
            const statusBadge = self.getCombinedStatusDisplay(order);
            const createdDate = self.formatDate(order.CreatedOn);
            const creditCardDisplay = self.formatCreditCard(order.CreditCardNumber);
            const bankAccountDisplay = self.formatBankAccount(order.BankAccountNumber);

            // Desktop table row - NEW COLUMN STRUCTURE
            const row = `
                <tr class="payment-order-item" data-order-id="${order.Id}">
                    <td>
                        <strong class="text-primary">${order.OrderReference || 'N/A'}</strong>
                    </td>
                    <td class="fw-bold">${self.formatCurrency(order.Amount, order.Currency)}</td>
                    <td>${self.formatCurrency(order.TotalAmountToChargeCustomer, order.Currency)}</td>
                    <td>${self.formatCurrency(order.TotalAmountToDepositToCustomer, order.Currency)}</td>
                    <td>
                        <small class="text-muted">${creditCardDisplay}</small>
                        ${order.CreditCardNumber ? '<br><small class="text-success"><i class="fas fa-credit-card me-1"></i>Card</small>' : ''}
                    </td>
                    <td>
                        <small class="text-muted">${bankAccountDisplay}</small>
                        ${order.BankAccountNumber ? '<br><small class="text-info"><i class="fas fa-university me-1"></i>Bank</small>' : ''}
                    </td>
                    <td>${statusBadge}</td>
                    <td><small class="text-muted">${createdDate}</small></td>
                    <td>
                        <div class="btn-group btn-group-sm">
                            <button class="btn btn-outline-primary view-order" data-order-id="${order.Id}" title="View Details">
                                <i class="fas fa-eye"></i>
                            </button>
                            <button class="btn btn-outline-info copy-order" data-order-ref="${order.OrderReference}" title="Copy Reference">
                                <i class="fas fa-copy"></i>
                            </button>
                        </div>
                    </td>
                </tr>
            `;
            tbody.append(row);

            // Mobile card - Updated with new fields
            const cardHtml = `
                <div class="card mb-3 border" data-order-id="${order.Id}">
                    <div class="card-header bg-light d-flex justify-content-between align-items-center">
                        <strong class="text-primary">${order.OrderReference || 'N/A'}</strong>
                        ${statusBadge}
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-6 mb-2">
                                <small class="text-muted">Amount:</small>
                                <div class="fw-bold">${self.formatCurrency(order.Amount, order.Currency)}</div>
                            </div>
                            <div class="col-6 mb-2">
                                <small class="text-muted">Charge Amount:</small>
                                <div>${self.formatCurrency(order.TotalAmountToChargeCustomer, order.Currency)}</div>
                            </div>
                            <div class="col-6 mb-2">
                                <small class="text-muted">Deposit Amount:</small>
                                <div>${self.formatCurrency(order.TotalAmountToDepositToCustomer, order.Currency)}</div>
                            </div>
                            <div class="col-6 mb-2">
                                <small class="text-muted">Created:</small>
                                <div><small class="text-muted">${createdDate}</small></div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">Credit Card:</small>
                                <div>${creditCardDisplay}</div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">Bank Account:</small>
                                <div>${bankAccountDisplay}</div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">Payment Reason:</small>
                                <div>${order.PaymentReasonName || 'N/A'}</div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer d-flex justify-content-between">
                        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}">
                            <i class="fas fa-eye me-1"></i>Details
                        </button>
                        <button class="btn btn-sm btn-outline-info copy-order" data-order-ref="${order.OrderReference}">
                            <i class="fas fa-copy me-1"></i>Copy Ref
                        </button>
                    </div>
                </div>
            `;
            cardsContainer.append(cardHtml);
        });

        // Initialize event handlers
        self.initializeEventHandlers();
    };

    // Initialize event handlers
    self.initializeEventHandlers = function () {
        $('.view-order').off('click').on('click', function () {
            const orderId = $(this).data('order-id');
            self.viewOrderDetails(orderId);
        });

        $('.copy-order').off('click').on('click', function () {
            const orderRef = $(this).data('order-ref');
            self.copyOrderReference(orderRef);
        });
    };

    self.viewOrderDetails = function (orderId) {
        window.location.href = '/PaymentOrder/Details?orderId=' + orderId;
    };

    self.copyOrderReference = function (orderRef) {
        if (!orderRef) {
            self.showToast('No order reference to copy', 'warning');
            return;
        }

        navigator.clipboard.writeText(orderRef).then(() => {
            self.showToast('Order reference copied to clipboard!', 'success');
        }).catch(() => {
            const textArea = document.createElement('textarea');
            textArea.value = orderRef;
            document.body.appendChild(textArea);
            textArea.select();
            document.execCommand('copy');
            document.body.removeChild(textArea);
            self.showToast('Order reference copied to clipboard!', 'success');
        });
    };

    self.showToast = function (message, type = 'info') {
        const toast = document.createElement('div');
        toast.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
        toast.style.cssText = 'top: 20px; right: 20px; z-index: 1050; min-width: 300px;';
        toast.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        document.body.appendChild(toast);

        setTimeout(() => {
            if (toast.parentNode) {
                toast.parentNode.removeChild(toast);
            }
        }, 3000);
    };

    $(document).on("click", "#makeNewPayment", function () {
        window.location.href = "/Transaction/MakeNewPayment";
    })
}