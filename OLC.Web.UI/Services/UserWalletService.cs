using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class UserWalletService : IUserWalletService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public UserWalletService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public Task<List<UserWalletLog>> GetAllUsersWalletlogAsync()
        {
           return _repositoryFactory.SendAsync<List<UserWalletLog>>(HttpMethod.Get, "UserWallet/GetAllUsersWalletlogAsync");
        }

        public Task<List<UserWalletLog>> GetAllUserWalletlogByUserIdAsync(long userId)
        {
          return  _repositoryFactory.SendAsync<List<UserWalletLog>>(HttpMethod.Get, $"UserWallet/GetAllUserWalletlogByUserIdAsync/{userId}");
        }

        public Task<List<UserWallet>> GetAllUserWalletsAsync()
        {
           return _repositoryFactory.SendAsync<List<UserWallet>>(HttpMethod.Get, "UserWallet/GetAllUserWalletsAsync");
        }

        public Task<UserWallet> GetUserWalletByUserIdAsync(long userId)
        {
          return  _repositoryFactory.SendAsync<UserWallet>(HttpMethod.Get, $"UserWallet/GetUserWalletByUserIdAsync/{userId}");
        }

        public async Task<UserWalletDetails> GetUserWalletDetailsByUserIdAsync(long userId)
        {
            var url = Path.Combine("UserWallet/GetUserWalletDetailsByUserIdAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<UserWalletDetails>(HttpMethod.Get, url);
        }

        public Task<bool> InsertUserWalletLogAsyn(UserWalletLog userWalletLog)
        {
          return _repositoryFactory.SendAsync<UserWalletLog,bool>(HttpMethod.Post,"UserWallet/InsertUserWalletLogAsyn", userWalletLog);
        }

        public Task<bool> SaveUserWalletAsync(UserWallet userWallet)
        {
            return _repositoryFactory.SendAsync<UserWallet, bool>(HttpMethod.Post, "UserWallet/SaveUserWalletAsync", userWallet);
        }

        public Task<bool> UpdateUserWalletBalanceAsync(UserWalletLog userWalletLog)
        {
            return _repositoryFactory.SendAsync<UserWalletLog, bool>(HttpMethod.Post, "UserWallet/UpdateUserWalletBalanceAsync", userWalletLog);
        }

        public Task<bool> UpdateUserWalletBalanceAsync(UserWallet userWallet)
        {
            return _repositoryFactory.SendAsync<UserWallet, bool>(HttpMethod.Post, "UserWallet/UpdateUserWalletBalanceAsync", userWallet);
        }
    }
}
