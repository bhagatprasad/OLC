using OLC.Web.UI.Models;
namespace OLC.Web.UI.Services
{
    public interface IUserWalletDetailsService
    {
        Task<UserWalletDetails> GetUserWalletDetailsByUserIdAsync(long userId);
    }
}
