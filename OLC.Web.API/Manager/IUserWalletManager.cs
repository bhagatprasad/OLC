using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserWalletManager
    {
        Task<bool> SaveUserWalletAsync (UserWallet userWallet); 
        Task<bool> UpdateUserWalletBalanceAsync(UserWallet userWallet);
        Task<UserWallet> GetUserWalletByUserIdAsync(long userId);
        Task<List<UserWallet>> GetAllUserWalletsAsync();
    }
}
