namespace OLC.Web.API.Models
{
    public class UserBillingAddress
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string? AddessLineOne { get; set; }
        public string? AddessLineTwo { get; set; }
        public string? AddessLineThress { get; set; }
        public string? Location { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public string? PinCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
