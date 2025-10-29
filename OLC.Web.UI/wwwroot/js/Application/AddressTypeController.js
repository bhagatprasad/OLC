function AddressTypeController() {
    var self = this;

    self.ApplicationUser = {};
    self.AddressTypes = [];
    self.CurrentSelectedAddressType = null;
    self.CurrentSelectedAddressType = {};

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
                cache: false,
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

        function getStatusBadge(addresstype) {
            if (addresstype.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-addresstype-id="${addresstype.Id}" class="badge bg-warning status-badge activiate-addresstype">In Active</span>`;
            }

        }

        function loadAddressTypes() {
            const tbody = $('#addressTypesBody');
            const cardsContainer = $('#mobileAddressTypesCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.AddressTypes.length > 0) {
                self.AddressTypes.forEach(function (type) {
                    const statusBadge = getStatusBadge(type);

                    const actionButtons = `
                
                <button class="btn btn-sm btn-outline-warning edit-addresstype me-1" data-id="${type.Id}">
                        <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-addresstype" data-id="${type.Id}">
                        <i class="fas fa-trash"></i>
                </button>
            `;

                    // Desktop row
                    const row = `
                <tr>
                <td>#${type.Id}</td>
                <td>${type.Name || ''}</td>
                <td>${type.Code || ''}</td>
                <td>${formatDate(type.CreatedOn)}</td>
                <td>${formatDate(type.ModifiedOn)}</td>
                <td>${statusBadge}</td>
                <td>${actionButtons}</td>
                </tr>`;
                    tbody.append(row);

                    // Mobile card
                    const typeHtml = `
                <div class="card mb-3 pt-2">
                        <div class="card-header"><strong>${type.Name}</strong></div>
                        <div class="card-body">
                            <p><strong>Code:</strong> ${type.Code}</p>
                            <p><strong>Status:</strong> ${statusBadge}</p>
                            <p><strong>Created:</strong> ${formatDate(type.CreatedOn)}</p>
                            <p><strong>Modified:</strong> ${formatDate(type.ModifiedOn)}</p>
                        </div>
                        <div class="card-footer d-flex justify-content-between">
                            ${actionButtons}
                        </div>
                </div>`;
                    cardsContainer.append(typeHtml);
                });
            }

        }

        // ===================== ADD ADDRESS TYPE =====================
        $(document).on("click", "#addAddressTypeBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        // ===================== CLOSE SIDEBAR =====================
        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditAddressTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        // ===================== EDIT ADDRESS TYPE =====================
        $(document).on("click", ".edit-addresstype", function () {
            var addressTypeId = $(this).data("addresstype-id");
            var selectedAddressType = self.AddressTypes.filter(x => x.Id == addressTypeId)[0];
            console.log("current address type is .." + JSON.stringify(selectedAddressType));
            self.CurrentSelectedAddressType = selectedAddressType;
            $("#Name").val(self.CurrentSelectedAddressType.Name);
            $("#Code").val(self.CurrentSelectedAddressType.Code);
            $("#IsActive").val(self.CurrentSelectedAddressType.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        // ===================== VIEW ADDRESS TYPE =====================
        $(document).on("click", ".btn-view-addresstype-close", function () {
            self.CurrentSelectedAddressType = null;
            $("#viewAddressType").modal("hide");
            $("#deleteAddressType").modal("hide");
        });

        // ===================== DELETE ADDRESS TYPE =====================
        $(document).on("click", ".delete-addresstype", function () {
            console.log("deleting...");
            var addressTypeId = $(this).data("addresstype-id");
            var selectedAddressType = self.AddressTypes.filter(x => x.Id == addressTypeId)[0];
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
                url: "/AddressType/DeleteAddressType?addressTypeId=" + self.CurrentSelectedAddressType.Id,
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

        $(document).on("click", ".view-addresstype", function () {
            console.log("Hoooooo");
            var addressTypeId = $(this).data("addresstype-id");
            var selectedAddressType = self.AddressTypes.filter(x => x.Id == addressTypeId)[0];
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

        // ===================== SAVE ADDRESS TYPE =====================
        $('#AddEditAddressTypeForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";
            console.log("Submitting:", name, code, isActive);

            var addressType = {
                Id: self.CurrentSelectedAddressType ? self.CurrentSelectedAddressType.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: isActive
            };
            console.log("addressType payload:", JSON.stringify(addressType));

            $.ajax({
                type: "POST",
                url: "/AddressType/SaveAddressType",
                cache: false,
                data: JSON.stringify(addressType),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    console.log("Save response:", response);
                    if (response === true) {
                        console.log("Save successful, refreshing grid...");
                        self.CurrentSelectedAddressType = null;
                        $('#AddEditAddressTypeForm')[0].reset();
                        $('#sidebar').removeClass('show');
                        $('.modal-backdrop').remove();
                        GetAddressTypes();  // This should refresh
                    } else {
                        alert("Save failed on server.");
                    }
                },
                error: function (error) {
                    console.log("Save error:", error);
                    alert("Error saving address type.");
                }
            });
        });
            
        

    }
}
