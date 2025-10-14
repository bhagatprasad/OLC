function LoginController() {
    var self = this;
    self.init = function () {
        var form = $('#formAuthentication');
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
                username: $("#username").val(),
                password: $("#password").val()
            };

            makeAjaxRequest({
                url: API_URLS.AuthenticateAsync,
                data: userAuthetnication,
                type: 'POST',
                successCallback: handleAuthenticationSuccess,
                errorCallback: handleAuthenticationError
            });
        });

        function handleAuthenticationSuccess(response) {
            console.info(response);
            if (response.status) {
                var _appUserInfo = storageService.get('ApplicationUser');
                if (_appUserInfo) {
                    storageService.remove('ApplicationUser');
                }

                var applicationUser = response.appUser;

                storageService.set('ApplicationUser', applicationUser);

                var appUserInfo = storageService.get('ApplicationUser');


                if (appUserInfo) {
                    if (appUserInfo.RoleName === "Doctor") {
                        window.location.href = "/DoctorDashboard/ManageDoctorAppointments";
                    } else if (appUserInfo.RoleName === "Executive") {
                        window.location.href = "/ExecutiveDashboard/Index";
                    }
                    else if (appUserInfo.RoleName === "Pharmacist") {
                        window.location.href = "/PharmacistDashBoard/Index";
                    }
                    else if (appUserInfo.RoleName === "Lab technicians") {
                        window.location.href = "/LabTechnicianDashBoard/Index";
                    } else if (appUserInfo.RoleName === "Receptionist") {
                        window.location.href = "/DoctorAppointment/Index";
                    } else {

                        window.location.href = "/Home/Index";
                    }
                }
                updateEnvironmentAndVersion();

                $(".se-pre-con").hide();
            }
        }

        function handleAuthenticationError(xhr, status, error) {
            console.error("Error in upserting data to server: " + error);
            $(".se-pre-con").hide();
        }
        function updateEnvironmentAndVersion() {
            var environment = storageService.get('Environment');
            if (environment) {
                storageService.remove('Environment');
            }
            storageService.set('Environment', window.location.hostname);

            var version = storageService.get('Version');
            if (version) {
                storageService.remove('Version');
            }
            storageService.set('Version', '1.0.0.0');
        }
    };
}