namespace OLC.Web.API.Models
{
    public class ServiceRequestReplies
    {
        public long Id { get; set; }
        public long? TicketId { get; set; }
        public long? ReplierId { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public bool? IsInternal { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
