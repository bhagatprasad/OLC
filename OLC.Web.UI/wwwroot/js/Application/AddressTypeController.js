function AddressTypeController() {
    var self = this;
    self.ApplicationUser = {};
    self.AddressTypes = [];
    self.CurrentSelectedAddressType = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetAddressTypes();

        function GetAddressTypes() {
            $.ajax({
                type: "GET",
                url: "/AddressType/GetAddressTypes",
                data: { Id: self.ApplicationUser.Id },
                cache:false,
                success: function (response) {
                    console.log(response);
                    self.AddressTypes = response && response.data ? response.data : [];
                    loadAddressTypes();
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

        function loadAddressTypes() {
            const tbody = $('#addressTypesBody').empty();
            const cardsContainer = $('#mobileAddressTypesCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.AddressTypes.length > 0) {
                self.AddressTypes.forEach(function (type) {
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
        $(document).on("click", "#addAddressTypeBtn", function () {                        
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditAddressTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("addresstype-id"));
            var selectedAddressType = self.AddressTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected address type is .." + JSON.stringify(selectedAddressType));
            self.CurrentSelectedAddressType = selectedAddressType;

            $("#Name").val(self.CurrentSelectedAddressType.Name);
            $("#Code").val(self.CurrentSelectedAddressType.Code);
            $("#IsActive").val(self.CurrentSelectedAddressType.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });


        // Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentSelectedAddressType = null;
            $("#viewAddressType").model("hide");
            $("#deleteAddressType").model("hide");
        });


        // Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Deleting...");
            var typeId = $(this).data("type-id");
            var selectedAddressType = self.AddressTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected address type is .." + JSON.stringify(selectedAddressType));
            self.CurrentSelectedAddressType = selectedAddressType;


            $("#DeleteName").val(self.CurrentSelectedAddressType.Name);
            $("#DeleteCode").val(self.CurrentSelectedAddressType.Code);
            $("#DeleteStatus").val(self.CurrentSelectedAddressType.IsActive ? "true" : "false");


            $("#deleteAddressType").modal("show");
        });

        $(document).on("click", "#deleteAddressTypeBtn", function () {
            console.log("delete yes clicked");
            
            $.ajax({
                type: "DELETE",
                url: "/AddressType/DeleteAddressType",
                data: { addressTypeId: self.CurrentSelectedAddressType.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedAddressType = null;
                    $("#deleteAddressType").modal("hide");
                    GetAddressTypes();
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });


        // View
        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("addressttype-id"));
            var selectedAddressType = self.AddressTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected address type is .." + JSON.stringify(selectedAddressType));
            self.CurrentSelectedAddressType = selectedAddressType;

            $("#ViewName").val(self.CurrentSelectedAddressType.Name);
            $("#ViewCode").val(self.CurrentSelectedAddressType.Code);
            $("#ViewCreatedBy").val(self.CurrentSelectedAddressType.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedAddressType.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedAddressType.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedAddressType.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedAddressType.IsActive);
            $("#viewAddressType").modal("show");
        });

        $('#AddEditAddressTypeForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(name);

            var addressType = {
                Id: self.CurrentSelectedAddressType ? self.CurrentSelectedAddressType.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: isActive
            };
            console.log("addressType..." + JSON.stringify(addressType));

            $.ajax({
                type: "POST",
                url: "/AddressType/SaveAddressType",
                cache: false,
                data: JSON.stringify(addressType),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedAddressType = null;
                    $('#AddEditAddressTypeForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetAddressTypes();
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
            var selectedAddressType = self.AddressTypes.filter(x => x.Id === typeId)[0];
            console.log("current selected address type is .." + JSON.stringify(selectedAddressType));
            self.CurrentSelectedAddressType = selectedAddressType;

            $("#activeName").val(self.CurrentSelectedAddressType.Name);
            $("#activeCode").val(self.CurrentSelectedAddressType.Code);
            $("#activeCreatedBy").val(self.CurrentSelectedAddressType.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentSelectedAddressType.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentSelectedAddressType.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentSelectedAddressType.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentSelectedAddressType.IsActive);
            $("#activateAddressType").modal("show");
        });

        $(document).on("click", "#activateAddressTypeBtn", function () {
            self.CurrentSelectedAddressType.ModifiedBy = self.ApplicationUser.Id;
            console.log("Active button yes clicked....................");
            $.ajax({
                type: "POST",
                url: "/AddressType/ActivateAddressType/",                
                data: JSON.stringify(self.CurrentSelectedAddressType),
                cache: false,
                contentType: 'application/json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedAddressType = null;
                    $("#activateAddressType").modal("hide");
                    GetAddressTypes();
                },
                error: function (error) {
                    console.log(error);
                }
                    
            });
        });        

    };
}
