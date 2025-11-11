function ExecutiveBoardController() {

    var self = this;

    self.ExecutivePaymentOrders = [];

    self.filteredPaymentOrders = [];

    self.CoreStatus = [];

    self.ApplicationUser = {};

    var actions = [];

    self.slaTimers = {};

    self.CurrentSelectedPaymentOrder = [];

    self.selectedPaymentOrder = {};

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
            $(".se-pre-con").show();
            var responses = arguments;
            console.log(responses);
            self.ExecutivePaymentOrders = responses[0][0] && responses[0][0].data ? responses[0][0].data : [];
            self.filteredPaymentOrders = [...self.ExecutivePaymentOrders];
            self.CoreStatus = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];

            self.populateSummaryCards();
            self.populatePaymentOrdersGrid();
            self.initializeSearch();

            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.log('One or more requests failed:', error);
            $(".se-pre-con").hide();
        });
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

    // Perform search across payment orders
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

    // Update SLA timer for a specific order
    self.updateSLATimer = function (orderId) {
        const order = self.ExecutivePaymentOrders.find(o => o.Id === orderId);
        if (!order) return;

        const slaInfo = calculateSLATimer(order);

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
        self.ExecutivePaymentOrders.forEach(order => {
            if (order.OrderStatus !== 'Completed') {
                self.slaTimers[order.Id] = setInterval(() => {
                    self.updateSLATimer(order.Id);
                }, 1000);
            }
        });
    };

    // Populate payment orders grid
    self.populatePaymentOrdersGrid = function () {
        const tbody = $('#paymentOrdersBody');
        const cardsContainer = $('#mobilePaymentOrdersCards');
        tbody.empty();
        cardsContainer.empty();

        if (self.filteredPaymentOrders.length === 0) {
            tbody.append(`
                <tr>
                    <td colspan="10" class="text-center text-muted">
                        ${self.ExecutivePaymentOrders.length === 0 ? 'No payment orders found' : 'No orders match your search'}
                    </td>
                </tr>
            `);
            cardsContainer.append(`
                <div class="text-center text-muted p-4">
                    ${self.ExecutivePaymentOrders.length === 0 ? 'No payment orders found' : 'No orders match your search'}
                </div>
            `);
            return;
        }

        self.filteredPaymentOrders.forEach(function (order) {
            const statusBadge = self.getStatusBadge(order.OrderStatus);
            const createdDate = self.formatDate(order.CreatedOn);
            const creditCardDisplay = self.formatCreditCard(order.CreditCardNumber);
            const bankAccountDisplay = self.formatBankAccount(order.BankAccountNumber);
            const slaInfo = calculateSLATimer(order);

            let actionButtons = '';

            if (order.OrderStatus === "Completed") {
                actionButtons = `
                <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Order Details">
                    <i class="fas fa-eye"></i>
                </button>
            `;
            } else if (order.OrderStatus === "Draft") {
                actionButtons = `
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Order Details">
            <i class="fas fa-eye"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    `;
            } else if (order.OrderStatus === "Payment Receved" && order.PaymentStatus === "Paid") {
                actionButtons = `
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Order Details">
            <i class="fas fa-eye"></i>
        </button>
        <button class="btn btn-sm btn-outline-success approve-order" data-order-id="${order.Id}" title="Approve Order">
            <i class="fas fa-check"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    `;
            } else if (order.OrderStatus === "Approved") {
                actionButtons = `
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Order Details">
            <i class="fas fa-eye"></i>
        </button>
        <button class="btn btn-sm btn-outline-success pay-now" data-order-id="${order.Id}" title="Pay Now">
            <i class="fas fa-credit-card"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    `;
            } else {
                actionButtons = `
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Order Details">
            <i class="fas fa-eye"></i>
        </button>
    `;
            }

            // MOBILE - Grouped compact icons with Cancel icon for all status except Completed
            const mobileActionButtonsGrouped = order.OrderStatus === "Completed" ? `
    <div class="btn-group" role="group">
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Details">
            <i class="fas fa-file-invoice"></i>
        </button>
    </div>
` : order.OrderStatus === "Draft" ? `
    <div class="btn-group" role="group">
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Details">
            <i class="fas fa-search"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    </div>
` : order.OrderStatus === "Payment Received" && order.PaymentStatus === "Paid" ? `
    <div class="btn-group" role="group">
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Details">
            <i class="fas fa-search"></i>
        </button>
        <button class="btn btn-sm btn-outline-success approve-order" data-order-id="${order.Id}" title="Approve Order">
            <i class="fas fa-check-circle"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    </div>
` : order.OrderStatus === "Approved" ? `
    <div class="btn-group" role="group">
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Details">
            <i class="fas fa-search"></i>
        </button>
        <button class="btn btn-sm btn-outline-success pay-now" data-order-id="${order.Id}" title="Pay Now">
            <i class="fas fa-credit-card"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    </div>
` : `
    <div class="btn-group" role="group">
        <button class="btn btn-sm btn-outline-primary view-order" data-order-id="${order.Id}" title="View Details">
            <i class="fas fa-search"></i>
        </button>
        <button class="btn btn-sm btn-outline-warning process-order" data-order-id="${order.Id}" title="Process Order">
            <i class="fas fa-play-circle"></i>
        </button>
        <button class="btn btn-sm btn-outline-danger cancel-order" data-order-id="${order.Id}" title="Cancel Order">
            <i class="fas fa-trash-alt"></i>
        </button>
    </div>
`;


            // Desktop table row - BOTH USER INFO AND PAYMENT METHODS
            const row = `
                <tr class="payment-order-item" data-order-id="${order.Id}">
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
                       <div class="btn-group">
                       ${actionButtons}
                       </div>
                    </td>
                </tr>
            `;
            tbody.append(row);

            // Mobile card - COMPREHENSIVE INFORMATION
            const cardHtml = `
                <div class="card mb-3 border" data-order-id="${order.Id}">
                    <div class="card-header bg-light d-flex justify-content-between align-items-center">
                        <div>
                            <strong class="text-primary">${order.OrderReference || 'N/A'}</strong>
                            <br>
                            <small class="text-muted">${self.truncateText(order.PaymentReasonName, 30)}</small>
                        </div>
                        ${statusBadge}
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-6 mb-2">
                                <small class="text-muted">Amount:</small>
                                <div class="fw-bold">${self.formatCurrency(order.Amount)}</div>
                            </div>
                            <div class="col-6 mb-2">
                                <small class="text-muted">Charge:</small>
                                <div>${self.formatCurrency(order.TotalAmountToChargeCustomer)}</div>
                            </div>
                            <div class="col-6 mb-2">
                                <small class="text-muted">Deposit:</small>
                                <div>${self.formatCurrency(order.TotalAmountToDepositToCustomer)}</div>
                            </div>
                            <div class="col-6 mb-2">
                                <small class="text-muted">SLA:</small>
                                <div><span class="sla-timer ${slaInfo.class}">${slaInfo.display}</span></div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">Created:</small>
                                <div><small class="text-muted">${createdDate}</small></div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">User Email:</small>
                                <div><i class="fas fa-envelope me-1 text-muted"></i>${order.UserEmail || 'N/A'}</div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">User Phone:</small>
                                <div><i class="fas fa-phone me-1 text-muted"></i>${order.UserPhone || 'N/A'}</div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">Credit Card:</small>
                                <div><i class="fas fa-credit-card me-1 text-success"></i>${creditCardDisplay}</div>
                            </div>
                            <div class="col-12 mb-2">
                                <small class="text-muted">Bank Account:</small>
                                <div><i class="fas fa-university me-1 text-info"></i>${bankAccountDisplay}</div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                       ${mobileActionButtonsGrouped}
                    </div>
                </div>
            `;
            cardsContainer.append(cardHtml);
        });

        // Initialize view order click handlers
        self.initializeViewOrderHandlers();

        // Start SLA timers
        self.startSLATimers();
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
        window.location.href = '/PaymentOrder/GetPaymentOrderDetails?paymentOrderId=' + orderId;
    };


    /////Cancel Order.............!

    $(document).on("click", ".cancel-order", function () {

        $('#cancelOrderModal').modal({ backdrop: 'static', keyboard: false });

        console.log("cancel...........");

        var orderId = $(this).data("order-id");

        var selectedPaymentOrder = self.ExecutivePaymentOrders.filter(x => x.Id == orderId)[0];

        console.log("current selected cancel order is................" + JSON.stringify(selectedPaymentOrder));

        self.CurrentSelectedPaymentOrder = selectedPaymentOrder;

        $("#cancelOrderModal").modal("show");

    });

    $(document).on("click", "#btnCancelOrder", function () {

        var message = $("#cancelOrderComment").val();

        console.log("Cancel Order:", self.CurrentSelectedPaymentOrder);

        console.log("Comment:", message);

        var processPaymentOrder = {
            PaymentOrderId: self.CurrentSelectedPaymentOrder.Id,
            OrderStatusId: statusConstants.Cancelled,
            PaymentStatusId: self.CurrentSelectedPaymentOrder.PaymentStatusId,
            DepositeStatusId: statusConstants.Cancelled,
            CreatedBy: self.ApplicationUser.Id,
            Description: message
        };

        console.log("Processing cancel payment order:", processPaymentOrder);


        self.ProcesspaymentOrderDetailsAsync(processPaymentOrder, true);


    });


    $(document).on("click", ".approve-order", function () {

        $('#approveOrderModal').modal({ backdrop: 'static', keyboard: false });

        console.log("deleting...");

        var orderId = $(this).data("order-id");

        var selectedPaymentOrder = self.ExecutivePaymentOrders.filter(x => x.Id == orderId)[0];

        console.log("current selected  approve order is .." + JSON.stringify(selectedPaymentOrder));

        self.selectedPaymentOrder = selectedPaymentOrder;

        $("#approveOrderModal").modal("show");
    });

    $(document).on("click", "#btnApproveOrder", function () {

        var message = $("#approvalComment").val();

        console.log("Processing Order:", self.selectedPaymentOrder);

        console.log("Comment:", message);

        const orderStatusId = statusConstants.Approved;

        var processPaymentOrder = {
            PaymentOrderId: self.selectedPaymentOrder.Id,
            OrderStatusId: orderStatusId,
            PaymentStatusId: self.selectedPaymentOrder.PaymentStatusId,
            DepositeStatusId: self.selectedPaymentOrder.DepositStatusId,
            CreatedBy: self.ApplicationUser.Id,
            Description: message
        };

        console.log("process payment order......");

        self.ProcesspaymentOrderDetailsAsync(processPaymentOrder, false);

    });

    self.ProcesspaymentOrderDetailsAsync = function (processPaymentOrder, isCancel) {
        $(".se-pre-con").show();
        $.ajax({
            type: "POST",
            url: "/PaymentOrder/ProcessPaymentOrder",
            data: JSON.stringify(processPaymentOrder),
            cache: false,
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',

            success: function (response) {
                console.log(response);

                if (response.data) {

                    self.CurrentSelectedPaymentOrder = null;

                    if (isCancel) {
                        $("#cancelOrderComment").val("");

                        $("#cancelOrderModal").modal("hide");

                    } else {
                        $("#approveOrderModal").modal("hide");

                        $("#approvalComment").val("");
                    }

                    self.ExecutivePaymentOrders = [];
                    self.filteredPaymentOrders = [];
                    self.ExecutivePaymentOrders = response && response.data ? response.data : [];
                    self.filteredPaymentOrders = [...self.ExecutivePaymentOrders];
                    self.populateSummaryCards();
                    self.populatePaymentOrdersGrid();
                    self.initializeSearch();
                    $(".se-pre-con").hide();

                }

            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    $(document).on("click", ".btn-approve-order-close", function () {
        self.selectedPaymentOrder = {};
        $("#approveOrderModal").modal("hide");
    });

    $(document).on("click", ".btn-cancel-order-close", function () {
        self.selectedPaymentOrder = {};
        $("#cancelOrderModal").modal("hide");
    });
}