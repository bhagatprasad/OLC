function BankController() {
    var self = this;

    self.ApplicationUser = {};
    self.Banks = [];
    self.CurrentSelectedBank = null;
    self.CurrentSelectedBank = {};

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetBanks();

        function GetBanks() {
            $.ajax({
                type: "GET",
                url: "/Bank/GetBanks", 
                data: { Id: self.ApplicationUser.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.Banks = response && response.data ? response.data : [];
                    loadBanks();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

        function getStatusBadge(bank) {  
            if (bank.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-bank-id="${bank.Id}" class="badge bg-warning status-badge activiate-bank">In Active</span>`;
            }
        }

        function loadBanks() {
            const tbody = $('#banksBody');
            const cardsContainer = $('#mobileBanksCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.Banks.length > 0) {
                self.Banks.forEach(function (bank) {
                    const statusBadge = getStatusBadge(bank);

                    const actionButtons = `
                        <button class="btn btn-sm btn-outline-warning edit-bank me-1" data-bank-id="${bank.Id}" title="edit bank">
                            <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-outline-danger delete-bank" data-bank-id="${bank.Id}" title="delete bank">
                            <i class="fas fa-trash"></i>
                        </button>
                    `;

                    const row = `
                        <tr>
                            <td>#${bank.Id}</td>
                            <td>${bank.Name || ''}</td>
                            <td>${bank.Code || ''}</td>
                            <td>${formatDate(bank.CreatedOn)}</td>
                            <td>${formatDate(bank.ModifiedOn)}</td>
                            <td>${statusBadge}</td>
                            <td>${actionButtons}</td>
                        </tr>
                    `;
                    tbody.append(row);

                    const bankHtml = `
                        <div class="card mb-3 pt-2">
                            <div class="card-body">
                                <p class="card-text mb-1"><strong>ID:</strong> #${bank.Id}</p>
                                <p class="card-text mb-1"><strong>Name:</strong> ${bank.Name}</p>
                                <p class="card-text mb-1"><strong>Code:</strong> ${bank.Code}</p>
                                <p class="card-text mb-1"><strong>Status:</strong> ${statusBadge}</p>
                                <p class="card-text mb-1"><strong>CreatedOn:</strong> ${formatDate(bank.CreatedOn)}</p>
                                <p class="card-text mb-1"><strong>ModifiedOn:</strong> ${formatDate(bank.ModifiedOn)}</p>
                            </div>
                            <div class="card-footer d-flex justify-content-between">
                                ${actionButtons}
                            </div>
                        </div>`;
                    cardsContainer.append(bankHtml);  
                });
            }
        }

        $(document).on("click", ".activiate-bank", function () {
            var bankId = $(this).data("bank-id");
            console.log(bankId);
        });

        $(document).on("click", "#addBankBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditBankForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-bank", function () {
            var bankId = parseInt($(this).data("bank-id"));  
            var selectedBank = self.Banks.filter(x => x.Id === bankId)[0];
            console.log("current selected bank is .." + JSON.stringify(selectedBank));
            self.CurrentSelectedBank = selectedBank;

            $("#Name").val(self.CurrentSelectedBank.Name);
            $("#Code").val(self.CurrentSelectedBank.Code);
            $("#IsActive").val(self.CurrentSelectedBank.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", ".btn-view-bank-close", function () {
            self.CurrentSelectedBank = null;
            $("#viewBank").modal("hide");
            $("#deleteBank").modal("hide");
        });

        $(document).on("click", ".delete-bank", function () {
            console.log("deleting...");
            var bankId = parseInt($(this).data("bank-id"));  
            var selectedBank = self.Banks.filter(x => x.Id === bankId)[0];
            console.log("current selected bank is .." + JSON.stringify(selectedBank));
            self.CurrentSelectedBank = selectedBank;

            $("#DeleteName").val(self.CurrentSelectedBank.Name);
            $("#DeleteCode").val(self.CurrentSelectedBank.Code);
            $("#DeleteStatus").val(self.CurrentSelectedBank.IsActive ? "true" : "false");

            $("#deleteBank").modal("show");
        });

        $(document).on("click", "#deleteBankBtn", function () {
            console.log("delete yes clicked");
            $.ajax({
                type: "DELETE",
                url: "/Bank/DeleteBank?bankId=" + self.CurrentSelectedBank.Id,  
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedBank = null;
                    $("#deleteBank").modal("hide");
                    GetBanks();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });

        $(document).on("click", ".view-bank", function () {
            console.log("Hoooooo");
            var bankId = parseInt($(this).data("bank-id"));  
            var selectedBank = self.Banks.filter(x => x.Id === bankId)[0];
            console.log("current selected bank is .." + JSON.stringify(selectedBank));
            self.CurrentSelectedBank = selectedBank;

            $("#ViewName").val(self.CurrentSelectedBank.Name);
            $("#ViewCode").val(self.CurrentSelectedBank.Code);
            $("#ViewCreatedBy").val(self.CurrentSelectedBank.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedBank.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedBank.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedBank.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedBank.IsActive);

            $("#viewBank").modal("show");
        });

        $('#AddEditBankForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var name = $("#Name").val();
            var code = $("#Code").val();
            var isActive = $("#IsActive").val() === "true";  
            console.log(name);

            var bank = {
                Id: self.CurrentSelectedBank ? self.CurrentSelectedBank.Id : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: isActive  
            };
            console.log("bank..." + JSON.stringify(bank));

            $.ajax({
                type: "POST",
                url: "/Bank/SaveBank", 
                cache: false,
                data: JSON.stringify(bank),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedBank = null;
                    $('#AddEditBankForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetBanks();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    };
}