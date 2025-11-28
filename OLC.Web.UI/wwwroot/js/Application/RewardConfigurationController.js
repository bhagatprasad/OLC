function RewardConfigurationController() {
    var self = this;
    self.ApplicationUser = {};
    self.RewardConfigurations = [];
    self.currentSelectedRewardConfiguration = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetRewardConfigurations();

        function GetRewardConfigurations() {
            $.ajax({
                type: "GET",
                url: "/RewardConfiguration/GetRewardConfigurations",
                cache: false,

                success: function (response) {
                    console.log("Data received:", response);
                    self.RewardConfigurations = response && response.data ? response.data : [];
                    loadRewardConfigurations();
                },
                error: function (xhr,status,error) {
                    console.log("AJAX error:", error);
                    alert("Failed to load data: " + err);
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

        function loadRewardConfigurations() {
            const tbody = $('#rewardConfigurationBody').empty();
            const mobileContainer = $('#mobileRewardConfigurationCards');
            tbody.empty();
            mobileContainer.empty();

            if (self.RewardConfigurations.length > 0) {
                self.RewardConfigurations.forEach(function (type) {
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
                                <td>${type.RewardName || ''}</td>
                                <td>${type.RewardType || ''}</td>
                                <td>${type.RewardValue || ''}</td>  
                                <td>${type.MinimumTransactionAmount || ''}</td>
                                <td>${type.MaximumReward || ''}</td>  
                                <td>${type.IsActive}</td>  
                                <td>${formatDate(type.ValidFrom)}</td>
                                <td>${formatDate(type.ValidTo)}</td>
                                <td>${formatDate(type.CreatedOn)}</td>
                                <td>${formatDate(type.ModifiedOn)}</td>
                                <td>${statusBudge}</td>
                                <td>${actionButtons}</td>
                            </tr>`;
                    tbody.append(row);

                    //Mobile Card -Now Works!
                    const cardHtml = `
                            <div class="card mb-3">
                                <div class="card-body">
                                    <p><strong>ID:</strong> #${type.Id}</p>
                                    <p><strong>Name:</strong> ${type.RewardName || ''}</p>
                                    <p><strong>Type:</strong> ${type.RewardType || ''}</p>
                                    <p><strong>Value:</strong> ${type.RewardValue || ''}</p>
                                    <p><strong>Min Amount:</strong> ${type.MinimumTransactionAmount || ''}</p>
                                    <p><strong>Max Reward:</strong> ${type.MaximumReward || ''}</p>
                                    <p><strong>Status:</strong> ${type.IsActive ? 'Active' : 'Inactive'}</p>
                                    <p><strong>ValidFrom:</strong> ${type.ValidFrom ? new Date(type.ValidFrom).toLocaleDateString() : ''}</p>
                                    <p><strong>ValidTo:</strong> ${type.ValidTo ? new Date(type.ValidTo).toLocaleDateString() : ''}</p>
                                    <p><strong>Status:</strong> ${statusBudge}</p>
                                    <p><strong>Created:</strong> ${formatDate(type.CreatedOn)}</p>
                                    <p><strong>Modified:</strong> ${formatDate(type.ModifiedOn)}</p>
                                </div>
                                <div class="card-footer d-flex justify-content-between">
                                ${actionButtons}</div>
                            </div>`;
                    mobileContainer.append(cardHtml);
                });
            } 
        }

        $(document).on("click", ".activate-type", function () {
            var typeId = $(this).data("type-id");
            console.log(typeId);
        });

        //Add Edit
        $(document).on("click", "#addRewardConfigurationBtn", function () {
            self.currentSelectedRewardConfiguration = null;
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditRewardConfigurationForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("type-id"));
            var selectedRewardConfiguration = self.RewardConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected rewardConfiguration is .." + JSON.stringify(selectedRewardConfiguration));
            self.currentSelectedRewardConfiguration = selectedRewardConfiguration;

            $("#RewardName").val(self.currentSelectedRewardConfiguration.RewardName);
            $("#RewardType").val(self.currentSelectedRewardConfiguration.RewardType);
            $("#RewardValue").val(self.currentSelectedRewardConfiguration.RewardValue);
            $("#IsActive").val(self.currentSelectedRewardConfiguration.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        //Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.currentSelectedRewardConfiguration = null;
            $("#viewRewardConfiguration").modal("hide");
            $("#deleteRewardConfiguration").modal("hide");
            $("#activateRewardConfiguration").modal("hide");
        });

        //Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Delete RewardConfiguration.....");
            var typeId = $(this).data("type-id");
            var selectedRewardConfiguration = self.RewardConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected reward Configuration is....." + JSON.stringify(selectedRewardConfiguration));
            self.currentSelectedRewardConfiguration = selectedRewardConfiguration;

            $("#DeleteRewardName").val(self.currentSelectedRewardConfiguration.RewardName);
            $("#DeleteRewardType").val(self.currentSelectedRewardConfiguration.RewardType);
            $("#DeleteStatus").val(self.currentSelectedRewardConfiguration.IsActive ? "true" : false);

            $("#deleteRewardConfiguration").modal("show");
        });

        $(document).on("click", "#deleteRewardConfigurationBtn", function () {
            console.log("Delete yes button clicked...");

            $.ajax({
                type: "DELETE",
                url: "/RewardConfiguration/DeleteRewardConfiguration",
                data: { rewardConfigurationId: self.currentSelectedRewardConfiguration.Id },
                cache: false,

                success: function (response) {
                    console.log(response);
                    self.currentSelectedRewardConfiguration = null;
                    $("#deleteRewardConfiguration").modal("hide");

                    GetRewardConfigurations();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });

        //View
        $(document).on("click", ".view-type", function () {
            console.log("hiiiii.....");
            var typeId = parseInt($(this).data("rewardConfiguration-id"));
            var selectedRewardConfiguration = self.RewardConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected reward Configuration is..." + JSON.stringify(selectedRewardConfiguration));
            self.currentSelectedRewardConfiguration = selectedRewardConfiguration;

            $("#ViewRewardName").val(self.currentSelectedRewardConfiguration.RewardName);
            $("#ViewRewardType").val(self.currentSelectedRewardConfiguration.RewardType);
            $("#ViewRewardValue").val(self.currentSelectedRewardConfiguration.RewardValue);
            $("#ViewCreatedBy").val(self.currentSelectedRewardConfiguration.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.currentSelectedRewardConfiguration.CreatedOn));
            $("#ViewModifiedBy").val(self.currentSelectedRewardConfiguration.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.currentSelectedRewardConfiguration.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.currentSelectedRewardConfiguration.IsActive);
            $("#viewRewardConfiguration").modal("show");
        });

        $('#AddEditRewardConfigurationForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var rewardName = $("#RewardName").val();
            var rewardType = $("#RewardType").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(rewardName);

            var rewardConfiguration = {
                Id: self.currentSelectedRewardConfiguration ? self.currentSelectedRewardConfiguration.Id : 0,
                RewardName: rewardName,
                RewardType: rewardType,
                RewardValue: $("#RewardValue").val(),  // ← FIXED
                MinimumTransactionAmount: $("#MinTransactionAmount").val(),
                MaximumReward: $("#MaxReward").val(),
                ValidFrom: $("#ValidFrom").val(),
                ValidTo: $("#ValidTo").val(),
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: isActive
            };
            console.log("rewardConfiguration...." + JSON.stringify(rewardConfiguration));

            $.ajax({
                type: "POST",
                url: "/RewardConfiguration/SaveRewardConfiguration",
                cache: false,
                data: JSON.stringify(rewardConfiguration),
                contentType: 'application/json',
                dataType: 'json',

                success: function (response) {
                    console.log(response);
                    self.currentSelectedRewardConfiguration = null,
                        $('#AddEditRewardConfigurationForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetRewardConfigurations();
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
            var selectedRewardConfiguration = self.RewardConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected reward Configuration is..." + JSON.stringify(selectedRewardConfiguration));
            self.currentSelectedRewardConfiguration = selectedRewardConfiguration;

            $("#activeRewardName").val(self.currentSelectedRewardConfiguration.RewardName);
            $("#activeRewardType").val(self.currentSelectedRewardConfiguration.RewardType);
            $("#activeRewardValue").val(self.currentSelectedRewardConfiguration.RewardValue);
            $("#activeCreatedBy").val(self.currentSelectedRewardConfiguration.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.currentSelectedRewardConfiguration.CreatedOn));
            $("#activeModifiedBy").val(self.currentSelectedRewardConfiguration.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.currentSelectedRewardConfiguration.ModifiedOn));
            $("#activeIsActive").prop('checked', self.currentSelectedRewardConfiguration.IsActive);
            $("#activateRewardConfiguration").modal("show");
        });

        $(document).on("click", "#activateRewardConfigurationBtn", function () {
            self.currentSelectedRewardConfiguration.ModifiedBy = self.ApplicationUser.Id;
            console.log("activate button yes clicked");

            $.ajax({
                type: "POST",
                url: "/RewardConfiguration/ActivateRewardConfiguration",
                data: Json.stringify(self.currentSelectedRewardConfiguration),
                cache: false,
                contentType: 'application/json',

                success: function (response) {
                    console.log(response);
                    self.currentSelectedRewardConfiguration = null,
                        $("#activateRewardConfiguration").modal("hide"),
                        GetRewardConfigurations();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    };
}