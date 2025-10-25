using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IBillingAddressService
    {
        Task<List<UserBillingAddress>> GetUserBillingAddressesAsync(long userId);
        Task<bool> InsertUserBillingAddressAsync(UserBillingAddress userBillingAddress);
        Task<UserBillingAddress> GetUserBillingAddressByIdAsync(long billingAddressId);
        Task<bool> UpdateUserBillingAddressAsync(UserBillingAddress userBillingAddress);
        Task<bool> DeleteUserBillingAddressAsync(long billingAddressId);
    }
}
