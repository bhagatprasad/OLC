 function WalletTypeController() {
    var self = this;
    self.ApplicationUser = {};
    self.WalletTypes = [];
    self.CurrentSelectedWalletType = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetWalletTypes();

        function GetWalletTypes() {
            $.ajax({
                type: "GET",
                url: "/WalletType/GetWalletTypes",
                data: { Id: self.ApplicationUser.Id },
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    console.log(response);
                    self.WalletTypes = response && response.data ? response.data : [];
                    loadWalletTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getStatusBadge(type) {
            return type.IsActive
                ? '<span class="badge bg-success">Active</span>'
                : `<button data-type-id="${type.Id}" class="badge bg-warning activate-type">Inactive</button>`;
        }
        function loadWalletTypes() {
            const tbody = $('#walletTypesBody').empty();
            const cardsContainer = $('#mobileWalletTypesCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.WalletTypes.length > 0) {
                self.WalletTypes.forEach(function (type) {
                    const statusBudge = getStatusBadge(type);
                    const actionButtons = type.IsActive
                        ? `<button class="btn btn-sm btn-outline-warning edit-type" data-type-id="${type.Id}"><i class="fas fa-edit"></i></button>
                           <button class="btn btn-sm btn-outline-danger delete-type" data-type-id="${type.Id}"><i class="fas fa-trash"></i></button>
                           `
                        : `<button class="btn btn-sm btn-outline-warning edit-type" data-type-id="${type.Id}"><i class="fas fa-edit"></i></button>
                        `;
                    const row =
                        `<tr>
                                <td>#${type.Id}</td>
                                <td>${type.Name}</td>
                                <td>${type.Code}</td>
                                <td>${formatDate(type.CreatedOn)}</td>
                                <td>${formatDate(type.ModifiedOn)}</td>
                                <td>${statusBudge}</td>
                                <td>${actionButtons}</td>
                            </tr>`;
                    tbody.append(row);

                    const typeHtml = `
                            <div class="card mb-3">
                                <div class="card-body">
                                    <p><strong>ID:</strong> #${type.Id}</p>
                                    <p><strong>Name:</strong> ${type.Name}</p>
                                    <p><strong>Code:</strong> ${type.Code}</p>
                                    <p><strong>Status:</strong> ${statusBudge}</p>
                                    <p><strong>Created:</strong> ${formatDate(type.CreatedOn)}</p>
                                    <p><strong>Modified:</strong> ${formatDate(type.ModifiedOn)}</p>
                                </div>
                                <div class="card-footer d-flex justify-content-between">
                                ${actionButtons}</div>
                            </div>`;
                    cardsContainer.append(typeHtml);
                });
            }
            $(".se-pre-con").hide();
        }

        // Add/Edit
        $(document).on("click", "#addWalletTypeBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditWalletTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("type-id"));
            var selectedWalletType = self.WalletTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected wallet type is .." + JSON.stringify(selectedWalletType));
            self.CurrentSelectedWalletType = selectedWalletType;

            $("#Name").val(self.CurrentSelectedWalletType.Name);
            $("#Code").val(self.CurrentSelectedWalletType.Code);
            $("#IsActive").val(self.CurrentSelectedWalletType.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        // Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentSelectedWalletType = null;
            $("#viewWalletType").modal("hide");
            $("#deleteWalletType").modal("hide");
            $("#activateWalletType").modal("hide");
        });

        // Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Deleting...");
            var typeId = $(this).data("type-id");
            var selectedWalletType = self.WalletTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected wallet type is .." + JSON.stringify(selectedWalletType));
            self.CurrentSelectedWalletType = selectedWalletType;


            $("#DeleteName").val(self.CurrentSelectedWalletType.Name);
            $("#DeleteCode").val(self.CurrentSelectedWalletType.Code);
            $("#DeleteStatus").val(self.CurrentSelectedWalletType.IsActive ? "true" : "false");


            $("#deleteWalletType").modal("show");
        });

        $(document).on("click", "#deleteWalletTypeBtn", function () {
            console.log("delete yes clicked");

            $.ajax({
                type: "DELETE",
                url: "/WalletType/DeleteWalletType",
                data: { Id: self.CurrentSelectedWalletType.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedWalletType = null;
                    $("#deleteWalletType").modal("hide");
                    GetWalletTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });

        //View
        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("walletType-id"));
            var selectedWalletType = self.WalletTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected wallet type is .." + JSON.stringify(selectedWalletType));
            self.CurrentSelectedWalletType = selectedWalletType;

            $("#ViewName").val(self.CurrentSelectedWalletType.Name);
            $("#ViewCode").val(self.CurrentSelectedWalletType.Code);
            $("#ViewCreatedBy").val(self.CurrentSelectedWalletType.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedWalletType.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedWalletType.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedWalletType.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedWalletType.IsActive);
            $("#viewWalletType").modal("show");
        });

        $('#AddEditWalletTypeForm').on('submit', function (e) {
            e.preventDefault();
            $(".se-pre-con").show();
            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(name);

            var walletType = {
                Id: self.CurrentSelectedWalletType ? self.CurrentSelectedWalletType.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn:new Date(),
                IsActive: isActive
            };
            console.log("walletType..." + JSON.stringify(walletType));

            $.ajax({
                type: "POST",
                url: "/WalletType/SaveWalletType",
                cache: false,
                data: JSON.stringify(walletType),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedWalletType = null;
                    $('#AddEditWalletTypeForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetWalletTypes();
                },
                error: function (error) {
                    console.log(error);
                }

            });
        });

        //activate
        $(document).on("click", ".activate-type", function () {
            console.log("inactive......");
            var typeId = parseInt($(this).data("type-id"));
            var selectedWalletType = self.WalletTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected wallet type is..." + JSON.stringify(selectedWalletType));
            self.CurrentSelectedWalletType = selectedWalletType;

            $("#activeName").val(self.CurrentSelectedWalletType.Name);
            $("#activeCode").val(self.CurrentSelectedWalletType.Code);
            $("#activeCreatedBy").val(self.CurrentSelectedWalletType.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentSelectedWalletType.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentSelectedWalletType.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentSelectedWalletType.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentSelectedWalletType.IsActive);
            $("#activateWalletType").modal("show");
        });

        $(document).on("click", "#activateWalletTypeBtn", function () {
            self.CurrentSelectedWalletType.ModifiedBy = self.ApplicationUser.Id;
            console.log("activate button yes clicked");

            $.ajax({
                type: "POST",
                url: "/WalletType/SaveWalletType",
                data: JSON.stringify(self.CurrentSelectedWalletType),
                cache: false,
                contentType: 'application/json',

                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedWalletType = null;
                    $("#activateWalletType").modal("hide");
                    GetWalletTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    }
}