namespace OLC.Web.API.Models
{
    public class UpdateUserCreditCard
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
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
