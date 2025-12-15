namespace OLC.Web.API.Models
{
    public class OrderQueue
    {
        public long Id { get; set; }

        public long PaymentOrderId { get; set; }

        public string OrderReference { get; set; } = string.Empty;

        public string QueueStatus { get; set; } = "Pending";

        public int Priority { get; set; } = 5;

        public long? AssignedExecutiveId { get; set; }

        public DateTimeOffset? AssignedOn { get; set; }

        public DateTimeOffset? ProcessingStartedOn { get; set; }

        public DateTimeOffset? ProcessingCompletedOn { get; set; }

        public int RetryCount { get; set; } = 0;

        public int MaxRetries { get; set; } = 3;

        public string? FailureReason { get; set; }

        public string? Metadata { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }
    }

}
