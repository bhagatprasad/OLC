namespace OLC.Web.UI.Models
{
    public class UserBankAccount
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string? AccountHolderName { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? AccountNumber { get; set; }
        public string? LastFourDigits { get; set; }
        public string? AccountType { get; set; }
        public string? RoutingNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? SWIFTCode { get; set; }
        public string? Currency { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset? VerifiedOn { get; set; }
        public string? VerificationStatus { get; set; }
        public int? VerificationAttempts { get; set; }
        public DateTimeOffset? LastVerificationAttempt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
