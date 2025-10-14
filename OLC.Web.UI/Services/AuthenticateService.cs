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

        public async Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth)
        {
            return await _repositoryFactory.SendAsync<AuthResponse, ApplicationUser>(HttpMethod.Post, "Account/GenarateUserClaimsAsync", auth);
        }

        public async Task<bool> RegisterUserAsync(UserRegistration userRegistration)
        {
            return await _repositoryFactory.SendAsync<UserRegistration, bool>(HttpMethod.Post, "Account/RegisterUserAsync", userRegistration);
        }
    }
}
