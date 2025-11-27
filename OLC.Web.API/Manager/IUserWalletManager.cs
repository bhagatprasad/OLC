using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserWalletManager
    {
        Task<bool> SaveUserWalletAsync (UserWallet userWallet); 
        Task<bool> UpdateUserWalletBalanceAsync(UserWalletLog userWalletLog);
        Task<UserWallet> GetUserWalletByUserIdAsync(long userId);
        Task<List<UserWallet>> GetAllUserWalletsAsync();
        Task<bool> InsertUserWalletLogAsyn(UserWalletLog userWalletLog);
        Task<List<UserWalletLog>> GetAllUsersWalletlogAsync();
        Task<List<UserWalletLog>> GetAllUserWalletlogByUserIdAsync(long userId);
    }
}
