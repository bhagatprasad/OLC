function PasswordSettingsController() {
    var self = this;
    self.init = function () {
        $('#eye').click(function () {
            $('#Password').attr('type', $('#Password').is(':password') ? 'text' : 'password');
            if ($('#Password').attr('type') === 'password') {
                $('#eye').removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                $('#eye').removeClass('fa-eye-slash').addClass('fa-eye');
            }
        });
    }
}