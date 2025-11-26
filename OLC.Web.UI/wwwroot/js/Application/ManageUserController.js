function ManageUserController() {
    var self = this;

    self.userId = null;

    self.isReadOnly = false;

    self.init = function () {

        self.userId = getQueryStringParameter("userId");

        self.isReadOnly = getQueryStringParameter("isReadOnly");

        console.log('The current userid is ' + self.userId + ', its access will be ' + self.isReadOnly);
    }
}