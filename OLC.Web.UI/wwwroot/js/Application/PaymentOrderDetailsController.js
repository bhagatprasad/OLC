function PaymentOrderDetailsController() {
    var self = this;
    self.PaymentOrderId = null;
    self.init = function () {
        self.PaymentOrderId = getQueryStringParameter("paymentOrderId");
        console.log(self.PaymentOrderId);
        self.loadPaymentOrderDetails();
    }

    self.loadPaymentOrderDetails = function () {
        $.ajax({
            url: '/PaymentOrder/GetExecutivePaymentOrderDetails',  
            type: 'GET',
            data: { paymentOrderId: self.PaymentOrderId },
            success: function (response)
            {
                console.log("Payment Order Details:", response);
                self.bindPaymentOrderDetails(response.data);
                if (response.data.paymentOrderHistory && response.data.paymentOrderHistory.length > 0)
                {
                    $("#paymentOrderHistory").empty(); // Clear existing rows

                    response.data.paymentOrderHistory.forEach(function (item)
                    {
                        const createdOn = item.CreatedOn ? new Date(item.CreatedOn).toLocaleString() : "";
                        const modifiedOn = item.ModifiedOn ? new Date(item.ModifiedOn).toLocaleString() : "";

                        $("#paymentOrderHistory").append(`
                        <tr>
                            <td>${item.Id}</td>
                            <td>${item.StatusId}</td>
                            <td>${item.Description}</td>
                            <td>${item.CreatedBy}</td>
                            <td>${createdOn}</td>
                            <td>${modifiedOn}</td>
                            <td>${item.IsActive ? "Yes" : "No"}</td>
                        </tr>
        `               );
                    });
                }
                else
                {
                     $("#paymentOrderHistory").append(
                          `<tr><td colspan="7">No history found</td></tr>`
                     );
                }
                
            },
            error: function (xhr, status, error) {
                console.error("Error fetching order details:", error);
            }
        });
    };

    self.bindPaymentOrderDetails = function (data) {
        $("#orderReference").text(data.paymentOrder.OrderReference);
        $("#amount").text(data.paymentOrder.Amount);
        $("#chargeAmount").text(data.paymentOrder.TotalPlatformFee);
        $("#depositAmount").text(data.paymentOrder.TotalAmountToDepositToCustomer);
        $("#user").text(data.paymentOrderBankAccount.AccountHolderName);
        $('#paymentMethod').text(data.userCreditCard.EncryptedCardNumber);
        $('#status').text(data.paymentOrderHistory.StatusId);
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
        $('#addressCountry').text(data.userBillingAddress.CountryId || '--'); 
        $('#addressPinCode').text(data.userBillingAddress.PinCode || '--');

    };
}