using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class UserKycService : IUserKycService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UserKycService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<UserKyc>> GetAllUsersKycAsync()
        {
            return await _repositoryFactory.SendAsync<List<UserKyc>>(HttpMethod.Get, "User/GetAllUsersKycAsync");
        }

        public async Task<UserKyc> GetUserKycByUserIdAsync(long userId)
        {
           var url = Path.Combine("User/GetUserKycByUSerIdAsync",userId.ToString());
            return await _repositoryFactory.SendAsync<UserKyc>(HttpMethod.Get,url);
        }

        public async Task<bool> InsertUserKycAsync(UserKyc userKyc)
        {
            return await _repositoryFactory.SendAsync<UserKyc, bool>(HttpMethod.Get, "User/InsertUserKycAsync", userKyc);
        }

        public async Task<bool> ProcessUserKycAsync(UserKyc userKyc)
        {
            return await _repositoryFactory.SendAsync<UserKyc, bool>(HttpMethod.Get, "User/ProcessUserKycAsync", userKyc);
        }
    }
}
