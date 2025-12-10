function DepositOrderController() {
    var self = this;

    self.DepositOrders = [];
    self.filteredOrders = [];
    self.ApplicationUser = {};
    self.isMobile = window.innerWidth < 768;

    // Pagination
    self.currentPage = 0;
    self.pageSize = 50;
    self.isLoading = false;
    self.hasMoreData = true;
    self.currentDisplayedOrders = [];

    // Init
    self.init = function () {
        $(".se-pre-con").show();

        //var appUserInfo = storageService.get('ApplicationUser');
        //if (appUserInfo) {
        //    self.ApplicationUser = appUserInfo;
        //}

        self.loadAllDepositOrders();
    };

    // Summary Cards
    self.populateSummaryCards = function () {
        if (self.DepositOrders.length === 0) return;

        const totalAmount = self.DepositOrders
            .reduce((sum, o) => sum + (o.TotalAmount || 0), 0);

        const depositedAmount = self.DepositOrders
            .reduce((sum, o) => sum + (o.DepositedAmount || 0), 0);

        const pendingAmount = self.DepositOrders
            .reduce((sum, o) => sum + (o.PendingAmount || 0), 0);

        $('#totalAmount').text('$' + totalAmount.toLocaleString());
        $('#totalDeposited').text('$' + depositedAmount.toLocaleString());
        $('#totalPending').text('$' + pendingAmount.toLocaleString());
    };

    // Load All Orders
    self.loadAllDepositOrders = function () {
        $.ajax({
            url: "/DepositOrder/GetAllExecutiveDepositOrderDetails",
            method: "GET",
            success: function (response) {
                self.DepositOrders = response?.data || [];
                self.filteredOrders = [...self.DepositOrders];

                self.populateSummaryCards();
                self.resetPagination();
                self.loadNextPage();
                self.initializeViewHandlers();
                self.initializeScrollHandler();

                $(".se-pre-con").hide();
            },
            error: function (error) {
                console.error("Failed to load deposit orders:", error);
                $(".se-pre-con").hide();
            }
        });
    };

    // Infinite scroll handler
    self.initializeScrollHandler = function () {
        const container = self.isMobile ? $('#mobileDepositCards') : $('.table-responsive');

        container.off('scroll').on('scroll', function () {
            if (self.isLoading || !self.hasMoreData) return;

            const scrollTop = $(this).scrollTop();
            const scrollHeight = $(this)[0].scrollHeight;
            const clientHeight = $(this).innerHeight();

            if (scrollTop + clientHeight >= scrollHeight - 100) {
                self.loadNextPage();
            }
        });
    };

    // Load next page
    self.loadNextPage = function () {
        if (self.isLoading || !self.hasMoreData) return;
        self.isLoading = true;

        const startIndex = self.currentPage * self.pageSize;
        const endIndex = startIndex + self.pageSize;
        const nextData = self.filteredOrders.slice(startIndex, endIndex);

        if (nextData.length === 0) {
            self.hasMoreData = false;
            self.isLoading = false;
            return;
        }

        self.currentDisplayedOrders = [...self.currentDisplayedOrders, ...nextData];
        self.renderOrders(nextData, self.currentPage === 0);

        self.currentPage++;
        self.hasMoreData = nextData.length === self.pageSize;
        self.isLoading = false;
    };
   
    self.renderOrders = function (orders, clearExisting = false) {
        const tbody = $('#depositTableBody');
        const mobileContainer = $('#mobileDepositCards');

        if (clearExisting) {
            tbody.empty();
            mobileContainer.empty();
        }

        const fragment = document.createDocumentFragment();
        const mobileFragment = document.createDocumentFragment();

        orders.forEach(order => {

            const actionButtons = self.getActionButtons(order, false);
            const mobileActionButtons = self.getActionButtons(order, true);

            // Desktop table
            if (!self.isMobile) {
                const row = document.createElement('tr');
                row.innerHTML = `
                <td>${order.DepositeReference || order.DepositeReferance || 'N/A'}</td>
                <td>${order.OrderReference || 'N/A'}</td>
                <td>${self.formatCurrency(order.TotalAmount)}</td>
                <td>${self.formatCurrency(order.DepositedAmount)}</td>
                <td>${self.formatCurrency(order.PendingAmount)}</td>

                <td>
                    <div class="small">
                        <div><i class="fas fa-envelope me-1 text-muted"></i>${order.UserEmail || 'N/A'}</div>
                        <div><i class="fas fa-phone me-1 text-muted"></i>${order.UserPhone || 'N/A'}</div>
                    </div>
                </td>

                <td>
                    <div class="small">
                        <div><i class="fas fa-credit-card me-1 text-success"></i>${order.CreditCardNumber || 'N/A'}</div>
                        <div><i class="fas fa-university me-1 text-info"></i>${order.BankAccount || 'N/A'}</div>
                    </div>
                </td>

                <td>
                    <div class="btn-group">
                        ${actionButtons}
                    </div>
                </td>
            `;
                fragment.appendChild(row);
            }

            // Mobile card
            else {
                const card = document.createElement('div');
                card.className = 'card mb-3 border';

                card.innerHTML = `
                <div class="card-body">
                    <div><strong>Deposit Ref:</strong> ${order.DepositeReference || order.DepositeReferance || 'N/A'}</div>
                    <div><strong>Order Ref:</strong> ${order.OrderReference || 'N/A'}</div>

                    <div><strong>Total:</strong> ${self.formatCurrency(order.TotalAmount)}</div>
                    <div><strong>Deposited:</strong> ${self.formatCurrency(order.DepositedAmount)}</div>
                    <div><strong>Pending:</strong> ${self.formatCurrency(order.PendingAmount)}</div>

                    <div class="mt-2">
                        <small class="text-muted d-block">User</small>
                        <div class="d-flex justify-content-between">
                            <span><i class="fas fa-envelope me-1"></i>${order.UserEmail}</span>
                            <span><i class="fas fa-phone me-1"></i>${order.UserPhone}</span>
                        </div>
                     </div>

                    <div class="mt-2">
                        <small class="text-muted d-block">Deposite Methods</small>
                        <div class="d-flex justify-content-between">
                            <span><i class="fas fa-envelope me-1"></i>${order.CreditCardNumber || 'N/A'}</span>
                            <span><i class="fas fa-phone me-1"></i>${order.BankAccount || 'N/A'}</span>
                        </div>
                     </div>

                    
                <div class="card-footer py-2">
                    <div class="btn-group w-100">
                        ${mobileActionButtons}
                    </div>
                </div>
            `;

                mobileFragment.appendChild(card);
            }
        });

        if (!self.isMobile)
            tbody.append(fragment);
        else
            mobileContainer.append(mobileFragment);

        self.initializeViewHandlers();
    };
     
    // Reset pagination
    self.resetPagination = function () {
        self.currentPage = 0;
        self.hasMoreData = true;
        self.currentDisplayedOrders = [];

        $('#depositTableBody').empty();
        $('#mobileDepositCards').empty();
    };

    // Action Buttons
    self.getActionButtons = function (order, isMobile = false) {
        let desktopButtons = `
            <button class="btn btn-sm btn-outline-primary view-order"
                data-order-id="${order.Id}"
                title="View Deposit Order Details">
                <i class="fas fa-eye"></i>              
            </button>
        `;
        let mobileButtons = `
        <button class="btn btn-sm btn-outline-primary view-order"
                data-order-id="${order.Id}"
                title="View Details">
            <i class="fas fa-eye"></i>
        </button>
    `;

        return isMobile ? mobileButtons : desktopButtons;
    };

    // View Order Details
    self.initializeViewHandlers = function () {
        $(".view-order").off("click").on("click", function () {
            const orderId = $(this).data("order-id");
            self.initializeViewHandlers(orderId);
        });
    };

    self.viewDepositOrderDetails = function (orderId) {
        window.location.href = '/ExecutiveDepositOrder/GetDepositOrderDetails?orderId =' + orderId;
    };

    // Formatters
    self.formatCurrency = function (amount) {
        if (amount == null) return '$0.00';
        return '$' + parseFloat(amount)
            .toLocaleString('en-US', { minimumFractionDigits: 2 });
    };

    // SEARCH
    self.performSearch = function (term) {
        if (!term || term.trim() === "") {
            self.filteredOrders = [...self.DepositOrders];
        } else {
            const t = term.toLowerCase().trim();
            self.filteredOrders = self.DepositOrders.filter(o =>
                (o.DepositReference && o.DepositReference.toLowerCase().includes(t)) ||
                (o.OrderReference && o.OrderReference.toLowerCase().includes(t)) ||
                (o.UserEmail && o.UserEmail.toLowerCase().includes(t)) ||
                (o.UserPhone && o.UserPhone.toLowerCase().includes(t)) ||
                (o.BankAccount && o.BankAccount.toLowerCase().includes(t))
            );
        }

        self.resetPagination();
        self.loadNextPage();
    };

    self.initializeSearch = function () {
        $('#depositSearch').on('input', function () {
            self.performSearch($(this).val());
        });
    };
}

// Initialize
$(document).ready(function () {
    const ctrl = new DepositOrderController();
    ctrl.init();
    ctrl.initializeSearch();
});
