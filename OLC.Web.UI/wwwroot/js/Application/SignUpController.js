function SignUpController() {
    var self = this;
    self.init = function () {
        var form = $('#formSignUp');
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

            var registerUser = {
                FirstName: $("#Firstname").val(),
                LastName: $("#Lastname").val(),
                Email: $("#Email").val(),
                Phone: $("#Phone").val(),
                Password: $("#password").val()
            };

            makeAjaxRequest({
                url: API_URLS.RegistrationUserAsync,
                data: registerUser,
                type: 'POST',
                successCallback: handleUserRegistrationSuccess,
                errorCallback: handleUserRegistrationError
            });
        });

        function handleUserRegistrationSuccess(response) {
            console.info(response);

            if (response.data)
                window.location.href = "/Account/Login";

            $(".se-pre-con").hide();
        }

        function handleUserRegistrationError(xhr, status, error) {
            console.error("Error in upserting data to server: " + error);
            $(".se-pre-con").hide();
        }
    };
}