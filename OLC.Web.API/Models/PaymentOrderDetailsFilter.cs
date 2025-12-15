namespace OLC.Web.API.Models
{
    public class PaymentOrderDetailsFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? OrderStatusId { get; set; }   
        public long? PaymentStatusId { get; set; }
        public long? DepositStatusId { get; set; }
        public string? PaymentOrderType { get; set; }
        public string? WalletId { get; set; }
    }
}
