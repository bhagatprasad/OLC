function PasswordSettingsController() {
    var self = this;
    self.init = function () {
        $('#eye').click(function () {
            $('#password').attr('type', $('#password').is(':password') ? 'text' : 'password');
            if ($('#password').attr('type') === 'password') {
                $('#eye').removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                $('#eye').removeClass('fa-eye-slash').addClass('fa-eye');
            }
        });
    }
}