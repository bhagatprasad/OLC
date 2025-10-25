function UserBankAccountController() {

    var self = this;

    self.ApplicationUser = {};

    self.UserBankAccounts = [];

    self.Banks = [];

    self.AccountTypes = [];

    self.Currencies = [];

    self.CurrentSelectedBankAccount = null;

    self.currentSelectedBankAccount = {};

    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');

        console.log(appUserInfo);

        if (appUserInfo) {

            self.ApplicationUser = appUserInfo;
        }

        GetUserBankAccounts();
        GetBanks();
        GetAccountTypes();
        GetCurrencies();

        function GetUserBankAccounts() {
            $.ajax({
                type: "GET",
                url: "/BankAccount/GetUserBankAccounts",
                data: { userId: self.ApplicationUser.Id },
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.UserBankAccounts = response && response.data ? response.data : [];

                    loadUserBankAccounts();

                }, error: function (error) {
                    console.log(error);
                }
            });
        };

        function GetAccountTypes() {
            $.ajax({
                type: "GET",
                url: "/AccountType/GetAccountTypes",
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.AccountTypes = response && response.data ? response.data : [];
                    genarateDropdown("AccountType", self.AccountTypes, "Id", "Name");

                }, error: function (error) {
                    console.log(error);
                }
            });
        };

        function GetBanks() {
            $.ajax({
                type: "GET",
                url: "/Bank/GetBanks",
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.Banks = response && response.data ? response.data : [];
                    genarateDropdown("Bank", self.Banks, "Id", "Name");

                }, error: function (error) {
                    console.log(error);
                }
            });
        };

        function GetCurrencies() {

            var currencies = [
                { Id: 1, Name: "Indian Rupee", Code: "INR" },
                { Id: 2, Name: "US Dollar", Code: "USD" },
                { Id: 3, Name: "Euro", Code: "EUR" },
                { Id: 4, Name: "British Pound", Code: "GBP" },
                { Id: 5, Name: "Japanese Yen", Code: "JPY" },
                { Id: 6, Name: "Australian Dollar", Code: "AUD" },
                { Id: 7, Name: "Canadian Dollar", Code: "CAD" },
                { Id: 8, Name: "Swiss Franc", Code: "CHF" },
                { Id: 9, Name: "Chinese Yuan", Code: "CNY" },
                { Id: 10, Name: "Swedish Krona", Code: "SEK" }
            ];
            self.Currencies = currencies;
            genarateDropdown("Currency", self.Currencies, "Id", "Code");
            
        };

        function getStatusBadge(account) {
            if (account.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-account-id="${account.Id}" class="badge bg-warning status-badge activiate-account">In Active</span>`;
            }
        }

        function loadUserBankAccounts() {
            const tbody = $('#userBankAccountsBody');
            tbody.empty(); // Clear existing rows

            if (self.UserBankAccounts.length > 0) {
                self.UserBankAccounts.forEach(function (account) {

                    const statusBadge = getStatusBadge(account);

                    const actionButtons = `
                <button class="btn btn-sm btn-outline-primary view-account" data-account-id="${account.Id}" data-account='${JSON.stringify(account)}' title="view account">
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-sm btn-outline-warning edit-account" data-account-id="${account.Id}" data-account='${JSON.stringify(account)}' title="edit account">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-account" data-account-id="${account.Id}" data-account='${JSON.stringify(account)}' title="delete account">
                    <i class="fas fa-trash"></i>
                </button>
            `;

                    const row = `<tr class="transaction-item">
                <td>#${account.Id}</td>
                <td>${account.AccountHolderName || 'N/A'}</td>
                <td>${account.LastFourDigits || 'N/A'}</td>
                <td>${account.BankName || 'N/A'}</td>
                <td>${account.AccountType || 'N/A'}</td>
                <td>${account.Currency || 'N/A'}</td>
                <td>${statusBadge}</td>
                <td>
                   ${actionButtons}
                </td>
            </tr>`;

                    tbody.append(row);
                });
            }
        }

        $(document).on("click", ".activiate-account", function () {
            var accountId = $(this).data("account-id");

            console.log(accountId);
            // Implement activation logic here, e.g., AJAX call to activate the account
        });

        $(document).on("click", "#addBankAccountBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditUserBankAccountForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-account", function () {
            var accountId = $(this).data("account-id");
            var selectedBankAccount = self.UserBankAccounts.filter(x => x.Id == accountId)[0];

            console.log("current selected user bank account is .." + JSON.stringify(selectedBankAccount));

            self.CurrentSelectedBankAccount = selectedBankAccount;

            var accountType = null;
            var bank = null;
            var currency = null;

            if (self.CurrentSelectedBankAccount.AccountType)
                accountType = self.AccountTypes.filter(x => x.Name == self.CurrentSelectedBankAccount.AccountType)[0];

            if (self.CurrentSelectedBankAccount.BankName)
                bank = self.Banks.filter(x => x.Name == self.CurrentSelectedBankAccount.BankName)[0];

            if (self.CurrentSelectedBankAccount.Currency)
                currency = self.Currencies.filter(x => x.Code == self.CurrentSelectedBankAccount.Currency)[0];

            $("#AccountHolderName").val(self.CurrentSelectedBankAccount.AccountHolderName);
            $("#AccountNumber").val(self.CurrentSelectedBankAccount.AccountNumber);
            $("#LastFourDigits").val(self.CurrentSelectedBankAccount.LastFourDigits);
            $("#BranchName").val(self.CurrentSelectedBankAccount.BranchName);
            $("#RoutingNumber").val(self.CurrentSelectedBankAccount.RoutingNumber);
            $("#IFSCCode").val(self.CurrentSelectedBankAccount.IFSCCode);
            $("#SWIFTCode").val(self.CurrentSelectedBankAccount.SWIFTCode);

            if (accountType)
                $("#AccountType").val(accountType.Id);

            if (bank)
                $("#Bank").val(bank.Id);

            if (currency)
                $("#Currency").val(currency.Id);

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');

            console.log("Iam getting from edit button click");
        });

        $(document).on("click", ".btn-view-account-close", function () {
            self.CurrentSelectedBankAccount = null;
            $("#viewBankAccount").modal("hide");
            $("#deleteBankAccount").modal("hide");
        });

        $(document).on("click", ".delete-account", function () {
            console.log("deleting...");

            var accountId = $(this).data("account-id");

            var selectedBankAccount = self.UserBankAccounts.filter(x => x.Id == accountId)[0];

            console.log("current selected user bank account is .." + JSON.stringify(selectedBankAccount));

            self.CurrentSelectedBankAccount = selectedBankAccount;

            $("#DeleteAccountHolderName").val(self.CurrentSelectedBankAccount.AccountHolderName);
            $("#DeleteAccountNumber").val(self.CurrentSelectedBankAccount.AccountNumber);
            $("#DeleteLastFourDigits").val(self.CurrentSelectedBankAccount.LastFourDigits);
            $("#DeleteBranchName").val(self.CurrentSelectedBankAccount.BranchName);
            $("#DeleteRoutingNumber").val(self.CurrentSelectedBankAccount.RoutingNumber);
            $("#DeleteIFSCCode").val(self.CurrentSelectedBankAccount.IFSCCode);
            $("#DeleteSWIFTCode").val(self.CurrentSelectedBankAccount.SWIFTCode);

            $("#deleteBankAccount").modal("show");
        });

        $(document).on("click", "#deleteBankAccountBtn", function () {
            console.log("delete yes clicked");
            $.ajax({
                type: "DELETE",
                url: "/BankAccount/DeleteUserBankAccount",
                data: { id: self.CurrentSelectedBankAccount.Id },
                cache: false,
                success: function (response) {

                    console.log(response);

                    self.CurrentSelectedBankAccount = null;

                    $("#deleteBankAccount").modal("hide");

                    GetUserBankAccounts();

                }, error: function (error) {
                    console.log(error);
                }
            });

        });

        $(document).on("click", ".view-account", function () {

            console.log("Viewing account");

            var accountId = $(this).data("account-id");

            var selectedBankAccount = self.UserBankAccounts.filter(x => x.Id == accountId)[0];

            console.log("current selected user bank account is .." + JSON.stringify(selectedBankAccount));

            self.CurrentSelectedBankAccount = selectedBankAccount;

            $("#ViewAccountHolderName").val(self.CurrentSelectedBankAccount.AccountHolderName);
            $("#ViewAccountNumber").val(self.CurrentSelectedBankAccount.AccountNumber);
            $("#ViewLastFourDigits").val(self.CurrentSelectedBankAccount.LastFourDigits);
            $("#ViewBranchName").val(self.CurrentSelectedBankAccount.BranchName);
            $("#ViewRoutingNumber").val(self.CurrentSelectedBankAccount.RoutingNumber);
            $("#ViewIFSCCode").val(self.CurrentSelectedBankAccount.IFSCCode);
            $("#ViewSWIFTCode").val(self.CurrentSelectedBankAccount.SWIFTCode);
            $("#viewBankAccount").modal("show");

        });

        $('#AddEditUserBankAccountForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var accountHolderName = $("#AccountHolderName").val();
            var accountNumber = $("#AccountNumber").val();
            var lastFourDigits = $("#LastFourDigits").val();
            var branchName = $("#BranchName").val();
            var routingNumber = $("#RoutingNumber").val();
            var ifscCode = $("#IFSCCode").val();
            var swiftCode = $("#SWIFTCode").val();
            var accountType = $("#AccountType option:selected").text();
            var bankName = $("#Bank option:selected").text();
            var currency = $("#Currency option:selected").text();

            console.log(accountHolderName);

            var userAccount = {
                Id: self.CurrentSelectedBankAccount ? self.CurrentSelectedBankAccount.Id : 0,
                UserId: self.ApplicationUser.Id,
                AccountHolderName: accountHolderName,
                AccountNumber: accountNumber,
                LastFourDigits: lastFourDigits,
                BranchName: branchName,
                RoutingNumber: routingNumber,
                IFSCCode: ifscCode,
                SWIFTCode: swiftCode,
                AccountType: accountType,
                BankName: bankName,
                Currency: currency,
                IsPrimary: false,
                IsActive: true,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date()
            };

            console.log("userAccount...." + JSON.stringify(userAccount));

            $.ajax({
                type: "POST",
                url: "/BankAccount/SaveUserBankAccount",
                cache: false,
                data: JSON.stringify(userAccount),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    console.log(response)
                    self.CurrentSelectedBankAccount = null;
                    $('#AddEditUserBankAccountForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetUserBankAccounts();

                }, error: function (error) {
                    console.log(error);
                }
            });

        });
    }
}
