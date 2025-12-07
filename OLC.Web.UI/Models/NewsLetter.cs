namespace OLC.Web.UI.Models
{
    public class NewsLetter
    {
        public long? Id { get; set; }
        public string? Email { get; set; }
        public DateTimeOffset? SubscribedOn { get; set; }
        public DateTimeOffset? UnsubscribedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
