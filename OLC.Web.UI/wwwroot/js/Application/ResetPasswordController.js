function ResetPasswordController() {
    var self = this;

    self.userId = null;

    self.init = function () {

        self.userId = getQueryStringParameter("userId");

        var form = $('#formResetPassword');
        var signUpButton = $('#btnSubmit');
        form.on('input', 'input, select, textarea', checkFormValidity);
        checkFormValidity();
        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
        $(document).on("click", "#btnSubmit", function (e) {
            e.preventDefault();
            $(".se-pre-con").show();

            var userAuthetnication = {
                UserId: self.userId,
                Password: $("#password").val()
            };

            makeAjaxRequest({
                url: API_URLS.ResetPasswordAsync,
                data: userAuthetnication,
                type: 'POST',
                successCallback: handleAuthenticationSuccess,
                errorCallback: handleAuthenticationError
            });
        });

        function handleAuthenticationSuccess(response) {
            console.info(response);
            window.location.href = "/Home/Index";
            $(".se-pre-con").hide();
        }

        function handleAuthenticationError(xhr, status, error) {
            console.error("Error in upserting data to server: " + error);
            $(".se-pre-con").hide();
        }
    };
}