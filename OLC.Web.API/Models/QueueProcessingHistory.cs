namespace OLC.Web.API.Models
{
    public class QueueProcessingHistory
    {
        public long Id { get; set; }

        public long OrderQueueId { get; set; }

        public long? ExecutiveId { get; set; }

        public string? FromStatus { get; set; }

        public string ToStatus { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public DateTimeOffset ActionTimestamp { get; set; }

        public string? Details { get; set; }

        public string? IPAddress { get; set; }
    }

}
