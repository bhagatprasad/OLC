using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IUserWalletService
    {
        Task<bool> SaveUserWalletAsync(UserWallet userWallet);
        Task<bool> UpdateUserWalletBalanceAsync(UserWallet userWallet);
        Task<UserWallet> GetUserWalletByUserIdAsync(long userId);
        Task<List<UserWallet>> GetAllUserWalletsAsync();
        Task<bool> InsertUserWalletLogAsyn(UserWalletLog userWalletLog);
        Task<List<UserWalletLog>> GetAllUsersWalletlogAsync();
        Task<List<UserWalletLog>> GetAllUserWalletlogByUserIdAsync(long userId);
        Task<List<UserWalletDetails>> GetAllExecutiveUserWalletDetailsAsync();
    }
}
