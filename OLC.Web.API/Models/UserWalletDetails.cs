namespace OLC.Web.API.Models
{
    public class UserWalletDetails : UserWallet
    {
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? Code { get; set; }
    }
}