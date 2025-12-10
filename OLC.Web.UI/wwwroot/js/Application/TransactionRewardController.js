function TransactionRewardController() {
    var self = this;

    self.TransactionRewards = [];
    self.filteredRewards = [];
    self.ApplicationUser = {};
    self.isMobile = window.innerWidth < 768;

    // Pagination
    self.currentPage = 0;
    self.pageSize = 50;
    self.isLoading = false;
    self.hasMoreData = true;
    self.currentDisplayedRewards = [];

    // Init
    self.init = function () {
        $(".se-pre-con").show();

        var appUserInfo = storageService.get("ApplicationUser");
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        self.loadAllTransactionRewards();
    };

    // Summary Cards
    self.populateSummaryCards = function () {
        if (self.TransactionRewards.length === 0) return;

        const totalRewards = self.TransactionRewards
            .reduce((sum, r) => sum + (r.RewardAmount || 0), 0);

        const completedRewards = self.TransactionRewards
            .filter(r => r.IsActive === true)
            .reduce((sum, r) => sum + (r.RewardAmount || 0), 0);

        const pendingRewards = self.TransactionRewards
            .filter(r => r.IsActive === false)
            .reduce((sum, r) => sum + (r.RewardAmount || 0), 0);

        $("#totalRewards").text("$" + totalRewards.toLocaleString());
        $("#completedRewards").text("$" + completedRewards.toLocaleString());
        $("#pendingRewards").text("$" + pendingRewards.toLocaleString());
    };

    // Load All Reward Transactions
    self.loadAllTransactionRewards = function () {
        $.ajax({
            url: "/TransactionReward/GetAllExecutiveTransactionRewardDetails",
            method: "GET",
            success: function (response) {
                self.TransactionRewards = response?.data || [];
                self.filteredRewards = [...self.TransactionRewards];

                self.populateSummaryCards();
                self.resetPagination();
                self.loadNextPage();
                self.initializeViewHandlers();
                self.initializeScrollHandler();

                $(".se-pre-con").hide();
            },
            error: function (error) {
                console.error("Failed to load transaction rewards:", error);
                $(".se-pre-con").hide();
            }
        });
    };

    // Scroll Handler
    self.initializeScrollHandler = function () {
        const container = self.isMobile ? $("#mobileRewardCards") : $(".table-responsive");

        container.off("scroll").on("scroll", function () {
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

        const nextData = self.filteredRewards.slice(startIndex, endIndex);

        if (nextData.length === 0) {
            self.hasMoreData = false;
            self.isLoading = false;
            return;
        }

        self.currentDisplayedRewards.push(...nextData);
        self.renderRewards(nextData, self.currentPage === 0);

        self.currentPage++;
        self.hasMoreData = nextData.length === self.pageSize;
        self.isLoading = false;
    };

    // Render Rewards
    self.renderRewards = function (rewards, clearExisting = false) {
        const tbody = $('#rewardTableBody');
        const mobileContainer = $('#mobileRewardCards');

        if (clearExisting) {
            tbody.empty();
            mobileContainer.empty();
        }

        const fragment = document.createDocumentFragment();
        const mobileFragment = document.createDocumentFragment();

        rewards.forEach(r => {

            const actionButtons = self.getActionButtons(r, false);
            const mobileActionButtons = self.getActionButtons(r, true);

            // -----------------------------------------
            // DESKTOP TABLE ROW
            // -----------------------------------------
            if (!self.isMobile) {

                const row = document.createElement("tr");

                row.innerHTML = `
                <td>${r.WalletId || "N/A"}</td>
                <td>${r.PaymentOrderReferenceId || "N/A"}</td>
                <td>${self.formatCurrency(r.TotalEarned)}</td>
                <td>${self.formatCurrency(r.TotalSpent)}</td>
                <td>${self.formatCurrency(r.CurrentBalance)}</td>
                <td>${self.formatCurrency(r.ChargeableAmount)}</td>
                <td>${self.formatCurrency(r.DepositableAmount)}</td>
                <td>${self.formatCurrency(r.RewardAmount)}</td>

                <td>
                    <div class="small">
                        <div><i class="fas fa-credit-card me-1 text-success"></i>${r.CardNumber || "N/A"}</div>
                        <div><i class="fas fa-university me-1 text-info"></i>${r.AccountNumber || "N/A"}</div>
                    </div>
                </td>

                <td>${r.CreatedOn ? new Date(r.CreatedOn).toLocaleString() : "N/A"}</td>

                <td>
                    <span class="badge ${r.IsActive ? "bg-success" : "bg-danger"}">
                        ${r.IsActive ? "Active" : "Inactive"}
                    </span>
                </td>

                <td>
                    <div class="btn-group">
                        ${actionButtons}
                    </div>
                </td>
            `;
                fragment.appendChild(row);
            }

            // -----------------------------------------
            // MOBILE VERSION (CARD UI)
            // -----------------------------------------
            else {

                const card = document.createElement("div");
                card.className = "card mb-3 border";

                card.innerHTML = `
                <div class="card-body">

                    <div><strong>Wallet Id:</strong> ${r.WalletId || "N/A"}</div>
                    <div><strong>Order Ref:</strong> ${r.PaymentOrderReferenceId || "N/A"}</div>

                    <hr class="my-2">

                    <div><strong>Total Earned:</strong> ${self.formatCurrency(r.TotalEarned)}</div>
                    <div><strong>Total Spent:</strong> ${self.formatCurrency(r.TotalSpent)}</div>
                    <div><strong>Balance:</strong> ${self.formatCurrency(r.CurrentBalance)}</div>

                    <div><strong>Chargeable:</strong> ${self.formatCurrency(r.ChargeableAmount)}</div>
                    <div><strong>Depositable:</strong> ${self.formatCurrency(r.DepositableAmount)}</div>
                    <div><strong>Reward:</strong> ${self.formatCurrency(r.RewardAmount)}</div>

                    <hr class="my-2">

                    <div><strong>Payment Methods:</strong></div>
                    <div><i class="fas fa-credit-card me-1 text-muted"></i>${r.CardNumber || "N/A"}</div>
                    <div><i class="fas fa-university me-1 text-muted"></i>${r.AccountNumber || "N/A"}</div>

                    <hr class="my-2">

                    <div><strong>Status:</strong>
                        <span class="badge ${r.IsActive ? "bg-success" : "bg-danger"}">
                            ${r.IsActive ? "Active" : "Inactive"}
                        </span>
                    </div>

                    <div><strong>Created:</strong> ${r.CreatedOn ? new Date(r.CreatedOn).toLocaleString() : "N/A"}</div>
                     </div>

                    <div class="card-footer py-2">
                        <div class="btn-group w-100">${mobileActionButtons}</div>
                    </div>
            `;

                mobileFragment.appendChild(card);
            }
        });

        if (!self.isMobile) tbody.append(fragment);
        else mobileContainer.append(mobileFragment);

        self.initializeViewHandlers();
    };

            


    // Reset pagination
    self.resetPagination = function () {
        self.currentPage = 0;
        self.hasMoreData = true;
        self.currentDisplayedRewards = [];

        $("#rewardTableBody").empty();
        $("#mobileRewardCards").empty();
    };

    // Action Buttons
    self.getActionButtons = function (reward, isMobile = false) {
        let btn = `
            <button class="btn btn-sm btn-outline-primary view-reward"
                data-reward-id="${reward.Id}"
                title="View Reward Details">
                <i class="fas fa-eye"></i>
            </button>
        `;
        return btn;
    };

    // View Reward Details
    self.initializeViewHandlers = function () {
        $(".view-reward").off("click").on("click", function () {
            const rewardId = $(this).data("reward-id");
            window.location.href = "/ExecutiveReward/GetRewardDetails?rewardId=" + rewardId;
        });
    };

    // Search
    self.performSearch = function (term) {
        if (!term || term.trim() === "") {
            self.filteredRewards = [...self.TransactionRewards];
        } else {
            const t = term.toLowerCase().trim();
            self.filteredRewards = self.TransactionRewards.filter(r =>
                (r.WalletId && r.WalletId.toString().includes(t)) ||
                (r.PaymentOrderReferenceId && r.PaymentOrderReferenceId.toLowerCase().includes(t)) ||
                (r.CardNumber && r.CardNumber.toLowerCase().includes(t)) ||
                (r.AccountNumber && r.AccountNumber.toLowerCase().includes(t))
            );
        }

        self.resetPagination();
        self.loadNextPage();
    };

    self.initializeSearch = function () {
        $("#rewardSearch").on("input", function () {
            self.performSearch($(this).val());
        });
    };

    // Currency Format
    self.formatCurrency = function (amount) {
        if (amount == null) return "$0.00";
        return "$" + parseFloat(amount).toLocaleString("en-US", {
            minimumFractionDigits: 2
        });
    };
}

// Init
$(document).ready(function () {
    const ctrl = new TransactionRewardController();
    ctrl.init();
    ctrl.initializeSearch();
});
