using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class UserWalletDetailsService: IUserWalletDetailsService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public UserWalletDetailsService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<UserWalletDetails> GetUserWalletDetailsByUserIdAsync(long userId)
        {
            var url = Path.Combine("UserWalletDetails/GetUserWalletDetailsByUserIdAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<UserWalletDetails>(HttpMethod.Get, url);
        }
    }
}
