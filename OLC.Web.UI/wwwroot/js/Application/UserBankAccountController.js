function UserBankAccountController() {
    var self = this;
    self.init = function () {

        $(document).on("click", "#addBankAccountBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
            console.log("Iam getting from add button click");
        });


        $(document).on("click", "#closeSidebar", function () {
            $('#AddEditUserBankAccountForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $('#AddEditUserBankAccountForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            console.log(userRegistration);
        });
    }
}