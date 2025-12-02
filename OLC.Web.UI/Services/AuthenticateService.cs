using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public AuthenticateService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication)
        {
            return await _repositoryFactory.SendAsync<UserAuthentication, AuthResponse>(HttpMethod.Post, "Account/AuthenticateUserAsync", authentication);
        }

        public async Task<long> ForgotPasswordAsync(ForgotPassword userServices)
        {
            return await _repositoryFactory.SendAsync<ForgotPassword, long>(HttpMethod.Post, "Account/ForgotPasswordAsync", userServices);
        }

        public async Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth)
        {
            return await _repositoryFactory.SendAsync<AuthResponse, ApplicationUser>(HttpMethod.Post, "Account/GenarateUserClaimsAsync", auth);
        }

        public async Task<AuthResponse> LoginOrRegisterExternalUserAsync(ExternalUserInfo externalUserInfo)
        {
            return await _repositoryFactory.SendAsync<ExternalUserInfo, AuthResponse>(HttpMethod.Post, "Account/LoginOrRegisterExternalUserAsync", externalUserInfo);
        }

        public async Task<RegistrationResult> RegisterUserAsync(UserRegistration userRegistration)
        {
            return await _repositoryFactory.SendAsync<UserRegistration, RegistrationResult>(HttpMethod.Post, "Account/RegisterUserAsync", userRegistration);
        }

        public async Task<bool> ResetPasswordAsync(ResetPassword resetPassword)
        {
            return await _repositoryFactory.SendAsync<ResetPassword, bool>(HttpMethod.Post, "Account/ResetPasswordAsync", resetPassword);
        }

        public async Task<bool> ChangePasswordAsync(ChangePassword changePassword)
        {
            return await _repositoryFactory.SendAsync<ChangePassword, bool>(HttpMethod.Post, "Account/ChangePasswordAsync", changePassword);
        }
    }
}
