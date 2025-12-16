namespace OLC.Web.UI.Models
{
    public class OrderQueue : ExecutivePaymentOrders
    {
        public long Id { get; set; }

        public long? PaymentOrderId { get; set; }

        public string? OrderReference { get; set; }

        public string? QueueStatus { get; set; }

        public int Priority { get; set; }

        public long? AssignedExecutiveId { get; set; }

        public DateTimeOffset? AssignedOn { get; set; }

        public DateTimeOffset? ProcessingStartedOn { get; set; }

        public DateTimeOffset? ProcessingCompletedOn { get; set; }

        public int RetryCount { get; set; }

        public int MaxRetries { get; set; }

        public string? FailureReason { get; set; }

        public string? Metadata { get; set; }

        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
