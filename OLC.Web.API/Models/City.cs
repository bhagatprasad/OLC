namespace OLC.Web.API.Models
{
    public class City
    {
        public long Id { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }

    }
}
