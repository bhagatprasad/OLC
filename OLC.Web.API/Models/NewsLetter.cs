namespace OLC.Web.API.Models
{
    public class NewsLetter
    {
        public long? Id { get; set; }
        public string? Email { get; set; }
        public DateTime? SubscribedOn { get; set; }
        public DateTime? UnsubscribedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
