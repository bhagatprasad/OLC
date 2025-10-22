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
                }, error: function (error) {
                    console.log(error);
                }
            });
        };

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
        });
    }
}