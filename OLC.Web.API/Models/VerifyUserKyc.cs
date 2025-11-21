namespace OLC.Web.API.Models
{
    public class VerifyUserKyc
    {
        public long? UserKycId { get; set; }
        public long? UserKycDocumentId { get; set; }
        public string?  Status  { get; set; }
        public string? Status { get; set; }
        public long? ModifiedBy { get; set; }
        public string? RejectedReason { get; set; }
    }
}
