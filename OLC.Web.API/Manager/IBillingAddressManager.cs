using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IBillingAddressManager
    {
        Task<List<UserBillingAddress>> GetUserBillingAddressesAsync(long userId);
        Task<bool> InsertUserBillingAddressAsync(UserBillingAddress userBillingAddress);
        Task<UserBillingAddress> GetUserBillingAddressByIdAsync(long billingAddressId);
        Task<bool> UpdateUserBillingAddressAsync(UserBillingAddress userBillingAddress);
        Task<bool> DeleteUserBillingAddressAsync(long billingAddressId);
    }
}
