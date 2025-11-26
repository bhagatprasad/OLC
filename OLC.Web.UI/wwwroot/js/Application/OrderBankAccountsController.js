function OrderBankAccountsController() {
    var self = this;
    self.init = function () {
        $(document).ready(function () {
            if ($("#BankAccountId").val() === null || $("#BankAccountId").val() === '00000000-0000-0000-0000-000000000000' || $("#BankAccountId").val() === "" || $("#BankAccountId").val() === undefined) {
                $("#btnStepTwo").attr("disabled", "disabled");
            }
            else {
                $("#btnStepTwo").removeAttr("disabled");
            }
        });
        $('input[type=radio]').change(function () {
            var id = $(this).attr('id');
            var selectedid = id.split('control_');
            $("#BankAccountId").val(selectedid[1]);
            if ($(this).is(":checked")) {
                $("#btnStepTwo").removeAttr("disabled");
            }
            else {
                $("#btnStepTwo").attr("disabled", "disabled");
            }
        });
    };
}