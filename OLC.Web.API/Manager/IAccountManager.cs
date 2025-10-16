using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAccountManager
    {
        Task<RegistrationResult> RegisterUserAsync(UserRegistration userRegistration);
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication userAuthentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse authResponse);
        Task<long> ForgotPasswordAsync(ForgotPassword userServices);
        Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
        Task<AuthResponse> LoginOrRegisterExternalUserAsync(ExternalUserInfo externalUserInfo);
    }
}
