namespace OLC.Web.API.Models
{
    public class PaymentOrderDetails
    {
        public ExecutivePaymentOrders  paymentOrder { get; set; }

        public UserBankAccount paymentOrderBankAccount { get; set; }

        public UserCreditCard userCreditCard { get; set; }

        public List<PaymentOrderHistory> paymentOrderHistory { get; set; }
        public UserBillingAddress userBillingAddress { get; set; }


    }
}
