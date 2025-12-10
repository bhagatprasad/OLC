function NewsLetterController() {
    var self = this;
    self.ApplicationUser = {};
    self.NewsLetters = [];
    self.CurrentSelectedNewsLetter = null;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        console.log(appUserInfo);
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        GetNewsLetters();

        function GetNewsLetters() {
            $.ajax({
                type: "GET",
                url: "/NewsLetter/GetNewsLetters",
                success: function (response) {
                    console.log(response);
                    self.NewsLetters = response && response.data ? response.data : [];
                    loadNewsLetters();
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
        function loadNewsLetters() {
            const tbody = $('#newsLettersBody');
            const cardsContainer = $('#mobileNewsLettersCards');
            tbody.empty();
            cardsContainer.empty();

            if (self.NewsLetters.length > 0) {
                self.NewsLetters.forEach(function (type) {
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
                                <td>#${type.Email}</td>
                                <td>${formatDate(type.SubscribedOn)}</td>
                                <td>${formatDate(type.UnsubscribedOn)}</td>
                                <td>${formatDate(type.CreatedOn)}</td>
                                <td>${formatDate(type.ModifiedOn)}</td>
                                <td>${statusBadge}</td>
                            </tr>`;
                    tbody.append(row);
                    const typeHtml = `
                            <div class="card mb-3">
                                <div class="card-body">
                                    <p><strong>ID:</strong> #${type.Id}</p>
                                    <p><strong>Email:</strong> #${type.Email}</p>
                                    <p><strong>SubscribedOn:</strong> ${type.SubscribedOn}</p>
                                    <p><strong>UnsubscribedOn:</strong> ${type.UnsubscribedOn}</p>
                                    <p><strong>Status:</strong> ${statusBadge}</p>
                                    <p><strong>CreatedOn:</strong> ${formatDate(type.CreatedOn)}</p>
                                    <p><strong>ModifiedOn:</strong> ${formatDate(type.ModifiedOn)}</p>
                                </div>
                            </div>`;
                    cardsContainer.append(typeHtml);
                });
            }
            $(".se-pre-con").hide();
        }

        $(document).on("click", ".view-type", function () {
            console.log("Hoooooo");
            var typeId = parseInt($(this).data("newsLetter-id"));
            var selectedNewsLetter = self.NewsLetters.filter(x => x.Id === typeId)[0];
            console.log("current selected block chian is .." + JSON.stringify(selectedNewsLetter));
            self.CurrentSelectedNewsLetter = selectedNewsLetter;

            $("#ViewEmail").val(self.CurrentSelectedNewsLetter.Email);
            $("#ViewSubscribedOn").val(self.CurrentSelectedNewsLetter.SubscribedOn);
            $("#ViewUnsubscribedOn").val(self.CurrentSelectedNewsLetter.UnsubscribedOn);
            $("#ViewCreatedBy").val(self.CurrentSelectedNewsLetter.CreatedBy || '');
            $("#ViewCreatedOn").val(formatDate(self.CurrentSelectedNewsLetter.CreatedOn));
            $("#ViewModifiedBy").val(self.CurrentSelectedNewsLetter.ModifiedBy || '');
            $("#ViewModifiedOn").val(formatDate(self.CurrentSelectedNewsLetter.ModifiedOn));
            $("#ViewIsActive").prop('checked', self.CurrentSelectedNewsLetter.IsActive);
            $("#viewBlockChain").modal("show");
        });

        //join button click

        $(document).on("click", "#btnSubscribeNewsLetter", function () {
            console.log("im getting from join button click");
        });

        $(document).on('click', '#btnSubscribeNewsLetter', function (e) {

            e.preventDefault();

            $(".se-pre-con").show();

            var email = $("#txtSubscribeNewsLetter").val();

            console.log(email);

            var newsLetter = {

                Id: 0,

                Email: email,

                SubscribedOn: new Date(),

                UnsubscribedOn: null,

                CreatedBy: -1,

                ModifiedBy: -1,

                CreatedOn: new Date(),

                ModifiedOn: new Date(),

                IsActive: isActive

            };

            console.log("newsLetter..." + JSON.stringify(newsLetter));

            $.ajax({

                type: "POST",

                url: "/NewsLetter/InsertNewsLetter",

                cache: false,

                data: JSON.stringify(newsLetter),

                contentType: 'application/json',

                dataType: 'json',

                success: function (response) {

                    console.log(response);

                    $(".se-pre-con").hide();

                },

                error: function (error) {

                    console.log(error);

                    $(".se-pre-con").hide();

                }

            });

        });

        //activate
        $(document).on("click", ".activate-type", function () {
            console.log("inactive......");
            var typeId = parseInt($(this).data("type-id"));
            var selectedNewsLetter = self.NewsLetters.filter(x => x.Id === typeId)[0];
            console.log("current selected news Letter is..." + JSON.stringify(selectedNewsLetter));
            self.CurrentSelectedNewsLetter = selectedNewsLetter;

            $("#activateEmail").val(self.CurrentSelectedNewsLetter.Email);
            $("#activateSubscribedOn").val(self.CurrentSelectedNewsLetter.SubscribedOn);
            $("#activateUnsubscribedOn").val(self.CurrentSelectedNewsLetter.UnsubscribedOn);
            $("#activeCreatedBy").val(self.CurrentSelectedNewsLetter.CreatedBy || '');
            $("#activeCreatedOn").val(formatDate(self.CurrentSelectedNewsLetter.CreatedOn));
            $("#activeModifiedBy").val(self.CurrentSelectedNewsLetter.ModifiedBy || '');
            $("#activeModifiedOn").val(formatDate(self.CurrentSelectedNewsLetter.ModifiedOn));
            $("#activeIsActive").prop('checked', self.CurrentSelectedNewsLetter.IsActive);
            $("#activateNewsLetter").modal("show");
        });

        $(document).on("click", "#activateNewsLetterBtn", function () {
            self.CurrentSelectedNewsLetter.ModifiedBy = self.ApplicationUser.Id;
            console.log("activate button yes clicked");

            $.ajax({
                type: "POST",
                url: "/NewsLetter/InsertNewsLetter",
                data: JSON.stringify(self.CurrentSelectedNewsLetter),
                cache: false,
                contentType: 'application/json',

                success: function (response) {
                    console.log(response);
                    self.CurrentSelectedNewsLetter = null;
                    $("#activateNewsLetter").modal("hide");
                    GetNewsLetters();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    }
}