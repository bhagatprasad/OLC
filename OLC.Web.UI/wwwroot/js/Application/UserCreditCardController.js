function UserCreditCardController() {
    var self = this;
    self.init = function () {

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