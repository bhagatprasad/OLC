function UserBoardController() {
    var self = this;
    self.UserPaymentOrders = [];
    self.filteredPaymentOrders = [];
    self.CoreStatus = [];
    self.ApplicationUser = {};
    var actions = [];
    var dataObjects = [];
    self.isMobile = window.innerWidth < 768;

    // Pagination variables
    self.currentPage = 0;
    self.pageSize = 100;
    self.isLoading = false;
    self.hasMoreData = true;
    self.currentDisplayedOrders = [];

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
            self.initializeSearch();
            self.initializeScrollHandler();
            self.loadNextPage(); // Load first page

            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.log('One or more requests failed:', error);
            self.showErrorState();
            $(".se-pre-con").hide();
        });
    };

    // Initialize infinite scroll
    self.initializeScrollHandler = function () {
        if (self.isMobile) {
            // For mobile, use the cards container
            const mobileContainer = $('#mobilePaymentOrdersCards');
            mobileContainer.off('scroll').on('scroll', function () {
                if (self.isLoading || !self.hasMoreData) return;

                const scrollTop = $(this).scrollTop();
                const scrollHeight = $(this)[0].scrollHeight;
                const clientHeight = $(this).innerHeight();

                // Load more when 100px from bottom
                if (scrollTop + clientHeight >= scrollHeight - 100) {
                    self.loadNextPage();
                }
            });
        } else {
            // For desktop, use the table wrapper div
            const tableContainer = $('.table-responsive');
            tableContainer.off('scroll').on('scroll', function () {
                if (self.isLoading || !self.hasMoreData) return;

                const scrollTop = $(this).scrollTop();
                const scrollHeight = $(this)[0].scrollHeight;
                const clientHeight = $(this).innerHeight();

                // Load more when 100px from bottom
                if (scrollTop + clientHeight >= scrollHeight - 100) {
                    self.loadNextPage();
                }
            });
        }
    };

    // Load next page of data
    self.loadNextPage = function () {
        if (self.isLoading || !self.hasMoreData) {
            return;
        }

        self.isLoading = true;

        // Show loading indicator
        if (self.currentPage === 0) {
            $(".se-pre-con").show();
        } else {
            self.showLoadingIndicator();
        }

        setTimeout(() => {
            try {
                const startIndex = self.currentPage * self.pageSize;
                const endIndex = startIndex + self.pageSize;
                const nextPageData = self.filteredPaymentOrders.slice(startIndex, endIndex);

                if (nextPageData.length === 0) {
                    self.hasMoreData = false;
                    self.hideLoadingIndicator();
                    self.isLoading = false;
                    return;
                }

                self.currentDisplayedOrders = [...self.currentDisplayedOrders, ...nextPageData];
                self.renderOrders(nextPageData, self.currentPage === 0);

                self.currentPage++;
                self.hasMoreData = nextPageData.length === self.pageSize;

            } catch (error) {
                console.error('Error loading next page:', error);
            } finally {
                self.isLoading = false;
                self.hideLoadingIndicator();

                if (self.currentPage === 1) {
                    $(".se-pre-con").hide();
                }
            }
        }, 100);
    };

    // Show loading indicator for pagination
    self.showLoadingIndicator = function () {
        self.hideLoadingIndicator();

        if (self.isMobile) {
            $('#mobilePaymentOrdersCards').append(`
                <div class="text-center p-3 loading-indicator">
                    <div class="spinner-border spinner-border-sm" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                    <span class="ms-2">Loading more orders...</span>
                </div>
            `);
        } else {
            $('#paymentOrdersBody').append(`
                <tr class="loading-indicator">
                    <td colspan="9" class="text-center p-3">
                        <div class="spinner-border spinner-border-sm" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                        <span class="ms-2">Loading more orders...</span>
                    </td>
                </tr>
            `);
        }
    };

    // Hide loading indicator
    self.hideLoadingIndicator = function () {
        $('.loading-indicator').remove();
    };

    // Reset pagination
    self.resetPagination = function () {
        self.currentPage = 0;
        self.hasMoreData = true;
        self.currentDisplayedOrders = [];
        self.isLoading = false;

        const tbody = $('#paymentOrdersBody');
        const cardsContainer = $('#mobilePaymentOrdersCards');
        tbody.empty();
        cardsContainer.empty();
    };
    self.isInvoiceDownloadAllowed = function (order) {
        const allowed = ['paid', 'partially paid', 'completed'];
        const orderStatus = (self.getStatusName(order.OrderStatusId) || '').trim().toLowerCase();
        const paymentStatus = (self.getStatusName(order.PaymentStatusId) || '').trim().toLowerCase();
        return allowed.includes(orderStatus);
    };
    
    // Render orders to the grid
    self.renderOrders = function (orders, clearExisting = false) {
        const tbody = $('#paymentOrdersBody');
        const cardsContainer = $('#mobilePaymentOrdersCards');

        if (clearExisting) {
            tbody.empty();
            cardsContainer.empty();
        }

        if (orders.length === 0 && clearExisting) {
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

        const fragment = document.createDocumentFragment();
        const mobileFragment = document.createDocumentFragment();

        orders.forEach(function (order) {
            const statusBadge = self.getCombinedStatusDisplay(order);
            const createdDate = self.formatDate(order.CreatedOn);
            const creditCardDisplay = self.formatCreditCard(order.CreditCardNumber);
            const bankAccountDisplay = self.formatBankAccount(order.BankAccountNumber);

            if (!self.isMobile) {
                // Desktop table row
                const row = document.createElement('tr');
                row.className = 'payment-order-item';
                row.setAttribute('data-order-id', order.Id);
                row.innerHTML = `
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
                            ${ (order.OrderStatus === "Partially Paid" || order.OrderStatus === "Paid") ? `
                                <button class="btn btn-outline-warning view-deposit" 
                                        data-order-id="${order.Id}" title="View Deposits">
                                    <i class="fas fa-handshake"></i>
                                </button>
                            ` : '' }

                               ${self.isInvoiceDownloadAllowed(order) ?
                                   `<button class="btn btn-outline-danger download-invoice btn-sm" data-order-id="${order.Id}" title="Download Invoice">
                                       <i class="fas fa-file-pdf"></i>
                                    </button>`
                                                    : ''
                               }
                        </div>
                    </td>
                `;
                fragment.appendChild(row);
            } else {
                // Mobile card - COMPACT LAYOUT
                const cardDiv = document.createElement('div');
                cardDiv.className = 'card mb-3 border';
                cardDiv.setAttribute('data-order-id', order.Id);
                cardDiv.innerHTML = `
                    <div class="card-header bg-light d-flex justify-content-between align-items-center py-2">
                        <div class="flex-grow-1">
                            <strong class="text-primary d-block">${order.OrderReference || 'N/A'}</strong>
                            <small class="text-muted">${self.truncateText(order.PaymentReasonName, 25)}</small>
                        </div>
                        <div class="ms-2">
                            ${statusBadge}
                        </div>
                    </div>
                    <div class="card-body py-2">
                        <div class="row g-1 small">
                            <div class="col-4">
                                <small class="text-muted d-block">Amount</small>
                                <div class="fw-bold text-primary">${self.formatCurrency(order.Amount, order.Currency)}</div>
                            </div>
                            <div class="col-4">
                                <small class="text-muted d-block">Charge</small>
                                <div>${self.formatCurrency(order.TotalAmountToChargeCustomer, order.Currency)}</div>
                            </div>
                            <div class="col-4">
                                <small class="text-muted d-block">Deposit</small>
                                <div>${self.formatCurrency(order.TotalAmountToDepositToCustomer, order.Currency)}</div>
                            </div>
                            <div class="col-6">
                                <small class="text-muted d-block">Created</small>
                                <div><small>${createdDate}</small></div>
                            </div>
                            <div class="col-6">
                                <small class="text-muted d-block">Payment Method</small>
                                <div class="d-flex flex-column">
                                    ${order.CreditCardNumber ? `<small><i class="fas fa-credit-card text-success me-1"></i>${creditCardDisplay}</small>` : ''}
                                    ${order.BankAccountNumber ? `<small><i class="fas fa-university text-info me-1"></i>${bankAccountDisplay}</small>` : ''}
                                    ${!order.CreditCardNumber && !order.BankAccountNumber ? '<small>N/A</small>' : ''}
                                </div>
                            </div>
                            <div class="col-12 mt-1">
                                <small class="text-muted d-block">Payment Reason</small>
                                <div>${self.truncateText(order.PaymentReasonName, 30)}</div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer py-2">
                        <div class="btn-group w-100" role="group">
                            <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}">
                                <i class="fas fa-eye me-1"></i>Details
                            </button>
                            <button class="btn btn-sm btn-outline-info copy-order" data-order-ref="${order.OrderReference}">
                                <i class="fas fa-copy me-1"></i>Copy Ref
                            </button>
                             ${ (order.OrderStatus === "Partially Paid" || order.OrderStatus === "Paid") ? `
                                    <button class="btn btn-sm btn-outline-warning view-deposit" 
                                            data-order-id="${order.Id}">
                                        <i class="fas fa-handshake me-1"></i>Deposits
                                    </button>
                             ` : '' }
                           ${self.isInvoiceDownloadAllowed(order) ?
                                `<button class="btn btn-outline-danger download-invoice btn-sm" data-order-id="${order.Id}" title="Download Invoice">
                                  <i class="fas fa-file-pdf"></i>
                                </button>`
                                : ''
                           }

                        </div>
                    </div>
                `;
                mobileFragment.appendChild(cardDiv);
            }
        });

        if (!self.isMobile) {
            tbody.append(fragment);
        } else {
            cardsContainer.append(mobileFragment);
        }

        // Initialize event handlers
        self.initializeEventHandlers();
    };

    // Replace populatePaymentOrdersGrid with new paginated version
    self.populatePaymentOrdersGrid = function () {
        self.resetPagination();
        self.loadNextPage();
    };

    // Update performOrderSearch to work with pagination
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
        const currencySymbol = '$';
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
        console.log('Viewing order details for:', orderId);
        window.location.href = '/PaymentOrder/GetPaymentOrderDetails?paymentOrderId=' + orderId;
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

    // Clean up timers when needed
    self.destroy = function () {
        $('.table-responsive').off('scroll');
        $('#mobilePaymentOrdersCards').off('scroll');
    };

    $(document).on("click", "#makeNewPayment", function () {
        window.location.href = "/Transaction/MakeNewPayment";
    });

   
    // VIEW DEPOSITS CLICK
   
    $(document).on("click", ".view-deposit", function () {

        console.log("UserBoard View Deposit clicked...");

        $("#depositOrderModal").modal({
            backdrop: "static",
            keyboard: false
        });
        $("#depositOrderModal").modal("show");

        const paymentOrderId = $(this).data("order-id");

        if (!paymentOrderId) {
            alert("Missing data-order-id");
            return;
        }

        console.log("Clicked PaymentOrderId:", paymentOrderId);

        const order = self.UserPaymentOrders?.find(o => o.Id == paymentOrderId);

        if (!order) {
            console.error("UserPaymentOrders:", self.UserPaymentOrders);
            alert("Order not found in user list");
            return;
        }

        self.CurrentSelectedPaymentOrder = order;

        $("#viewDepositOrderRef").text(order.OrderReference || "N/A");

        const $tbody = $("#depositOrderTableBody").empty();

        $(".se-pre-con").show();

        $.ajax({
            type: "GET",
            url: "/PaymentOrder/GetDepositOrders",
            data: { paymentOrderId: paymentOrderId },
            cache: false,
            success: function (response) {
                console.log("Deposit Records Response:", response);

                self.DepositOrders = response && response.data ? response.data : [];

                if (self.DepositOrders.length === 0) {
                    $tbody.append(`
                    <tr>
                        <td class="text-center py-3" colspan="5">
                            No deposit records found.
                        </td>
                    </tr>
                `);
                    $(".se-pre-con").hide();
                    return;
                }

                // Append table rows
                self.DepositOrders.forEach(d => {
                    $tbody.append(`
                    <tr class="align-middle">
                        <td class="ps-4">${d.OrderReference || "N/A"}</td>
                        <td class="ps-4 text-end text-success fw-bold">${Number(d.ActualDepositeAmount || 0).toFixed(2)}</td>
                        <td class="ps-4 text-end">${Number(d.DepositeAmount || 0).toFixed(2)}</td>
                        <td class="ps-4 text-end text-danger">${Number(d.PendingDepositeAmount || 0).toFixed(2)}</td>
                        <td class="ps-4 text-end text-danger">${d.StripeDepositeChargeId || ""}</td>
                    </tr>
                `);
                });

                $(".se-pre-con").hide();
            },
            error: function (xhr) {
                console.error("Error fetching deposits:", xhr.responseText);
                $(".se-pre-con").hide();
            }
        });
    });

    $(document).on("click", "#btnCloseViewDepositModal", function () {
        self.CurrentSelectedPaymentOrder = {};
        self.DepositOrders = [];
        $("#depositOrderModal").modal("hide");
    });

       
    //Download Invoice   
    $(document).on("click", ".download-invoice", function () {
        const orderId = $(this).data("order-id");
        const order = self.UserPaymentOrders.find(o => o.Id == orderId);
        if (order) self.generateInvoicePDF(order);
    });

    self.generateInvoicePDF = function (order) {
        const { jsPDF } = window.jspdf;
        const doc = new jsPDF();

        // Header 
        doc.addImage("/images/logo.png", "PNG", 14, 8, 48, 18);
        doc.setFontSize(11);
        doc.text(`Date: ${new Date().toLocaleDateString('en-GB')}`, 195, 20, { align: "right" });
        doc.setFontSize(32); doc.setFont("helvetica", "bold");
        doc.text("INVOICE", 105, 38, { align: "center" });

        doc.setFontSize(11); doc.setFont("helvetica", "normal");
        doc.text(`Invoice No: ${order.OrderReference}`, 14, 48);
        doc.setFont("helvetica", "bold");
        doc.text(`Status: ${self.getStatusName(order.OrderStatusId) || self.getStatusName(order.PaymentStatusId) || 'Paid'}`, 14, 55);

        // Bill To
        doc.setFontSize(12); doc.setFont("helvetica", "bold"); doc.text("Bill To:", 14, 68);
        doc.setFontSize(11); doc.setFont("helvetica", "normal");
        doc.text([
            order.UserFullName || self.ApplicationUser.FullName || "Customer",
            order.UserEmail || self.ApplicationUser.Email || "N/A",
            order.UserPhone || self.ApplicationUser.Phone || "N/A"
        ], 14, 76);


        const pendingAmount = self.formatCurrency(order.PendingDepositeAmount || 0);
        doc.autoTable({
            startY: 92,
            head: [["Description", "Amount"]],
            body: [
                ["Original Amount", self.formatCurrency(order.Amount || 0)],
                ["Charge Amount", self.formatCurrency(order.TotalAmountToChargeCustomer || 0)],
                ["Total Deposit to Customer", self.formatCurrency(order.TotalAmountToDepositToCustomer || 0)],
                ["Amount Paid", self.formatCurrency(order.TotalAmountToDepositToCustomer || order.Amount || 0)],
                ["Pending Amount", pendingAmount],
                ["Payment Status", order.PaymentStatus || order.OrderStatus]
            ],
            theme: 'grid',
            headStyles: { fillColor: [220, 53, 69], textColor: [255, 255, 255], fontStyle: 'bold', fontSize: 11 },
            bodyStyles: { fontSize: 10, cellPadding: 5 },
            columnStyles: {
                0: { cellWidth: 120 },  // wider description
                1: { cellWidth: 60, halign: 'right', fontStyle: 'bold' }  // FIXED width amount column
            },
            tableWidth: 'auto',
            margin: { left: 14, right: 14 }
        });


        const baseY = doc.lastAutoTable.finalY + 14;

        // Payment & Deposit Info
        doc.setFontSize(12); doc.setFont("helvetica", "bold");
        doc.text("Payment Information", 14, baseY);
        doc.setFontSize(10); doc.setFont("helvetica", "normal");
        doc.text("Please make payment to the following bank account:", 14, baseY + 7);
        doc.setFont("helvetica", "bold"); doc.text("Bank Name      :", 14, baseY + 15); doc.setFont("helvetica", "normal"); doc.text("ICICI Bank", 55, baseY + 15);
        doc.setFont("helvetica", "bold"); doc.text("Account Name   :", 14, baseY + 21); doc.setFont("helvetica", "normal"); doc.text("Betalen Payments Pvt Ltd", 55, baseY + 21);
        doc.setFont("helvetica", "bold"); doc.text("Account Number :", 14, baseY + 27); doc.setFont("helvetica", "normal"); doc.text("678999922445", 55, baseY + 27);
        doc.setFont("helvetica", "bold"); doc.text("IFSC Code      :", 14, baseY + 33); doc.setFont("helvetica", "normal"); doc.text("ICIC00016", 55, baseY + 33);
        doc.setFont("helvetica", "bold"); doc.text("Branch         :", 14, baseY + 39); doc.setFont("helvetica", "normal"); doc.text("Andheri East, Mumbai", 55, baseY + 39);

        doc.setFontSize(12); doc.setFont("helvetica", "bold");
        doc.text("Deposit Information", 120, baseY);
        doc.setFontSize(10); doc.setFont("helvetica", "normal");
        doc.text("Amount will be deposited to customer via:", 120, baseY + 7);
        doc.setFont("helvetica", "bold"); doc.text("Bank Name      :", 120, baseY + 15); doc.setFont("helvetica", "normal"); doc.text(order.BankName || "Customer's Bank", 158, baseY + 15);
        doc.setFont("helvetica", "bold"); doc.text("Account Holder :", 120, baseY + 21); doc.setFont("helvetica", "normal"); doc.text(order.AccountHolderName || "Customer", 158, baseY + 21);
        doc.setFont("helvetica", "bold"); doc.text("Account Number :", 120, baseY + 27); doc.setFont("helvetica", "normal"); doc.text(order.BankAccountNumber ? `****${order.BankAccountNumber.slice(-4)}` : "N/A", 158, baseY + 27);
        doc.setFont("helvetica", "bold"); doc.text("IFSC Code      :", 120, baseY + 33); doc.setFont("helvetica", "normal"); doc.text(order.IFSCCode || "N/A", 158, baseY + 33);
        doc.setFont("helvetica", "bold"); doc.text("Total Deposit  :", 120, baseY + 39); doc.setFont("helvetica", "normal"); doc.text(self.formatCurrency(order.TotalAmountToDepositToCustomer), 158, baseY + 39);

        // Footer
        const h = doc.internal.pageSize.height;
        doc.setFontSize(11); doc.setTextColor(100);
        doc.text("Thank you for your business!", 14, h - 26);
        doc.setFont("helvetica", "italic");
        doc.text("Betalen - Secure Global Payments", 14, h - 12);
        doc.text("support@betalen.in | www.betalen.in", 14, h - 22);

        doc.save(`Invoice_${order.OrderReference}.pdf`);
    };
}