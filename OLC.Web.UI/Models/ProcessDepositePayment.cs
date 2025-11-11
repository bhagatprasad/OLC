namespace OLC.Web.UI.Models
{
    public class ProcessDepositePayment
    {
        public long? PaymentOrderId { get; set; }
        public string? OrderReference { get; set; }
        public decimal? DepositeAmount { get; set; }
        public decimal? ActualDepositeAmount { get; set; }
        public decimal? PendingDepositeAmount { get; set; }
        public bool? IsPartialPayment { get; set; }
        public string? StripePaymentChargeId { get; set; }
        public string? StripeDepositeIntentId { get; set; }
        public string? StripeDepositeChargeId { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
