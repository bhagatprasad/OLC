function ExternalLoginController() {
    var self = this;
    self.userClaimes = {};
    self.init = function () {
        makeAjaxRequest({
            url: API_URLS.ExternalCallaBackResponseAsync,
            type: 'GET',
            successCallback: handleAuthenticationSuccess,
            errorCallback: handleAuthenticationError
        });
        function handleAuthenticationSuccess(response) {
            console.info(response);
            makeAjaxRequest({
                url: API_URLS.LoginOrRegisterExternalUserAsync,
                type: 'POST',
                data: JSON.parse(response.data),
                successCallback: handleExternalAuthenticationSuccess,
                errorCallback: handleAuthenticationError
            });
        }
        function handleExternalAuthenticationSuccess(response) {
            console.info(response);
            var _appUserInfo = storageService.get('ApplicationUser');
            if (_appUserInfo) {
                storageService.remove('ApplicationUser');
            }

            var applicationUser = response.appUser;

            storageService.set('ApplicationUser', applicationUser);

            var appUserInfo = storageService.get('ApplicationUser');

            updateEnvironmentAndVersion();

            if (appUserInfo) {
                if (appUserInfo.RoleId === 1) {
                    window.location.href = "/AdminBaord/Index";
                } else if (appUserInfo.RoleId === 2) {
                    window.location.href = "/UserBoard/Index";
                }
                else if (appUserInfo.RoleId === 3) {
                    window.location.href = "/ExecutiveBoard/Index";
                } else {
                    window.location.href = "/NotFound/Error";
                }
            }
            $(".se-pre-con").hide();
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
    }
}