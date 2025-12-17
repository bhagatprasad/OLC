namespace OLC.Web.API.Models
{
    public class ExecutivePaymentOrders : UserPaymentOrder
    {
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? PaymentOrderType { get;set; }
        public string? WalletId { get;set; }
    }
}
