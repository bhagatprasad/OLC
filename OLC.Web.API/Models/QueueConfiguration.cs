namespace OLC.Web.API.Models
{
    public class QueueConfiguration
    {
        public long? Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? DataType { get; set; }
        public string? Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
