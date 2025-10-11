function OrderCreditCardsController() {
    var self = this;
    self.init = function () {
        $(document).ready(function () {
            if ($("#creditcardsLength").val() == 0 || $("#CreditCardId").val() === null || $("#CreditCardId").val() === '00000000-0000-0000-0000-000000000000' || $("#CreditCardId").val() === "" || $("#CreditCardId").val() === undefined) {
                $("#btnStepThree").attr("disabled", "disabled");
            }
            else {
                $("#btnStepThree").removeAttr("disabled");
            }
        });
        $('input[type=radio]').change(function () {
            var id = $(this).attr('id');
            var selectedid = id.split('control_');
            $("#CreditCardId").val(selectedid[1]);
            if ($(this).is(":checked")) {
                $("#btnStepThree").removeAttr("disabled");
            }
            else {
                $("#btnStepThree").attr("disabled", "disabled");
            }
        });
    };
}