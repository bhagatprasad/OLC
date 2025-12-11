function EmailTemplateController() {
    var self = this;
    self.ApplicationUser = {};
    self.EmailTemplates = [];
    self.CurrentSelectedEmailTemplate = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetAllEmailTemplates();

        function GetAllEmailTemplates() {
            $.ajax({
                type: "GET",
                url: "/EmailTemplate/GetAllEmailTemplates",
                cache: false,
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    self.EmailTemplates = response && response.data ? response.data : [];
                    loadEmailTemplates();
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
        function getStatusBadge(templateType) {
            return templateType.IsActive
                ? '<span class="badge bg-success">Active</span>'
                : `<button data-type-id="${templateType.Id}" class="badge bg-warning activate-type">Inactive</button>`;
        }

        function loadEmailTemplates() {
            const tbody = $('#emailTemplatesBody');
            const cardsContainer = $('#mobileEmailTemplatesCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.EmailTemplates.length > 0) {
                self.EmailTemplates.forEach(function (templateType) {
                    const statusBudge = getStatusBadge(templateType);
                    const actionButtons = templateType.IsActive
                        ? `<button class="btn btn-sm btn-outline-warning edit-type" data-type-id="${templateType.Id}"><i class="fas fa-edit"></i></button>
                           <button class="btn btn-sm btn-outline-danger delete-type" data-type-id="${templateType.Id}"><i class="fas fa-trash"></i></button>
                           `
                        : `<button class="btn btn-sm btn-outline-warning edit-type" data-type-id="${templateType.Id}"><i class="fas fa-edit"></i></button>
                        `;
                    const row =
                        `<tr>
                                <td>#${templateType.Id}</td>
                                <td>${templateType.Name}</td>
                                <td>${templateType.Code}</td>
                                <td>${templateType.Template}</td>
                                <td>${formatDate(templateType.CreatedOn)}</td>
                                <td>${formatDate(templateType.ModifiedOn)}</td>
                                <td>${statusBudge}</td>
                                <td>${actionButtons}</td>
                            </tr>`;
                    tbody.append(row);

                    const typeHtml = `
                            <div class="card mb-3">
                                <div class="card-body">
                                    <p><strong>ID:</strong> #${templateType.Id}</p>
                                    <p><strong>Name:</strong> ${templateType.Name}</p>
                                    <p><strong>Code:</strong> ${templateType.Code}</p>
                                    <p><strong>Template:</strong> ${templateType.Template}</p>
                                    <p><strong>Status:</strong> ${statusBudge}</p>
                                    <p><strong>Created:</strong> ${formatDate(templateType.CreatedOn)}</p>
                                    <p><strong>Modified:</strong> ${formatDate(templateType.ModifiedOn)}</p>
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
        $(document).on("click", "#addEmailTemplateBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });
        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditEmailTemplateForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $(document).on("click", ".edit-type", function () {
            var typeId = parseInt($(this).data("type-id"));
            var selectedEmailTemplate = self.EmailTemplates.filter(x => x.Id === typeId)[0];
            console.log("current selected Email Template is .." + JSON.stringify(selectedEmailTemplate));
            self.CurrentSelectedEmailTemplate = selectedEmailTemplate;

            $("#Name").val(self.CurrentSelectedEmailTemplate.Name);
            $("#Code").val(self.CurrentSelectedEmailTemplate.Code);
            $("#Template").val(self.CurrentSelectedEmailTemplate.Template);
            $("#IsActive").val(self.CurrentSelectedEmailTemplate.IsActive ? "true" : "false");

            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("im getting from add button click");
        });

        // Close modals
        $(document).on("click", ".btn-view-type-close", function () {
            self.CurrentSelectedEmailTemplate = null;
            $("#viewEmailTemplate").modal("hide");
            $("#deleteEmailTemplate").modal("hide");
            $("#activateEmailTemplate").modal("hide");
        });

        // Delete
        $(document).on("click", ".delete-type", function () {
            console.log("Deleting...");
            var typeId = $(this).data("type-id");
            var selectedEmailTemplate = self.EmailTemplates.filter(x => x.Id === typeId)[0];
            console.log("current selected email template is .." + JSON.stringify(selectedEmailTemplate));
            self.CurrentSelectedEmailTemplate = selectedEmailTemplate;


            $("#DeleteName").val(self.CurrentSelectedEmailTemplate.Name);
            $("#DeleteCode").val(self.CurrentSelectedEmailTemplate.Code);
            $("#DeleteTemplate").val(self.CurrentSelectedEmailTemplate.Template);
            $("#DeleteStatus").val(self.CurrentSelectedEmailTemplate.IsActive ? "true" : "false");


            $("#deleteEmailTemplate").modal("show");
        });

        $(document).on("click", "#deleteEmailTemplateBtn", function () {
            console.log("delete yes clicked");

            $.ajax({
                type: "DELETE",
                url: "/EmailTemplate/DeleteEmailTemplate/" + self.CurrentSelectedEmailTemplate.Id,
               /* data: { Id: self.CurrentSelectedEmailTemplate.Id },*/
                cache: false,
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedEmailTemplate = null;
                    $("#deleteEmailTemplate").modal("hide");
                    GetAllEmailTemplates();
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });

        //View
        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("emailTemplate-id"));
            var selectedEmailTemplate = self.EmailTemplates.filter(x => x.Id === typeId)[0];
            console.log("current selected email template is .." + JSON.stringify(selectedEmailTemplate));
            self.CurrentSelectedEmailTemplate = selectedEmailTemplate;

            $("#ViewName").val(self.CurrentSelectedEmailTemplate.Name);
            $("#ViewCode").val(self.CurrentSelectedEmailTemplate.Code);
            $("#ViewTemplate").val(self.CurrentSelectedEmailTemplate.Template);
            $("#ViewCreatedBy").val(self.CurrentSelectedEmailTemplate.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedEmailTemplate.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedEmailTemplate.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedEmailTemplate.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedEmailTemplate.IsActive);
            $("#viewEmailTemplate").modal("show");
        });

        $('#AddEditEmailTemplateForm').on('submit', function (e) {
            e.preventDefault();
            $(".se-pre-con").show();
            var name = $("#Name").val();
            var code = $("#Code").val();
            var template = $("#Template").val();
            var isActive = $("#IsActive").val() === "true";
            console.log(name);

            var emailTemplate = {
                Id: self.CurrentSelectedEmailTemplate ? self.CurrentSelectedEmailTemplate.Id : 0,
                Name: name,
                Code: code,
                Template: template,
                CreatedBy: self.ApplicationUser.Id,
                ModifiedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn: new Date(),
                IsActive: isActive
            };
            console.log("emailTemplate..." + JSON.stringify(emailTemplate));

            $.ajax({
                type: "POST",
                url: "/EmailTemplate/SaveEmailTemplate",
                cache: false,
                data: JSON.stringify(emailTemplate),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedEmailTemplate = null;
                    $('#AddEditEmailTemplateForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetAllEmailTemplates();
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
            var selectedEmailTemplate = self.EmailTemplates.filter(x => x.Id === typeId)[0];
            console.log("current selected email template is..." + JSON.stringify(selectedEmailTemplate));
            self.CurrentSelectedEmailTemplate = selectedEmailTemplate;

            $("#activeName").val(self.CurrentSelectedEmailTemplate.Name);
            $("#activeCode").val(self.CurrentSelectedEmailTemplate.Code);
            $("#activeTemplate").val(self.CurrentSelectedEmailTemplate.Template);
            $("#activeCreatedBy").val(self.CurrentSelectedEmailTemplate.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentSelectedEmailTemplate.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentSelectedEmailTemplate.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentSelectedEmailTemplate.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentSelectedEmailTemplate.IsActive);
            $("#activateEmailTemplate").modal("show");
        });

        $(document).on("click", "#activateEmailTemplateBtn", function () {
            self.CurrentSelectedEmailTemplate.ModifiedBy = self.ApplicationUser.Id;
            console.log("activate button yes clicked");

            $.ajax({
                type: "POST",
                url: "/EmailTemplate/SaveEmailTemplate",
                data: JSON.stringify(self.CurrentSelectedEmailTemplate),
                cache: false,
                contentType: 'application/json',

                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedEmailTemplate = null;
                    $("#activateEmailTemplate").modal("hide");
                    GetAllEmailTemplates();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    }
}