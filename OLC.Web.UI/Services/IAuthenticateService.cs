using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IAuthenticateService
    {
        Task<bool> RegisterUserAsync(UserRegistration userRegistration);
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
        Task<long> ForgotPasswordAsync(ForgotPassword userServices);
        Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
    }
}
