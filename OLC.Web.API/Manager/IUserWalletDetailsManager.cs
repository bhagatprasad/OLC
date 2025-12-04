using OLC.Web.API.Models;
namespace OLC.Web.API.Manager
{
    public interface IUserWalletDetailsManager
    {
        Task<UserWalletDetails> uspGetUserWalletDetailsByUserIdAsync(long userId);
    }
}
