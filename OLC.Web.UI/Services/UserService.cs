using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public UserService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<ApplicationUser> GetUserAccountAsync(long userId)
        {
            var url = Path.Combine("User/GetUserAccountAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<ApplicationUser>(HttpMethod.Get, url);
        }

        public async Task<List<UserAccount>> GetUserAccountsAsync()
        {
            return await _repositoryFactory.SendAsync<List<UserAccount>>(HttpMethod.Get, "User/GetUserAccountsAsync");
        }

        public async Task<PreviewUserKycDocument> PreviewUserKycDocumentAsync(long userId)
        {
            var url = Path.Combine("User/PreviewUserKycDocumentAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<PreviewUserKycDocument>(HttpMethod.Get, url);
        }

        public async Task<ApplicationUser> UpdateUserPersonalInformationAsync(UserPersonalInformation userPersonalInformation)
        {
            return await _repositoryFactory.SendAsync<UserPersonalInformation, ApplicationUser>(HttpMethod.Post, "User/UpdateUserPersonalInformationAsync", userPersonalInformation);
        }
    }
}
