function OrderSummaryController() {
    var self = this;
    self.paymentOptionId = null;
    self.convenienceFeePercentage = null;
    self.gSTPercentage = null;
    self.gST = null;
    self.convenienceFee = null;
    self.checkoutAmount = null;
    self.despositeAmount = null;
    self.init = function () {
        self.paymentOptionId = $("#PaymentOptionId").val();
        self.convenienceFeePercentage = $("#ConvenienceFeePercentage").val();
        self.gSTPercentage = $("#GSTPercentage").val();
        self.gST = $("#GST").val();
        self.convenienceFee = $("#ConvenienceFee").val();
        self.checkoutAmount = $("#CheckoutAmount").val();
        self.despositeAmount = $("#DespositeAmount").val();
        self.receiverWillReceive = $("#ReceiverWillReceive").val();
        $(".switch").on("change", function () {
            if ($(this).is(':checked')) {
                var expenceSum = parseFloat(self.gST) + parseFloat(self.convenienceFee);
                var recvrAMount = parseFloat(self.despositeAmount - expenceSum);
                $("#ReceiverWillReceive").val(recvrAMount.toFixed(2));
                $("#CheckoutAmount").val(parseFloat(self.despositeAmount).toFixed(2));
                $("#ReceiverWillReceivePay").text(recvrAMount.toFixed(2));
                $("#CheckoutAmountPay").text(parseFloat(self.despositeAmount).toFixed(2));
            }
            else {
                var expenceSum = parseFloat(self.gST) + parseFloat(self.convenienceFee);
                $("#ReceiverWillReceive").val(parseFloat(self.despositeAmount).toFixed(2));
                $("#ReceiverWillReceivePay").text(parseFloat(self.despositeAmount).toFixed(2));
                var checkOutAMount = parseFloat(self.despositeAmount) + parseFloat(expenceSum);
                $("#CheckoutAmount").val(checkOutAMount.toFixed(2));
                $("#CheckoutAmountPay").text(checkOutAMount.toFixed(2));
            }
        });
        $(document).on("click", ".dummy", function () {
            var ancherConvenienceFee = $(this).find("#AncherConvenienceFee").val();
            var ancherGST = $(this).find("#AncherGST").val();
            var ancherPaymentOptionId = $(this).find("#AncherPaymentOptionId").val();
            $("#PaymentOptionId").val(ancherPaymentOptionId);
            $("#ConvenienceFeePercentage").val(ancherConvenienceFee);
            $("#GSTPercentage").val(ancherGST);
            if ($(".switch").is(':checked')) {
                var gstCollection = parseFloat((parseFloat(self.despositeAmount) * parseFloat(ancherGST)) / 100);
                var convienceCollection = parseFloat((parseFloat(self.despositeAmount) * parseFloat(ancherConvenienceFee)) / 100);
                var expenceSum = parseFloat(gstCollection) + parseFloat(convienceCollection);
                $("#ReceiverWillReceive").val(parseFloat(self.despositeAmount).toFixed(2));
                $("#ReceiverWillReceivePay").text(parseFloat(self.despositeAmount).toFixed(2));
                $("#ConvenienceFeePay").text(convienceCollection.toFixed(2));
                $("#GSTPay").text(gstCollection.toFixed(2));
                $("#ConvenienceFee").val(convienceCollection.toFixed(2));
                $("#GST").val(gstCollection.toFixed(2));
                var checkOutAMount = parseFloat(self.despositeAmount) - parseFloat(expenceSum);
                $("#CheckoutAmount").val(checkOutAMount.toFixed(2));
                $("#CheckoutAmountPay").text(checkOutAMount.toFixed(2));
            } else {
                var gstCollection = parseFloat((parseFloat(self.despositeAmount) * parseFloat(ancherGST)) / 100);
                var convienceCollection = parseFloat((parseFloat(self.despositeAmount) * parseFloat(ancherConvenienceFee)) / 100);
                var expenceSum = parseFloat(gstCollection) + parseFloat(convienceCollection);
                $("#ReceiverWillReceive").val(parseFloat(self.despositeAmount).toFixed(2));
                $("#ReceiverWillReceivePay").text(parseFloat(self.despositeAmount).toFixed(2));
                $("#ConvenienceFeePay").text(convienceCollection.toFixed(2));
                $("#GSTPay").text(gstCollection.toFixed(2));
                $("#ConvenienceFee").val(convienceCollection.toFixed(2));
                $("#GST").val(gstCollection.toFixed(2));
                var checkOutAMount = parseFloat(self.despositeAmount) + parseFloat(expenceSum);
                $("#CheckoutAmount").val(checkOutAMount.toFixed(2));
                $("#CheckoutAmountPay").text(checkOutAMount.toFixed(2));
            }
        });
    };
}