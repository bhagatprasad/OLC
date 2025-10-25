function UserCreditCardController() {

    var self = this;

    self.ApplicationUser = {};

    self.UserCreditCards = [];

    self.Banks = [];

    self.CardTypes = [];

    self.CurrentSelectedCreditCard = null;

    self.currentSelectedCreditCard = {};

    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');

        console.log(appUserInfo);

        if (appUserInfo) {

            self.ApplicationUser = appUserInfo;
        }

        GetUserCreditCards();
        GetBanks();
        GetCardTypes();
        function GetUserCreditCards() {
            $.ajax({
                type: "GET",
                url: "/CreditCard/GetUserCreditCards",
                data: { userId: self.ApplicationUser.Id },
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.UserCreditCards = response && response.data ? response.data : [];

                    loadUserCreditCards();

                }, error: function (error) {
                    console.log(error);
                }
            });
        };
        function GetCardTypes() {
            $.ajax({
                type: "GET",
                url: "/CardType/GetCardTypes",
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.CardTypes = response && response.data ? response.data : [];
                    genarateDropdown("CardType", self.CardTypes, "Id", "Name");

                }, error: function (error) {
                    console.log(error);
                }
            });
        };

        function GetBanks() {
            $.ajax({
                type: "GET",
                url: "/Bank/GetBanks",
                cache: false,
                success: function (response) {
                    console.log(response)
                    self.Banks = response && response.data ? response.data : [];
                    genarateDropdown("IssuingBank", self.Banks, "Id", "Name");

                }, error: function (error) {
                    console.log(error);
                }
            });
        };
        //activiate-card

        function getStatusBadge(card) {
            if (card.IsActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return `<span data-card-id="${card.Id}" class="badge bg-warning status-badge activiate-card">In Active</span>`;
            }

        }
        function loadUserCreditCards() {
            const tbody = $('#userCreditCardsBody');
            const cardsContainer = $('#mobileCreditCardsCards');
            tbody.empty(); // Clear existing desktop table rows
            cardsContainer.empty(); // Clear existing mobile cards

            if (self.UserCreditCards.length > 0) {
                self.UserCreditCards.forEach(function (card, index) {
                    const statusBadge = getStatusBadge(card);

                    const actionButtons = `
                <button class="btn btn-sm btn-outline-primary view-card me-1" data-card-id="${card.Id}" data-card='${JSON.stringify(card).replace(/'/g, "&apos;")}' title="view card">
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-sm btn-outline-warning edit-card me-1" data-card-id="${card.Id}" data-card='${JSON.stringify(card).replace(/'/g, "&apos;")}' title="edit card">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-card" data-card-id="${card.Id}" data-card='${JSON.stringify(card).replace(/'/g, "&apos;")}' title="delete card">
                    <i class="fas fa-trash"></i>
                </button>
            `;

                    // Desktop table row
                    const row = `<tr class="transaction-item">
                <td>#${card.Id}</td>
                <td>${card.EncryptedCardNumber}</td>
                <td>${card.ExpiryMonth}/${card.ExpiryYear}</td>
                <td>${card.EncryptedCVV || 'N/A'}</td>
                <td>${card.CardType}</td>
                <td>${card.IssuingBank}</td>
                <td>${statusBadge}</td>
                <td>
                   ${actionButtons}
                </td>
            </tr>`;
                    tbody.append(row);

                    // Mobile card
                    const cardHtml = `
                <div class="card mb-3 pt-2">
                    <div class="card-header">
                        <strong>${card.EncryptedCardNumber} (${card.CardType})</strong>
                    </div>
                    <div class="card-body">
                        <p class="card-text mb-1"><strong>ID:</strong> #${card.Id}</p>
                        <p class="card-text mb-1"><strong>Expiry:</strong> ${card.ExpiryMonth}/${card.ExpiryYear}</p>
                        <p class="card-text mb-1"><strong>CVV:</strong> ${card.EncryptedCVV || 'N/A'}</p>
                        <p class="card-text mb-1"><strong>Bank:</strong> ${card.IssuingBank}</p>
                        <p class="card-text mb-1"><strong>Status:</strong> ${statusBadge}</p>
                    </div>
                    <div class="card-footer d-flex justify-content-between">
                        ${actionButtons}
                    </div>
                </div>
            `;
                    cardsContainer.append(cardHtml);
                });
            }
        }


        $(document).on("click", ".activiate-card", function () {
            var cardId = $(this).data("card-id");

            console.log(cardId);
        });
        $(document).on("click", "#addCreditCardBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });

        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditUserCreditCardForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });


        $(document).on("click", ".edit-card", function () {
            var cardId = $(this).data("card-id");
            var selectedCreditCard = self.UserCreditCards.filter(x => x.Id == cardId)[0];

            console.log("current selected user credit card is .." + JSON.stringify(selectedCreditCard));

            self.CurrentSelectedCreditCard = selectedCreditCard;

            var cardType = null;

            var bank = null;


            if (self.CurrentSelectedCreditCard.CardType)
                cardType = self.CardTypes.filter(x => x.Name == self.CurrentSelectedCreditCard.CardType)[0];

            if (self.CurrentSelectedCreditCard.IssuingBank)
                bank = self.Banks.filter(x => x.Name == self.CurrentSelectedCreditCard.IssuingBank)[0];


            $("#Cardholdername").val(self.CurrentSelectedCreditCard.CardHolderName);
            $("#Cardnumber").val(self.CurrentSelectedCreditCard.EncryptedCardNumber);
            $("#Expirymonth").val(self.CurrentSelectedCreditCard.ExpiryMonth);
            $("#ExpiryYear").val(self.CurrentSelectedCreditCard.ExpiryYear);

            if (cardType)
                $("#CardType").val(cardType.Id);

            if (bank)
                $("#IssuingBank").val(bank.Id);

            $("#CVV").val(self.CurrentSelectedCreditCard.EncryptedCVV);
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');

            console.log("Iam getting from add button click");
        });


        $(document).on("click", ".btn-view-card-close", function () {
            self.CurrentSelectedCreditCard = null;
            $("#viewCreditCard").modal("hide");
            $("#deleteCreditCard").modal("hide");
        });

        $(document).on("click", ".delete-card", function () {
            console.log("deleting...");

            var cardId = $(this).data("card-id");

            var selectedCreditCard = self.UserCreditCards.filter(x => x.Id == cardId)[0];

            console.log("current selected user credit card is .." + JSON.stringify(selectedCreditCard));

            self.CurrentSelectedCreditCard = selectedCreditCard;

            $("#DeleteCardHolderName").val(self.CurrentSelectedCreditCard.CardHolderName);
            $("#DeleteCardNumber").val(self.CurrentSelectedCreditCard.EncryptedCardNumber);
            $("#DeleteExpirymonth").val(self.CurrentSelectedCreditCard.ExpiryMonth);
            $("#DeleteExpiryYear").val(self.CurrentSelectedCreditCard.ExpiryYear);
            $("#DeleteCVV").val(self.CurrentSelectedCreditCard.EncryptedCVV);

            $("#deleteCreditCard").modal("show");
        });

        $(document).on("click", "#deleteCreditCardBtn", function () {
            console.log("delete yes clicked");
            $.ajax({
                type: "DELETE",
                url: "/CreditCard/DeleteUserCredit",
                data: { creditCardId: self.CurrentSelectedCreditCard.Id },
                cache: false,
                success: function (response) {

                    console.log(response);

                    self.CurrentSelectedCreditCard = null;

                    $("#deleteCreditCard").modal("hide");

                    GetUserCreditCards();

                }, error: function (error) {
                    console.log(error);
                }
            });

        });

        $(document).on("click", ".view-card", function () {

            console.log("Hoooooo");

            var cardId = $(this).data("card-id");

            var selectedCreditCard = self.UserCreditCards.filter(x => x.Id == cardId)[0];

            console.log("current selected user credit card is .." + JSON.stringify(selectedCreditCard));

            self.CurrentSelectedCreditCard = selectedCreditCard;

            $("#ViewCardHolderName").val(self.CurrentSelectedCreditCard.CardHolderName);
            $("#ViewCardNumber").val(self.CurrentSelectedCreditCard.EncryptedCardNumber);
            $("#ViewExpirymonth").val(self.CurrentSelectedCreditCard.ExpiryMonth);
            $("#ViewExpiryYear").val(self.CurrentSelectedCreditCard.ExpiryYear);
            $("#ViewCVV").val(self.CurrentSelectedCreditCard.EncryptedCVV);
            $("#viewCreditCard").modal("show");

        });

        $('#AddEditUserCreditCardForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();

            var cardHolderName = $("#Cardholdername").val();
            var cardnumber = $("#Cardnumber").val();
            var expirymonth = $("#Expirymonth").val();
            var expiryYear = $("#ExpiryYear").val();
            var cardType = $("#CardType option:selected").text();
            var issuingBank = $("#IssuingBank option:selected").text();
            var cvv = $("#CVV").val();

            console.log(cardHolderName);


            var userCard = {
                Id: self.CurrentSelectedCreditCard ? self.CurrentSelectedCreditCard.Id : 0,
                UserId: self.ApplicationUser.Id,
                CardHolderName: cardHolderName,
                EncryptedCardNumber: cardnumber,
                MaskedCardNumber: cardnumber,
                LastFourDigits: cardnumber,
                ExpiryMonth: expirymonth,
                ExpiryYear: expiryYear,
                EncryptedCVV: cvv,
                CardType: cardType,
                IssuingBank: issuingBank,
                IsDefault: false,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true
            };

            console.log("userCard...." + JSON.stringify(userCard));

            $.ajax({
                type: "POST",
                url: "/CreditCard/SaveUserCreditCard",
                cache: false,
                data: JSON.stringify(userCard),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    console.log(response)
                    self.CurrentSelectedCreditCard = null;
                    $('#AddEditUserCreditCardForm')[0].reset();
                    $('#sidebar').removeClass('show');
                    $('.modal-backdrop').remove();
                    GetUserCreditCards();

                }, error: function (error) {
                    console.log(error);
                }
            });


        });
    }
}