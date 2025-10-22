function UserCreditCardController() {
    var self = this;

    self.ApplicationUser = {};
    self.UserCreditCards = [];
    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');

        console.log(appUserInfo);

        if (appUserInfo) {

            self.ApplicationUser = appUserInfo;
        }

        GetUserCreditCards();

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
        function getStatusBadge(isActive) {
            if (isActive) {
                return '<span class="badge bg-success status-badge">Active</span>';
            } else {
                return '<span class="badge bg-warning status-badge">Inactive</span>';
            }
        }
        function loadUserCreditCards() {
            const tbody = $('#userCreditCardsBody');
            tbody.empty(); // Clear existing rows

            if (self.UserCreditCards.length > 0) {
                self.UserCreditCards.forEach(function (card) {

                    const statusBadge = getStatusBadge(card.IsActive);

                    const actionButtons =`
                <button class="btn btn-sm btn-outline-primary view-card" data-card-id="${card.Id}" data-card='${card}' title="view card">
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-sm btn-outline-warning edit-card" data-card-id="${card.Id}" data-card='${card}' title="edit card">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger delete-card" data-card-id="${card.Id}" data-card='${card}' title="delete card">
                    <i class="fas fa-trash"></i>
                </button>
            `;


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
                });
            }

        }

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

        $('#AddEditUserCreditCardForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            console.log(userRegistration);
            GetUserCreditCards();

            $('#AddEditUserCreditCardForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
    }
}