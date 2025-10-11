function SendController() {
    var self = this;
    self.init = function () {
        /*$("#paymentReason").prepend(new Option("Select Payment Reason", "0"));*/
        $("#paymentReason").change(function () {
            var purpose = $('option:selected', this).text();
            $("#Purpose").val(purpose);
            self.validate();
        });
        $("#DespositeAmount").change(function () {
            self.validate();
        });
        $(document).ready(function () {
            self.validate();
        });

    };
    self.validate = function () {
        if ($("#paymentReason")[0].selectedIndex > 0 && $("#DespositeAmount").val() > 0) {
            $("#btnStepOne").removeAttr("disabled");
        }
        else {
            $("#btnStepOne").attr("disabled", "disabled");
        }
    }
}