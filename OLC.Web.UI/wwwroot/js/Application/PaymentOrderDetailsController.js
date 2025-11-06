function PaymentOrderDetailsController() {
    var self = this;

    self.PaymentOrderId = null;

    var actions = [];

    var dataObjects = [];

    self.PaymentOrderDetails = {};

    self.CoreStatuses = [];

    self.CoreCountries = [];

    self.CoreStates = [];

    self.CoreCities = [];

    actions.push("/PaymentOrder/GetExecutivePaymentOrderDetails");
    actions.push("/Status/GetStatuses");
    actions.push("/Country/GetCountriesList");
    actions.push("/State/GetStatesList");
    actions.push("/City/GetCitiesList");

    self.init = function () {
        $(".se-pre-con").show();
        self.PaymentOrderId = getQueryStringParameter("paymentOrderId");
        console.log(self.PaymentOrderId);

        dataObjects.push({ paymentOrderId: self.PaymentOrderId });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log('Payment Orders details response Response:', responses);

            self.PaymentOrderDetails = responses[0][0] && responses[0][0].data ? responses[0][0].data : {};
            self.CoreStatuses = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];
            self.CoreCountries = responses[2][0] && responses[2][0].data ? responses[2][0].data : [];
            self.CoreStates = responses[3][0] && responses[3][0].data ? responses[3][0].data : [];
            self.CoreCities = responses[4][0] && responses[4][0].data ? responses[4][0].data : [];

            self.bindPaymentOrderDetails(self.PaymentOrderDetails);
            if (self.PaymentOrderDetails.paymentOrderHistory && self.PaymentOrderDetails.paymentOrderHistory.length > 0) {
                $("#paymentOrderHistory").empty(); // Clear existing rows

                self.PaymentOrderDetails.paymentOrderHistory.forEach(function (item) {
                    const createdOn = item.CreatedOn ? new Date(item.CreatedOn).toLocaleString() : "";
                    const modifiedOn = item.ModifiedOn ? new Date(item.ModifiedOn).toLocaleString() : "";
                    const currentStatus = self.CoreStatuses.filter(x => x.Id == item.StatusId)[0];

                    $("#paymentOrderHistory").append(`
                        <tr>
                            <td>${item.Id}</td>
                            <td>${currentStatus != null ? currentStatus.Name :""}</td>
                            <td>${item.Description}</td>
                            <td>${item.CreatedBy}</td>
                            <td>${createdOn}</td>
                            <td>${modifiedOn}</td>
                            <td>${item.IsActive ? "Yes" : "No"}</td>
                        </tr>
        `               );
                });
            }
            else {
                $("#paymentOrderHistory").append(
                    `<tr><td colspan="7">No history found</td></tr>`
                );
            }

            $(".se-pre-con").hide();
        }).fail(function (error) {
            console.log('One or more requests failed:', error);
            self.showErrorState();
            $(".se-pre-con").hide();
        });
    }

    self.bindPaymentOrderDetails = function (data) {

        var country = {};
        if (data.userBillingAddress.CountryId)
            country = self.CoreCountries.filter(x => x.Id == data.userBillingAddress.CountryId)[0];//data.userBillingAddress.CountryId

        $("#orderReference").text(data.paymentOrder.OrderReference);
        $("#amount").text(data.paymentOrder.Amount);
        $("#chargeAmount").text(data.paymentOrder.TotalPlatformFee);
        $("#depositAmount").text(data.paymentOrder.TotalAmountToDepositToCustomer);
        $("#user").text(data.paymentOrder.UserEmail);
        $('#paymentMethod').text(data.userCreditCard.EncryptedCardNumber);
        $('#status').text(data.paymentOrder.OrderStatus);
        $('#sla').text(data.sla);
        $('#creditDate').text(data.paymentOrder.CreatedOn);
        // Credit Card Data
        $('#cardHolderName').text(data.userCreditCard.CardHolderName || '--');
        $('#cardType').text(data.userCreditCard.CardType || '--');
        $('#cardNumberMasked').text(data.userCreditCard.MaskedCardNumber || '--');
        $('#cardExpiry').text(`${data.userCreditCard.ExpiryMonth}/${data.userCreditCard.ExpiryYear}` || '--');
        $('#issuingBank').text(data.userCreditCard.IssuingBank || '--');
        // Populating Bank Account Information
        $('#bankAccountHolderName').text(data.paymentOrderBankAccount.AccountHolderName || '--');
        $('#bankName').text(data.paymentOrderBankAccount.BankName || '--');
        $('#bankAccountNumber').text(data.paymentOrderBankAccount.AccountNumber || '--');
        $('#bankAccountType').text(data.paymentOrderBankAccount.AccountType || '--');
        $('#bankBranchName').text(data.paymentOrderBankAccount.BranchName || '--');
        $('#bankIFSC').text(data.paymentOrderBankAccount.IFSCCode || '--');
        $('#bankSwift').text(data.paymentOrderBankAccount.SWIFTCode || '--');
        $('#bankVerificationStatus').text(data.paymentOrderBankAccount.VerificationStatus || '--');

        // Populating Billing Address Information
        $('#addressLine1').text(data.userBillingAddress.AddessLineOne || '--');
        $('#addressLine2').text(data.userBillingAddress.AddessLineTwo || '--');
        $('#addressLine3').text(data.userBillingAddress.AddessLineThress || '--');
        $('#addressLocation').text(data.userBillingAddress.Location || '--');
        $('#addressCity').text(data.userBillingAddress.CityId || '--');
        $('#addressState').text(data.userBillingAddress.StateId || '--');
        if (country)
            $('#addressCountry').text(country.Name || '--');
        $('#addressPinCode').text(data.userBillingAddress.PinCode || '--');

    };
}