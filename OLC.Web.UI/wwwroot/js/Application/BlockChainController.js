function BlockChainController() {
    var self = this;
    self.ApplicationUser = {};
    self.BlockChains = [];
    self.CurrentSelectedBlockChain = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetBlockChains();

        function GetBlockChains() {
            $.ajax({
                type: "GET",
                url: "/BlockChain/GetBlockChains",
                data: { Id: self.ApplicationUser.Id },
                cache: false,
                dataType: "json",
                contentType: "application/json",

                success: function (response) {
                    console.log(response);
                    self.BlockChains = response && response.data ? response.data : [];
                    loadBlockChains();
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
        function loadBlockChains() {
            const tbody = $('#blockChainsBody').empty();
            const cardsContainer = $('#mobileBlockChainsCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.BlockChains.length > 0) {
                self.BlockChains.forEach(function (type) {
                    const statusBadge = getStatusBadge(type);
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
                                <td>${statusBadge}</td>
                                <td>${actionButtons}</td>
                            </tr>`;
                    tbody.append(row);
                    const typeHtml = `
                            <div class="card mb-3">
                                <div class="card-body">
                                    <p><strong>ID:</strong> #${type.Id}</p>
                                    <p><strong>Name:</strong> ${type.Name}</p>
                                    <p><strong>Code:</strong> ${type.Code}</p>
                                    <p><strong>Status:</strong> ${statusBadge}</p>
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
        $(document).on("click", "#addBlockChainBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditBlockChainForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("type-id"));
            var selectedBlockChain = self.BlockChains.filter(x => x.Id === typeId)[0];
            console.log("current selected Block Chain is .." + JSON.stringify(selectedBlockChain));
            self.CurrentSelectedBlockChain = selectedBlockChain;

            $("#Name").val(self.CurrentSelectedBlockChain.Name);
            $("#Code").val(self.CurrentSelectedBlockChain.Code);
            $("#IsActive").val(self.CurrentSelectedBlockChain.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        // Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentSelectedBlockChain = null;
            $("#viewBlockChain").modal("hide");
            $("#deleteBlockChain").modal("hide");
            $("#activateBlockChain").modal("hide");
        });

        // Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Deleting...");
            var typeId = $(this).data("type-id");
            var selectedBlockChain = self.BlockChains.filter(x => x.Id === typeId)[0];
            console.log("current selected block Chain is .." + JSON.stringify(selectedBlockChain));
            self.CurrentSelectedBlockChain = selectedBlockChain;


            $("#DeleteName").val(self.CurrentSelectedBlockChain.Name);
            $("#DeleteCode").val(self.CurrentSelectedBlockChain.Code);
            $("#DeleteStatus").val(self.CurrentSelectedBlockChain.IsActive ? "true" : "false");


            $("#deleteBlockChain").modal("show");
        });

        $(document).on("click", "#deleteBlockChainBtn", function () {
            console.log("delete yes clicked");

            $.ajax({
                type: "DELETE",
                url: "/BlockChain/DeleteBlockChain",
                data: { Id: self.CurrentSelectedBlockChain.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedBlockChain = null;
                    $("#deleteBlockChain").modal("hide");
                    GetBlockChains();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });

        //View
        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("blockChain-id"));
            var selectedBlockChain = self.BlockChains.filter(x => x.Id === typeId)[0];
            console.log("current selected block chian is .." + JSON.stringify(selectedBlockChain));
            self.CurrentSelectedBlockChain = selectedBlockChain;

            $("#ViewName").val(self.CurrentSelectedBlockChain.Name);
            $("#ViewCode").val(self.CurrentSelectedBlockChain.Code);
            $("#ViewCreatedBy").val(self.CurrentSelectedBlockChain.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedBlockChain.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedBlockChain.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedBlockChain.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedBlockChain.IsActive);
            $("#viewBlockChain").modal("show");
        });

        $('#AddEditBlockChainForm').on('submit', function (e) {
            e.preventDefault();
            $(".se-pre-con").show();
            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(name);

            var blockChain = {
                Id: self.CurrentSelectedBlockChain ? self.CurrentSelectedBlockChain.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn: new Date(),
                IsActive: isActive
            };
            console.log("blockChain..." + JSON.stringify(blockChain));

            $.ajax({
                type: "POST",
                url: "/BlockChain/InsertBlockChain",
                cache: false,
                data: JSON.stringify(blockChain),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedBlockChain = null;
                    $('#AddEditBlockChainForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetBlockChains();
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
            var selectedBlockChain = self.BlockChains.filter(x => x.Id === typeId)[0];
            console.log("current selected block chain is..." + JSON.stringify(selectedBlockChain));
            self.CurrentSelectedBlockChain = selectedBlockChain;

            $("#activeName").val(self.CurrentSelectedBlockChain.Name);
            $("#activeCode").val(self.CurrentSelectedBlockChain.Code);
            $("#activeCreatedBy").val(self.CurrentSelectedBlockChain.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentSelectedBlockChain.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentSelectedBlockChain.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentSelectedBlockChain.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentSelectedBlockChain.IsActive);
            $("#activateBlockChain").modal("show");
        });

        $(document).on("click", "#activateBlockChainBtn", function () {
            self.CurrentSelectedBlockChain.ModifiedBy = self.ApplicationUser.Id;
            console.log("activate button yes clicked");

            $.ajax({
                type: "POST",
                url: "/BlockChain/InsertBlockChain",
                data: JSON.stringify(self.CurrentSelectedBlockChain),
                cache: false,
                contentType: 'application/json',

                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedBlockChain = null;
                    $("#activateBlockChain").modal("hide");
                    GetBlockChains();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    }
}