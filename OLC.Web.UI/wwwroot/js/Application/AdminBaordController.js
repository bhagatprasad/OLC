function AdminBaordController() {
    var self = this;
    self.ExecutivePaymentOrders = [];
    self.filteredPaymentOrders = [];
    self.CoreStatus = [];
    self.ApplicationUser = {};
    var actions = [];
    self.slaTimers = {}; // To store interval IDs for SLA timers
    self.isMobile = window.innerWidth < 768;

    // Pagination variables
    self.currentPage = 0;
    self.pageSize = 100;
    self.isLoading = false;
    self.hasMoreData = true;
    self.currentDisplayedOrders = [];

    self.init = function () {
        $(".se-pre-con").show();

        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        actions.push("/PaymentOrder/GetExecutivePaymentOrders");
        actions.push("/Status/GetStatuses");

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);
            self.ExecutivePaymentOrders = responses[0][0] && responses[0][0].data ? responses[0][0].data : [];
            self.filteredPaymentOrders = [...self.ExecutivePaymentOrders];
            self.CoreStatus = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];

            self.populateSummaryCards();
            self.initializeSearch();
            self.initializeScrollHandler();
            self.loadNextPage(); // Load first page

            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.log('One or more requests failed:', error);
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
                    <td colspan="10" class="text-center p-3">
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

    // Render orders to the grid
    self.renderOrders = function (orders, clearExisting = false) {
        const tbody = $('#paymentOrdersBody');
        const cardsContainer = $('#mobilePaymentOrdersCards');

        if (clearExisting) {
            tbody.empty();
            cardsContainer.empty();
        }

        if (orders.length === 0 && clearExisting) {
            const noDataMessage = self.ExecutivePaymentOrders.length === 0 ? 'No payment orders found' : 'No orders match your search';
            tbody.append(`
                <tr>
                    <td colspan="10" class="text-center text-muted">
                        ${noDataMessage}
                    </td>
                </tr>
            `);
            cardsContainer.append(`
                <div class="text-center text-muted p-4">
                    ${noDataMessage}
                </div>
            `);
            return;
        }

        const fragment = document.createDocumentFragment();
        const mobileFragment = document.createDocumentFragment();

        orders.forEach(function (order) {
            const statusBadge = self.getStatusBadge(order.OrderStatus);
            const createdDate = self.formatDate(order.CreatedOn);
            const creditCardDisplay = self.formatCreditCard(order.CreditCardNumber);
            const bankAccountDisplay = self.formatBankAccount(order.BankAccountNumber);
            const slaInfo = self.calculateSLATimer(order);

            if (!self.isMobile) {
                // Desktop table row
                const row = document.createElement('tr');
                row.className = 'payment-order-item';
                row.setAttribute('data-order-id', order.Id);
                row.innerHTML = `
                    <td>
                        <strong class="text-primary">${order.OrderReference || 'N/A'}</strong>
                    </td>
                    <td class="fw-bold">${self.formatCurrency(order.Amount)}</td>
                    <td>${self.formatCurrency(order.TotalAmountToChargeCustomer)}</td>
                    <td>${self.formatCurrency(order.TotalAmountToDepositToCustomer)}</td>
                    <td>
                        <div class="small">
                            <div><i class="fas fa-envelope me-1 text-muted"></i>${order.UserEmail || 'N/A'}</div>
                            <div><i class="fas fa-phone me-1 text-muted"></i>${order.UserPhone || 'N/A'}</div>
                        </div>
                    </td>
                    <td>
                        <div class="small">
                            <div><i class="fas fa-credit-card me-1 text-success"></i>${creditCardDisplay}</div>
                            <div><i class="fas fa-university me-1 text-info"></i>${bankAccountDisplay}</div>
                        </div>
                    </td>
                    <td>${statusBadge}</td>
                    <td>
                        <span class="sla-timer ${slaInfo.class}">${slaInfo.display}</span>
                    </td>
                    <td><small class="text-muted">${createdDate}</small></td>
                    <td>
                        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Order Details">
                            <i class="fas fa-eye"></i>
                        </button>
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
                                <div class="fw-bold text-primary">${self.formatCurrency(order.Amount)}</div>
                            </div>
                            <div class="col-4">
                                <small class="text-muted d-block">Charge</small>
                                <div>${self.formatCurrency(order.TotalAmountToChargeCustomer)}</div>
                            </div>
                            <div class="col-4">
                                <small class="text-muted d-block">Deposit</small>
                                <div>${self.formatCurrency(order.TotalAmountToDepositToCustomer)}</div>
                            </div>
                            <div class="col-6">
                                <small class="text-muted d-block">SLA</small>
                                <div><span class="sla-timer ${slaInfo.class}">${slaInfo.display}</span></div>
                            </div>
                            <div class="col-6">
                                <small class="text-muted d-block">Created</small>
                                <div><small>${createdDate}</small></div>
                            </div>
                            <div class="col-12 mt-1">
                                <small class="text-muted d-block">User</small>
                                <div class="d-flex justify-content-between">
                                    <span><i class="fas fa-envelope text-muted me-1"></i>${self.truncateText(order.UserEmail, 20)}</span>
                                    <span><i class="fas fa-phone text-muted me-1"></i>${order.UserPhone || 'N/A'}</span>
                                </div>
                            </div>
                            <div class="col-12 mt-1">
                                <small class="text-muted d-block">Payment Methods</small>
                                <div class="d-flex justify-content-between">
                                    <span><i class="fas fa-credit-card text-success me-1"></i>${creditCardDisplay}</span>
                                    <span><i class="fas fa-university text-info me-1"></i>${bankAccountDisplay}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer py-2">
                        <button class="btn btn-sm btn-outline-primary w-100 view-order" data-order-id="${order.Id}">
                            <i class="fas fa-eye me-2"></i>View Details
                        </button>
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

        // Initialize view order click handlers for new elements
        self.initializeViewOrderHandlers();

        // Start SLA timers for new orders
        self.startSLATimers();
    };

    // Replace populatePaymentOrdersGrid with new paginated version
    self.populatePaymentOrdersGrid = function () {
        self.resetPagination();
        self.loadNextPage();
    };

    // Update performOrderSearch to work with pagination
    self.performOrderSearch = function (searchTerm) {
        if (!searchTerm || searchTerm.trim() === '') {
            self.filteredPaymentOrders = [...self.ExecutivePaymentOrders];
        } else {
            const term = searchTerm.toLowerCase().trim();
            self.filteredPaymentOrders = self.ExecutivePaymentOrders.filter(order =>
                (order.OrderReference && order.OrderReference.toLowerCase().includes(term)) ||
                (order.UserEmail && order.UserEmail.toLowerCase().includes(term)) ||
                (order.UserPhone && order.UserPhone.includes(term)) ||
                (order.CreditCardNumber && order.CreditCardNumber.includes(term)) ||
                (order.BankAccountNumber && order.BankAccountNumber.includes(term)) ||
                (order.OrderStatus && order.OrderStatus.toLowerCase().includes(term)) ||
                (order.Amount && order.Amount.toString().includes(term)) ||
                (order.TotalAmountToChargeCustomer && order.TotalAmountToChargeCustomer.toString().includes(term)) ||
                (order.TotalAmountToDepositToCustomer && order.TotalAmountToDepositToCustomer.toString().includes(term))
            );
        }
        self.populatePaymentOrdersGrid();
    };

    // Populate summary cards with calculated data
    self.populateSummaryCards = function () {
        if (self.ExecutivePaymentOrders.length === 0) return;

        const totalAmount = self.ExecutivePaymentOrders.reduce((sum, order) => sum + (order.Amount || 0), 0);
        const completedAmount = self.ExecutivePaymentOrders
            .filter(order => order.OrderStatus === 'Completed')
            .reduce((sum, order) => sum + (order.Amount || 0), 0);
        const cancelledAmount = self.ExecutivePaymentOrders
            .filter(order => order.OrderStatus === 'Cancelled')
            .reduce((sum, order) => sum + (order.Amount || 0), 0);
        const failedAmount = self.ExecutivePaymentOrders
            .filter(order => order.OrderStatus === 'Failed')
            .reduce((sum, order) => sum + (order.Amount || 0), 0);

        $('#totalPayments').text('$' + totalAmount.toLocaleString());
        $('#totalTransfers').text('$' + completedAmount.toLocaleString());
        $('#totalCancelled').text('$' + cancelledAmount.toLocaleString());
        $('#totalFailed').text('$' + failedAmount.toLocaleString());
    };

    // Initialize search functionality
    self.initializeSearch = function () {
        $('#orderSearch').on('input', function () {
            self.performOrderSearch($(this).val());
        });
    };

    // Function to format date
    self.formatDate = function (dateString) {
        if (!dateString) return 'N/A';
        const date = new Date(dateString);
        return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    };

    // Function to get status badge
    self.getStatusBadge = function (orderStatus) {
        if (!orderStatus) return '<span class="badge bg-secondary status-badge">Unknown</span>';

        const status = orderStatus.toLowerCase();
        if (status.includes('completed') || status.includes('success')) {
            return '<span class="badge bg-success status-badge">' + orderStatus + '</span>';
        } else if (status.includes('pending') || status.includes('processing')) {
            return '<span class="badge bg-warning status-badge">' + orderStatus + '</span>';
        } else if (status.includes('cancelled') || status.includes('canceled')) {
            return '<span class="badge bg-danger status-badge">' + orderStatus + '</span>';
        } else if (status.includes('failed')) {
            return '<span class="badge bg-danger status-badge">' + orderStatus + '</span>';
        } else {
            return '<span class="badge bg-secondary status-badge">' + orderStatus + '</span>';
        }
    };

    // Format currency
    self.formatCurrency = function (amount) {
        if (!amount) return '$0.00';
        return '$' + parseFloat(amount).toLocaleString('en-US', {
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

    // Truncate long text for better display
    self.truncateText = function (text, maxLength) {
        if (!text) return 'N/A';
        if (text.length <= maxLength) return text;
        return text.substring(0, maxLength) + '...';
    };

    // Calculate SLA timer
    self.calculateSLATimer = function (order) {
        if (!order.CreatedOn) {
            return { display: 'N/A', class: 'sla-normal' };
        }

        const createdDate = new Date(order.CreatedOn);
        const now = new Date();
        const diffMs = now - createdDate;
        const diffHours = diffMs / (1000 * 60 * 60);

        let display, timerClass;

        if (order.OrderStatus === 'Completed') {
            display = 'Completed';
            timerClass = 'sla-completed';
        } else if (diffHours <= 1) {
            // Less than 1 hour - show minutes
            const minutes = Math.floor(diffMs / (1000 * 60));
            display = `${minutes}m`;
            timerClass = 'sla-normal';
        } else if (diffHours <= 4) {
            // 1-4 hours - show hours and minutes
            const hours = Math.floor(diffHours);
            const minutes = Math.floor((diffHours - hours) * 60);
            display = `${hours}h ${minutes}m`;
            timerClass = diffHours > 2 ? 'sla-warning' : 'sla-normal';
        } else {
            // More than 4 hours - critical
            const hours = Math.floor(diffHours);
            display = `${hours}h`;
            timerClass = 'sla-critical';
        }

        return {
            display: display,
            class: timerClass
        };
    };

    // Update SLA timer for a specific order
    self.updateSLATimer = function (orderId) {
        const order = self.ExecutivePaymentOrders.find(o => o.Id === orderId);
        if (!order) return;

        const slaInfo = self.calculateSLATimer(order);

        // Update desktop table
        $(`tr[data-order-id="${orderId}"] .sla-timer`).text(slaInfo.display)
            .removeClass('sla-normal sla-warning sla-critical sla-completed')
            .addClass(slaInfo.class);

        // Update mobile card
        $(`.card[data-order-id="${orderId}"] .sla-timer`).text(slaInfo.display)
            .removeClass('sla-normal sla-warning sla-critical sla-completed')
            .addClass(slaInfo.class);
    };

    // Start SLA timers for all orders
    self.startSLATimers = function () {
        // Clear any existing timers
        Object.values(self.slaTimers).forEach(timerId => clearInterval(timerId));
        self.slaTimers = {};

        // Start timers for each order that's not completed
        self.currentDisplayedOrders.forEach(order => {
            if (order.OrderStatus !== 'Completed') {
                self.slaTimers[order.Id] = setInterval(() => {
                    self.updateSLATimer(order.Id);
                }, 1000);
            }
        });
    };

    // Initialize view order click handlers
    self.initializeViewOrderHandlers = function () {
        $('.view-order').off('click').on('click', function () {
            const orderId = $(this).data('order-id');
            self.viewOrderDetails(orderId);
        });
    };

    // View order details
    self.viewOrderDetails = function (orderId) {
        console.log('Viewing order details for:', orderId);
        // Implement your view order details logic here
        // Example: window.location.href = '/PaymentOrder/Details?id=' + orderId;
        window.location.href = '/PaymentOrder/GetPaymentOrderDetails?paymentOrderId=' + orderId;
    };

    // Clean up timers when needed
    self.destroy = function () {
        Object.values(self.slaTimers).forEach(timerId => clearInterval(timerId));
        self.slaTimers = {};
        $('.table-responsive').off('scroll');
        $('#mobilePaymentOrdersCards').off('scroll');
    };
}