namespace OLC.Web.API.Models
{
    public class ExecutiveAssignments
    {
        public long Id { get; set; }
        public long ExecutiveId { get; set; }
        public long OrderQueueId { get; set; }
        public string AssignmentStatus { get; set; }
        public DateTimeOffset AssignedAt { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public string? Notes { get; set; }
    }
}
