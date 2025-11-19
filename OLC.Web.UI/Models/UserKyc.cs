namespace OLC.Web.UI.Models
{
    public class UserKyc
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string? KycStatus { get; set; }
        public string? KycLevel { get; set; }
        public DateTimeOffset? SubmittedOn { get; set; }
        public DateTimeOffset? VerifiedOn { get; set; }
        public long? VerifiedBy { get; set; }
        public string? RejectionReason { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
