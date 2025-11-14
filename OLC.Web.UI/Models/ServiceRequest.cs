namespace OLC.Web.UI.Models
{
    public class ServiceRequest
    {
        public long TicketId { get; set; }
        public long? OrderId { get; set; }
        public long? UserId { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Category { get; set; }
        public string? RequestReference { get; set; }
        public string? Classification { get; set; }
        public string? Priority { get; set; }
        public long? StatusId { get; set; }
        public long? AssignTo { get; set; }
        public long? AssignBy { get; set; }
        public DateTimeOffset? AssignedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
