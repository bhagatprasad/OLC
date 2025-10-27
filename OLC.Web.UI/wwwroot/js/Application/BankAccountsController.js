function BankAccountsController() {
    var self = this;
    self.init = function (e) {
        $("#preloader").show();
        var accountNumberInputMask = new Inputmask("####-####-####-####");
        accountNumberInputMask.mask($('[id*=AccountNumber]'));

        $(document).on("click", ".dataClass", function () {
            $('.modal').hide();
            $('.modal-backdrop').remove();
        });
        self.LoadBankAccounts();
        self.loadBankAccountActivity();
        $(document).on("click", "#btncreateBankAccount", function (e) {
            self.AddBankAccount(e);
        });
        $(document).on("click", ".btnEditBankAccount", function (e) {
            var BankAccountId = $(this).data("id");
            self.EditBankAccount(BankAccountId);
        });
        $(document).on("click", ".transaction-item", function () {
            var activityId = $(this).data("id");
            self.loadUserBankAccountsActivityDetails(activityId);
        });
    };
    self.loadBankAccountActivity = function () {
        $.ajax({
            url: "/BankAccounts/LoadUserBankAccountActivity",
            type: 'GET',
            data: { take:100, skip:0 },
            dataType: 'html', // added data type
            beforeSend: function () {
                $("#preloader").show();
            },
            complete: function () {
                $("#preloader").hide();
            },
            success: function (res) {
                $("#BankAccountActivityDetailsListPartial").html("");
                $("#BankAccountActivityDetailsListPartial").html(res);

            }
        });
    };
    self.LoadUserBankAccounts = function () {
        $.ajax({
            url: "/BankAccounts/LoadUserBankAccounts",
            type: 'GET',
            dataType: 'html', // added data type
            beforeSend: function () {
                $("#preloader").show();
            },
            complete: function () {
                $("#preloader").hide();
            },
            success: function (res) {
                $("#BankAccountsPartial").html("");
                $("#BankAccountsPartial").html(res);

            }
        });
    }
    self.LoadBanks = function () {
        $.ajax({
            url: "/BankAccounts/banklist",
            type: 'GET',
            dataType: 'html', // added data type
            beforeSend: function () {
                $("#preloader").show();
            },
            complete: function () {
                $("#preloader").hide();
            },
            success: function (res) {
                $("#BankAccountsPartial").html("");
                $("#BankAccountsPartial").html(res);

            }
        });
    }
    self.loadBankAccountActivity = function () {
        var take = $("#take").val();
        var skip = $("#skip").val();
        $.ajax({
            url: "/BankAccounts/LoadUserBankAccountsActivity",
            type: 'GET',
            data: { take: take, skip: skip },
            dataType: 'html', // added data type
            success: function (res) {
                $("#BankAccountActivityDetailsListPartial").html("");
                $("#BankAccountActivityDetailsListPartial").html(res);
            }
        });
    }
    self.loadUserBankAccountsActivityDetails = function (id) {
        $.ajax({
            url: "/BankAccounts/LoadUserBankAccountsActivityDetails",
            type: 'GET',
            data: { id: id },
            dataType: 'html', // added data type
            success: function (res) {
                $("#BankAccountActivityDetailsPartial").html("");
                $("#BankAccountActivityDetailsPartial").html(res);
                $("#transaction-detail").modal('show');
            }
        });
    }
    self.EditedBankAccount = function (e) {
        e.preventDefault();
        $.ajax({
            url: "/BankAccounts/EditBankAccount",
            type: "POST",
            data: $('#updateBankAccount').serialize(),
            dataType: 'json',
            success: function (responce) {
                $("#edit-BankAccount-details-data").modal('hide');
                self.loadBankAccountActivity();
                self.LoadUserBankAccount();
            },
            error: function (err) {
                console.log(err);
            }
        }); // ajax call closing
    }
    self.EditBankAccount = function (id) {
        $.ajax({
            url: "/BankAccounts/EditBankAccount",
            type: 'GET',
            data: { id: id },
            dataType: 'html', // added data type
            success: function (res) {
                $("#edit-BankAccount-details-data").modal('show');
                $("#EditBankAccountPartial").html("");
                $("#EditBankAccountPartial").html(res);
            }
        });
    }
    self.AddBankAccount = function (e) {
        debugger;
        e.preventDefault();
        $.ajax({
            url: "/BankAccounts/CreateBankAccount",
            type: "POST",
            data: $('#addBankAccount').serialize(),
            dataType: 'json',
            beforeSend: function () {

                $("#preloader").show();
            },
            complete: function () {
                $("#preloader").hide();
            },
            success: function (responce) {
                $("#add-new-BankAccount-details").modal('hide');
                $('.modal-backdrop').remove();
                self.loadBankAccountActivity();
                self.LoadBankAccounts();
            },
            error: function (err) {
                console.log(err);
            }
        }); // ajax call closing
    }
}