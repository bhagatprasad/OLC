function UserWalletController() {
    var self = this;

    self.UserWallets = [];
    self.filteredWallets = [];
    self.ApplicationUser = {};
    self.isMobile = window.innerWidth < 768;

    // Pagination variables
    self.currentPage = 0;
    self.pageSize = 50;
    self.isLoading = false;
    self.hasMoreData = true;
    self.currentDisplayedWallets = [];

    // Initialize controller
    self.init = function () {
        $(".se-pre-con").show();
        
        const appUserInfo = localStorage.getItem('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = JSON.parse(appUserInfo);
        }

        // Load all wallets
        self.loadAllWallets();
    };
    self.populateSummaryCards = function () {
        if (self.UserWallets.length === 0) return;

        const totalAmount = self.UserWallets.reduce((sum, wallet) => sum + (wallet.Amount || 0), 0);
        const totalEarned = self.UserWallets
            .reduce((sum, wallet) => sum + (wallet.TotalEarned || 0), 0);
        const currentBalance = self.UserWallets
            .reduce((sum, wallet) => sum + (wallet.CurrentBalance || 0), 0);
        const totalSpent = self.UserWallets
            .reduce((sum, wallet) => sum + (wallet.TotalSpent || 0), 0);
        const totalFailed = self.UserWallets
            .reduce((sum, wallet) => sum + (wallet.totalFailed || 0), 0);

        $('#totalEarned').text('$' + totalEarned.toLocaleString());
        $('#currentBalance').text('$' + currentBalance.toLocaleString());
        $('#totalSpent').text('$' + totalSpent.toLocaleString());
        $('#totalFailed').text('$' + totalFailed.toLocaleString());
    };

    // Load all wallets
    self.loadAllWallets = function () {
        $.ajax({
            url: "/UserWallet/GetAllUserWallets",
            method: "GET",
            success: function (response) {
                self.UserWallets = response?.data || [];
                self.filteredWallets = [...self.UserWallets];
                self.populateSummaryCards();
                self.resetPagination();
                self.loadNextPage();
                self.initializeViewWalletHandlers();
                self.initializeScrollHandler();
                $(".se-pre-con").hide();
            },
            error: function (error) {
                console.error("Failed to load wallets:", error);
                $(".se-pre-con").hide();
                alert("Failed to load wallets. Check console for details.");
            }
        });
    };

    // Infinite scroll handler
    self.initializeScrollHandler = function () {
        if (self.isMobile) {
            const mobileContainer = $('#mobileWalletCards');
            mobileContainer.off('scroll').on('scroll', function () {
                if (self.isLoading || !self.hasMoreData) return;

                const scrollTop = $(this).scrollTop();
                const scrollHeight = $(this)[0].scrollHeight;
                const clientHeight = $(this).innerHeight();

                if (scrollTop + clientHeight >= scrollHeight - 100) {
                    self.loadNextPage();
                }
            });
        } else {
            const tableContainer = $('.table-responsive');
            tableContainer.off('scroll').on('scroll', function () {
                if (self.isLoading || !self.hasMoreData) return;

                const scrollTop = $(this).scrollTop();
                const scrollHeight = $(this)[0].scrollHeight;
                const clientHeight = $(this).innerHeight();

                if (scrollTop + clientHeight >= scrollHeight - 100) {
                    self.loadNextPage();
                }
            });
        }
    };

    // Load next page
    self.loadNextPage = function () {
        if (self.isLoading || !self.hasMoreData) return;
        self.isLoading = true;

        const startIndex = self.currentPage * self.pageSize;
        const endIndex = startIndex + self.pageSize;
        const nextPageData = self.filteredWallets.slice(startIndex, endIndex);

        if (nextPageData.length === 0) {
            self.hasMoreData = false;
            self.isLoading = false;
            return;
        }

        self.currentDisplayedWallets = [...self.currentDisplayedWallets, ...nextPageData];
        self.renderWallets(nextPageData, self.currentPage === 0);

        self.currentPage++;
        self.hasMoreData = nextPageData.length === self.pageSize;
        self.isLoading = false;
    };
     

    // Render wallets
    self.renderWallets = function (wallets, clearExisting = false) {
        const tbody = $('#walletTableBody');
        const mobileContainer = $('#mobileWalletCards');

        if (clearExisting) {
            tbody.empty();
            mobileContainer.empty();
        }

        const fragment = document.createDocumentFragment();
        const mobileFragment = document.createDocumentFragment();

        wallets.forEach(wallet => {
            const statusBadge = self.getStatusBadge(wallet.Status);
            const actionButtons = self.getActionButtons(wallet, false); // Desktop
            const mobileActionButtons = self.getActionButtons(wallet, true); // Mobile

            if (!self.isMobile) {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${wallet.WalletId}</td>
                    <td>${wallet.WalletType}</td>
                    <td>${self.formatCurrency(wallet.CurrentBalance)}</td>
                    <td>${self.formatCurrency(wallet.TotalEarned)}</td>
                    <td>${self.formatCurrency(wallet.TotalSpent)}</td>
                    <td>${wallet.Currency}</td>                    
                    <td>
                        <div class="small">
                            <div><i class="fas fa-envelope me-1 text-muted"></i>${wallet.UserEmail || 'N/A'}</div>
                            <div><i class="fas fa-phone me-1 text-muted"></i>${wallet.UserPhone || 'N/A'}</div>
                        </div>
                    </td>
                    <td>${wallet.Status || 'N/A'}</td>
                   
                    <td>${self.formatDate(wallet.ModifiedOn)}</td>
                    <td>
                       <div class="btn-group">
                       ${actionButtons}
                       </div>
                    </td>
                `;
                fragment.appendChild(row);
            } else {
                const card = document.createElement('div');
                card.className = 'card mb-3 border';

                card.innerHTML = `
                        <div class="card-body">
                            <div><strong>ID:</strong> ${wallet.WalletId}</div>
                            <div><strong>Type:</strong> ${wallet.WalletType}</div>
                            <div><strong>Balance:</strong> ${self.formatCurrency(wallet.CurrentBalance)}</div>
                            <div><strong>Total Earned:</strong> ${self.formatCurrency(wallet.TotalEarned)}</div>
                            <div><strong>Total Spent:</strong> ${self.formatCurrency(wallet.TotalSpent)}</div>
                            <div><strong>Currency:</strong> ${wallet.Currency}</div>

                            <div class="mt-2">
                                <small class="text-muted d-block">User</small>
                                <div class="d-flex justify-content-between">
                                    <span><i class="fas fa-envelope me-1"></i>${wallet.UserEmail}</span>
                                    <span><i class="fas fa-phone me-1"></i>${wallet.UserPhone}</span>
                                </div>
                            </div>

                            <div><strong>Status:</strong> ${wallet.Status}</div>
                            <div><strong>Last Updated:</strong> ${self.formatDate(wallet.ModifiedOn)}</div>
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

        if (!self.isMobile) {
            tbody.append(fragment);
        } else {
            mobileContainer.append(mobileFragment);
        }
        self.initializeViewWalletHandlers();
    };

    // Reset pagination
    self.resetPagination = function () {
        self.currentPage = 0;
        self.hasMoreData = true;
        self.currentDisplayedWallets = [];
        $('#walletTableBody').empty();
        $('#mobileWalletCards').empty();
    };
    self.getActionButtons = function (wallet, isMobile = false) {
        let desktopButtons = `
        <button class="btn btn-sm btn-outline-primary view-wallet" 
                data-wallet-id="${wallet.WalletId}" 
                title="View Wallet Details">
            <i class="fas fa-eye"></i>
        </button>`;

        let mobileButtons = `
        <button class="btn btn-sm btn-outline-primary view-wallet" 
                data-wallet-id="${wallet.WalletId}" 
                title="View Details">
            <i class="fas fa-eye"></i>
        </button>`;

        return isMobile ? mobileButtons : desktopButtons;
    };

    // Status badge
    self.getStatusBadge = function (status) {
        if (!status) return '<span class="badge bg-secondary">Unknown</span>';
        const s = status.toLowerCase();
        if (s === 'active') return '<span class="badge bg-success">Active</span>';
        if (s === 'inactive') return '<span class="badge bg-danger">Inactive</span>';
        return '<span class="badge bg-secondary">' + status + '</span>';
    };

    // Currency formatter
    self.formatCurrency = function (amount) {
        if (amount == null) return '$0.00';
        return '$' + parseFloat(amount).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    };

    // Date formatter
    self.formatDate = function (dateString) {
        if (!dateString) return 'N/A';
        const date = new Date(dateString);
        return date.toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' });
    };

    // Search wallets
    self.performWalletSearch = function (term) {
        if (!term || term.trim() === '') {
            self.filteredWallets = [...self.UserWallets];
        } else {
            const t = term.toLowerCase().trim();
            self.filteredWallets = self.UserWallets.filter(w =>
                (w.WalletId && w.WalletId.toString().toLowerCase().includes(t)) ||
                (w.WalletType && w.WalletType.toLowerCase().includes(t)) ||
                (w.Currency && w.Currency.toLowerCase().includes(t)) ||
                (w.Status && w.Status.toLowerCase().includes(t)) ||
                (w.WalletInfo && w.WalletInfo.toLowerCase().includes(t))
            );
        }
        self.resetPagination();
        self.loadNextPage();
    };

    // Initialize search
    self.initializeSearch = function () {
        $('#walletSearch').on('input', function () {
            self.performWalletSearch($(this).val());
        });
    };
    self.initializeViewWalletHandlers = function () {
        $('.view-wallet').off('click').on('click', function () {
            const walletId = $(this).data('wallet-id');
            self.viewWalletDetails(walletId);
        });
    };
    self.viewWalletDetails = function (walletId) {
        console.log('Viewing wallet details for:', walletId);

        // Redirect to your wallet details endpoint
        window.location.href = '/UserWallet/GetUserWalletDetailsByUserId?userId=' + walletId;
    };


    // Destroy handlers
    self.destroy = function () {
        $('.table-responsive').off('scroll');
        $('#mobileWalletCards').off('scroll');
    };
}

// Initialize
$(document).ready(function () {
    const userWalletController = new UserWalletController();
    userWalletController.init();
    userWalletController.initializeSearch();
});
