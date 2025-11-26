function TransactionTypeController() {
    var self = this;
    self.ApplicationUser = {};
    self.TransactionTypes = [];
    self.CurrentSelectedTransactionType = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetTransactionTypes();

        function GetTransactionTypes() {
            $.ajax({
                type: "GET",
                url: "/TransactionType/GetTransactionTypes",
                data: { Id: self.ApplicationUser.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.TransactionTypes = response && response.data ? response.data : [];
                    loadTransactionTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

        function formatDate(dateStr) {
            if (!dateStr) return '';
            return new Date(dateStr).toLocaleDateString();
        }

        function getStatusBadge(type) {
            return type.IsActive
                ? '<span class="badge bg-success">Active</span>'
                : `<button data-type-id="${type.Id}" class="badge bg-warning activate-type">Inactive</button>`;
        }

        function loadTransactionTypes() {
            const tbody = $('#transactionTypesBody').empty();
            const cardsContainer = $('#mobileTransactionTypesCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.TransactionTypes.length > 0) {
                self.TransactionTypes.forEach(function (type) {
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

        }


        $(document).on("click", ".activate-type", function () {
            var typeId = $(this).data("type-id");
            console.log(typeId);
        });

        // Add/Edit
        $(document).on("click", "#addTransactionTypeBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditTransactionTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("type-id"));
            var selectedTransactionType = self.TransactionTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected Transaction type is .." + JSON.stringify(selectedTransactionType));
            self.CurrentSelectedTransactionType = selectedTransactionType;

            $("#Name").val(self.CurrentSelectedTransactionType.Name);
            $("#Code").val(self.CurrentSelectedTransactionType.Code);
            $("#IsActive").val(self.CurrentSelectedTransactionType.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });


        // Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentSelectedTransactionType = null;
            $("#viewTransactionType").modal("hide");
            $("#deleteTransactionType").modal("hide");
            $("#activateTransactionType").modal("hide");
        });


        // Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Deleting...");
            var typeId = $(this).data("type-id");
            var selectedTransactionType = self.TransactionTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected Transaction type is .." + JSON.stringify(selectedTransactionType));
            self.CurrentSelectedTransactionType = selectedTransactionType;


            $("#DeleteName").val(self.CurrentSelectedTransactionType.Name);
            $("#DeleteCode").val(self.CurrentSelectedTransactionType.Code);
            $("#DeleteStatus").val(self.CurrentSelectedTransactionType.IsActive ? "true" : "false");


            $("#deleteTransactionType").modal("show");
        });

        $(document).on("click", "#deleteTransactionTypeBtn", function () {
            console.log("delete yes clicked");

            $.ajax({
                type: "DELETE",
                url: "/TransactionType/DeleteTransactionType",
                data: { transactionTypeId: self.CurrentSelectedTransactionType.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedTransactionType = null;
                    $("#deleteTransactionType").modal("hide");
                    GetTransactionTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });


        // View
        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("Transactionttype-id"));
            var selectedTransactionType = self.TransactionTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected Transaction type is .." + JSON.stringify(selectedTransactionType));
            self.CurrentSelectedTransactionType = selectedTransactionType;

            $("#ViewName").val(self.CurrentSelectedTransactionType.Name);
            $("#ViewCode").val(self.CurrentSelectedTransactionType.Code);
            $("#ViewCreatedBy").val(self.CurrentSelectedTransactionType.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedTransactionType.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedTransactionType.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedTransactionType.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedTransactionType.IsActive);
            $("#viewTransactionType").modal("show");
        });

        $('#AddEditTransactionTypeForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(name);

            var transactionType = {
                Id: self.CurrentSelectedTransactionType ? self.CurrentSelectedTransactionType.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: isActive
            };
            console.log("TransactionType..." + JSON.stringify(transactionType));

            $.ajax({
                type: "POST",
                url: "/TransactionType/SaveTransactionType",
                cache: false,
                data: JSON.stringify(transactionType),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedTransactionType = null;
                    $('#AddEditTransactionTypeForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetTransactionTypes();
                },
                error: function (error) {
                    console.log(error);
                }

            });
        });


        // Activate
        $(document).on("click", ".activate-type", function () {
            console.log("inactive...");
            var typeId = parseInt($(this).data("type-id"));
            var selectedTransactionType = self.TransactionTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected Transaction type is .." + JSON.stringify(selectedTransactionType));
            self.CurrentSelectedTransactionType = selectedTransactionType;

            $("#activeName").val(self.CurrentSelectedTransactionType.Name);
            $("#activeCode").val(self.CurrentSelectedTransactionType.Code);
            $("#activeCreatedBy").val(self.CurrentSelectedTransactionType.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentSelectedTransactionType.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentSelectedTransactionType.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentSelectedTransactionType.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentSelectedTransactionType.IsActive);
            $("#activateTransactionType").modal("show");
        });

        $(document).on("click", "#activateTransactionTypeBtn", function () {
            self.CurrentSelectedTransactionType.ModifiedBy = self.ApplicationUser.Id;
            console.log("Active button yes clicked....................");
            $.ajax({
                type: "POST",
                url: "/TransactionType/ActivateTransactionType/",
                data: JSON.stringify(self.CurrentSelectedTransactionType),
                cache: false,
                contentType: 'application/json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedTransactionType = null;
                    $("#activateTransactionType").modal("hide");
                    GetTransactionTypes();
                },
                error: function (error) {
                    console.log(error);
                }

            });
        });

    };
}
