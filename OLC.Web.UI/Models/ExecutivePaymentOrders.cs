namespace OLC.Web.UI.Models
{
    public class ExecutivePaymentOrders : UserPaymentOrder
    {
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? PaymentOrderTpe { get; set; }
        public string? WalletId { get; set; }
    }
}
