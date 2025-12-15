namespace OLC.Web.API.Models
{
    public class PaymentOrderDetails
    {
        public string? PaymentOrderType { get; set; }
        public string? WalletId { get; set; }

        public PaymentOrderDetails? StripePaymentIntentId { get; set; }
        public ExecutivePaymentOrders  paymentOrder { get; set; }

        public UserBankAccount paymentOrderBankAccount { get; set; }

        public UserCreditCard userCreditCard { get; set; }

        public UserBillingAddress userBillingAddress { get; set; }
        public List<PaymentOrderHistory> paymentOrderHistory { get; set; }
        public List<DepositOrder> DepositeOrders { get; set; }
    }
}
