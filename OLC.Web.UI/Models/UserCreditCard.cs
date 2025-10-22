namespace OLC.Web.UI.Models
{
    public class UserCreditCard
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string? CardHolderName { get; set; }
        public string? EncryptedCardNumber { get; set; }
        public string? MaskedCardNumber { get; set; }
        public string? LastFourDigits { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public string? EncryptedCVV { get; set; }
        public string? CardType { get; set; }
        public string? IssuingBank { get; set; }
        public bool? IsDefault { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
