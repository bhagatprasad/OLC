using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IAuthenticateService
    {
        Task<RegistrationResult> RegisterUserAsync(UserRegistration userRegistration);
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
        Task<long> ForgotPasswordAsync(ForgotPassword userServices);
        Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
        Task<AuthResponse> LoginOrRegisterExternalUserAsync(ExternalUserInfo externalUserInfo);
    }
}
