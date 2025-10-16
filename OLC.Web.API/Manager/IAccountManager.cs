using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAccountManager
    {
        Task<bool> RegisterUserAsync(UserRegistration userRegistration);
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication userAuthentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse authResponse);
        Task<long> ForgotPasswordAsync(ForgotPassword userServices);
        Task<bool> ResetPasswordAsync(ResetPassword resetPassword);

        Task<bool> ChangePasswordAsync(ChangePassword changePassword);

        Task<UserCreditCardDetails> GetUserCreditCardsAsync(GetUserCreditCards getUserCreditCards);

        Task<bool> InsertUserCreditCardAsync(UserCreditCardDetails insertUserCreditCard);

        Task<UserCreditCardDetails> GetUserCreditCardByCardIdAsync(GetUserCreditCardByCardId getUserCreditCardByCardId);
    }
}
