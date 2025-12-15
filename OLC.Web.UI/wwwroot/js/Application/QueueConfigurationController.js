function QueueConfigurationController() {
    var self = this;
    self.ApplicationUser = {};
    self.QueueConfigurations = {};
    self.CurrentQueueConfiguration = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetAllQueueConfigurations();

        function GetAllQueueConfigurations() {
            $.ajax({
                type: "GET",
                url: "/QueueConfiguration/GetAllQueueConfigurations",
                data: { Id: self.ApplicationUser.Id },
                cache: false,
                dataType: "json",
                contentType: "application/json",

                success: function (response) {
                    console.log(response);
                    self.QueueConfigurations = response && response.data ? response.data : [];
                    loadQueueConfigurations();
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
        function loadQueueConfigurations() {
            const tbody = $('#queueConfiguratonsBody');
            const cardsContainer = $('#mobileQueueConfiguratonsCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.QueueConfigurations.length > 0) {
                self.QueueConfigurations.forEach(function (type) {
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
                                <td>${type.Key}</td>
                                <td>${type.Value}</td>
                                <td>${type.DataType}</td>
                                <td>${type.Description}</td>
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
                                    <p><strong>Key:</strong> ${type.Key}</p>
                                    <p><strong>Value:</strong> ${type.Value}</p
                                    <p><strong>DataType:</strong> ${type.DataType}</p>
                                    <p><strong>Description:</strong> ${type.Description}</p>
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
        $(document).on("click", "#addQueueConfigurationBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });
        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditQueueConfigurationForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("type-id"));
            var selectedQueueConfiguration = self.QueueConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected queue Configuration is .." + JSON.stringify(selectedQueueConfiguration));
            self.CurrentQueueConfiguration = selectedQueueConfiguration;

            $("#Key").val(self.CurrentQueueConfiguration.Key);
            $("#Value").val(self.CurrentQueueConfiguration.Value);
            $("#DataType").val(self.CurrentQueueConfiguration.DataType);
            $("#Description").val(self.CurrentQueueConfiguration.Description);
            $("#IsActive").val(self.CurrentQueueConfiguration.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });
        // Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentQueueConfiguration = null;
            $("#viewQueueConfiguration").modal("hide");
            $("#deleteQueueConfiguration").modal("hide");
            $("#activateQueueConfiguration").modal("hide");
        });
        // Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Deleting...");
            var typeId = $(this).data("type-id");
            var selectedQueueConfiguration = self.QueueConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected queue Configuration is .." + JSON.stringify(selectedQueueConfiguration));
            self.CurrentQueueConfiguration = selectedQueueConfiguration;


            $("#DeleteKey").val(self.CurrentQueueConfiguration.Key);
            $("#DeleteValue").val(self.CurrentQueueConfiguration.Value);
            $("#DeleteDataType").val(self.CurrentQueueConfiguration.DataType);
            $("#DeleteDescription").val(self.CurrentQueueConfiguration.Description);
            $("#DeleteStatus").val(self.CurrentQueueConfiguration.IsActive ? "true" : "false");


            $("#deleteQueueConfiguration").modal("show");
        });
        $(document).on("click", "#deleteQueueConfigurationBtn", function () {
            console.log("delete yes clicked");

            $.ajax({
                type: "DELETE",
                url: "/QueueConfiguration/DeleteQueueConfiguration",
                data: { Id: self.CurrentQueueConfiguration.Id },
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentQueueConfiguration = null;
                    $("#deleteQueueConfiguration").modal("hide");
                    GetQueueConfigurations();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
        //View
        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("queueConfiguration-id"));
            var selectedQueueConfiguration = self.QueueConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected queue Configuration is .." + JSON.stringify(selectedQueueConfiguration));
            self.CurrentQueueConfiguration = selectedQueueConfiguration;

            $("#ViewKey").val(self.CurrentQueueConfiguration.Key);
            $("#ViewValue").val(self.CurrentQueueConfiguration.Value);
            $("#ViewDataType").val(self.CurrentQueueConfiguration.DataType);
            $("#ViewDescription").val(self.CurrentQueueConfiguration.Description);
            $("#ViewCreatedBy").val(self.CurrentQueueConfiguration.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentQueueConfiguration.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentQueueConfiguration.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentQueueConfiguration.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentQueueConfiguration.IsActive);
            $("#viewQueueConfiguration").modal("show");
        });
        $('#AddEditQueueConfigurationForm').on('submit', function (e) {
            e.preventDefault();
            $(".se-pre-con").show();
            var key = $("#Key").val();
            var value = $("#Value").val();
            var dataType = $("#DataType").val();
            var description = $("#Description").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(key);

            var queueConfiguration = {
                Id: self.CurrentQueueConfiguration ? self.CurrentQueueConfiguration.Id : 0,
                Key: key,
                Value: value,
                DataType: dataType,
                Description: description,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn: new Date(),
                IsActive: isActive
            };
            console.log("queueConfiguration..." + JSON.stringify(queueConfiguration));

            $.ajax({
                type: "POST",
                url: "/QueueConfiguration/SaveQueueConfiguration",
                cache: false,
                data: JSON.stringify(queueConfiguration),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentQueueConfiguration = null;
                    $('#AddEditQueueConfigurationForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetQueueConfigurations();
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
            var selectedQueueConfiguration = self.QueueConfigurations.filter(x => x.Id === typeId)[0];
            console.log("current selected wallet type is..." + JSON.stringify(selectedQueueConfiguration));
            self.CurrentQueueConfiguration = selectedQueueConfiguration;

            $("#activeKey").val(self.CurrentQueueConfiguration.Key);
            $("#activeValue").val(self.CurrentQueueConfiguration.Value);
            $("#activeDataType").val(self.CurrentQueueConfiguration.DataType);
            $("#activeDescription").val(self.CurrentQueueConfiguration.Description);
            $("#activeCreatedBy").val(self.CurrentQueueConfiguration.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentQueueConfiguration.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentQueueConfiguration.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentQueueConfiguration.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentQueueConfiguration.IsActive);
            $("#activateQueueConfiguration").modal("show");
        });
        $(document).on("click", "#activateQueueConfigurationBtn", function () {
            self.CurrentQueueConfiguration.ModifiedBy = self.ApplicationUser.Id;
            console.log("activate button yes clicked");

            $.ajax({
                type: "POST",
                url: "/QueueConfiguration/SaveQueueConfiguration",
                data: JSON.stringify(self.CurrentQueueConfiguration),
                cache: false,
                contentType: 'application/json',

                success: function (response) {
                    console.log(response);
                    self.CurrentQueueConfiguration = null;
                    $("#activateQueueConfiguration").modal("hide");
                    GetQueueConfigurations();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    }
}