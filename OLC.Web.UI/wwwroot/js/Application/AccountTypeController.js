function AccountTypeController() {
    var self = this;

    self.ApplicationUser = {};
    self.AccountTypes = [];
    self.CurrentSelectedAccountType = null;
    self.CurrentSelectedAccountType = {};

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetAccountTypes();

        function GetAccountTypes() {
            $.ajax({
                type: "GET",
                url: "/AccountType/GetAccountTypes",
                data: { Id: self.ApplicationUser.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.AccountTypes = response && response.data ? response.data : [];
                    loadAccountTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

        function getStatusBadge(type) {
            if (type.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-accounttype-id="${type.Id}" class="badge bg-warning status-badge activiate-type">In Active</span>`;
            }
        }

        function loadAccountTypes() {
            const tbody = $('#accountTypesBody');
            const cardsContainer = $('#mobileAccountTypesCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.AccountTypes.length > 0) {
                self.AccountTypes.forEach(function (type) {
                    const statusBadge = getStatusBadge(type);

                    const actionButtons = `
                        <button class="btn btn-sm btn-outline-warning edit-type me-1" data-accounttype-id="${type.Id}" title="edit type">
                            <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-outline-danger delete-type" data-accounttype-id="${type.Id}" title="delete type">
                            <i class="fas fa-trash"></i>
                        </button>
                    `;

                    const row = `
                        <tr>
                            <td>#${type.Id}</td>
                            <td>${type.Name || ''}</td>
                            <td>${type.Code || ''}</td>
                            <td>${formatDate(type.CreatedOn)}</td>
                            <td>${formatDate(type.ModifiedOn)}</td>
                            <td>${statusBadge}</td>
                            <td>${actionButtons}</td>
                        </tr>
                    `;
                    tbody.append(row);

                    const typeHtml = `
                        <div class="card mb-3 pt-2">
                            <div class="card-body">
                                <p class="card-text mb-1"><strong>ID:</strong> #${type.Id}</p>
                                <p class="card-text mb-1"><strong>Name:</strong> ${type.Name}</p>
                                <p class="card-text mb-1"><strong>Code:</strong> ${type.Code}</p>
                                <p class="card-text mb-1"><strong>Status:</strong> ${statusBadge}</p>
                                <p class="card-text mb-1"><strong>CreatedOn:</strong> ${formatDate(type.CreatedOn)}</p>
                                <p class="card-text mb-1"><strong>ModifiedOn:</strong> ${formatDate(type.ModifiedOn)}</p>
                            </div>
                            <div class="card-footer d-flex justify-content-between">
                                ${actionButtons}
                            </div>
                        </div>`;
                    cardsContainer.append(typeHtml);  
                });
            }
        }

        $(document).on("click", ".activiate-type", function () {
            var typeId = $(this).data("type-id");
            console.log(typeId);
        });

        $(document).on("click", "#addAccountTypeBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditAccountTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("accounttype-id"));  
            var selectedAccountType = self.AccountTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected account type is .." + JSON.stringify(selectedAccountType));
            self.CurrentSelectedAccountType = selectedAccountType;

            $("#Name").val(self.CurrentSelectedAccountType.Name);
            $("#Code").val(self.CurrentSelectedAccountType.Code);
            $("#IsActive").val(self.CurrentSelectedAccountType.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentSelectedAccountType = null;
            $("#viewAccountType").modal("hide");
            $("#deleteAccountType").modal("hide");
        });

        $(document).on("click", ".delete-type", function () {
            console.log("deleting...");
            var typeId = parseInt($(this).data("accounttype-id"));  
            var selectedAccountType = self.AccountTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected account type is .." + JSON.stringify(selectedAccountType));
            self.CurrentSelectedAccountType = selectedAccountType;

            $("#DeleteName").val(self.CurrentSelectedAccountType.Name);
            $("#DeleteCode").val(self.CurrentSelectedAccountType.Code);
            $("#DeleteStatus").val(self.CurrentSelectedAccountType.IsActive ? "true" : "false");

            $("#deleteAccountType").modal("show");
        });

        $(document).on("click", "#deleteAccountTypeBtn", function () {
            console.log("delete yes clicked");
            $.ajax({
                type: "DELETE",
                url: "/AccountType/DeleteAccountType",
                data: { accountTypeId: self.CurrentSelectedAccountType.Id },// Fixed: Query param
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedAccountType = null;
                    $("#deleteAccountType").modal("hide");
                    GetAccountTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });

        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("accounttype-id"));  
            var selectedAccountType = self.AccountTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected account type is .." + JSON.stringify(selectedAccountType));
            self.CurrentSelectedAccountType = selectedAccountType;

            $("#ViewName").val(self.CurrentSelectedAccountType.Name);
            $("#ViewCode").val(self.CurrentSelectedAccountType.Code);
            $("#ViewCreatedBy").val(self.CurrentSelectedAccountType.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedAccountType.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedAccountType.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedAccountType.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedAccountType.IsActive);

            $("#viewAccountType").modal("show");
        });

        $('#AddEditAccountTypeForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";  
            console.log(name);

            var accountType = {
                Id: self.CurrentSelectedAccountType ? self.CurrentSelectedAccountType.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: isActive  
            };
            console.log("accountType..." + JSON.stringify(accountType));

            $.ajax({
                type: "POST",
                url: "/AccountType/SaveAccountType",
                cache: false,
                data: JSON.stringify(accountType),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedAccountType = null;
                    $('#AddEditAccountTypeForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetAccountTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    };
}