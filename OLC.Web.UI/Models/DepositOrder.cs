namespace OLC.Web.UI.Models
{
    public class DepositOrder
    {
        public long? Id { get; set; }
        public long? PaymentOrderId { get; set; }
        public string? OrderReference { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? ActualDepositAmount { get; set; }
        public decimal? PendingDepositAmount { get; set; }
        public string? StripeDepositIntentId { get; set; }
        public string? StripeDepositChargeId { get; set; }
        public long? IsPartialPayment { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
