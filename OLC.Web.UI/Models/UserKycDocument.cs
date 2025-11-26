namespace OLC.Web.UI.Models
{
    public class UserKycDocument
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string? DocumentFilePath { get; set; }
        public byte[]? DocumentFileData { get; set; }
        public string VerificationStatus { get; set; }
        public DateTimeOffset? VerifiedOn { get; set; }
        public long? VerifiedBy { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
