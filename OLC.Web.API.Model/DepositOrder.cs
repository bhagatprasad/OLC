namespace OLC.Web.API.Models
{
    public class DepositOrder
    {
        public long? Id { get; set; }
        public long? PaymentOrderId { get; set; }
        public string? OrderReference { get; set; }
        public decimal? DepositeAmount { get; set; }
        public decimal? ActualDepositeAmount { get; set; }
        public decimal? PendingDepositeAmount { get; set; }
        public string? StripeDepositeIntentId { get; set; }
        public string? StripeDepositeChargeId { get; set; }
        public long? IsPartialPayment { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get;set; }
        public bool? IsActive { get; set; }
    }
}
