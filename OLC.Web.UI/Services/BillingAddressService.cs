using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class BillingAddressService : IBillingAddressService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public BillingAddressService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteUserBillingAddressAsync(long billingAddressId)
        {
            var url = Path.Combine("BillingAddress/DeleteUserBillingAddressAsync", billingAddressId.ToString());

            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<UserBillingAddress> GetUserBillingAddressByIdAsync(long billingAddressId)
        {
            var url = Path.Combine("BillingAddress/GetUserBillingAddressByIdAsync", billingAddressId.ToString());

            return await _repositoryFactory.SendAsync<UserBillingAddress>(HttpMethod.Get, url);
        }

        public async Task<List<UserBillingAddress>> GetUserBillingAddressesAsync(long userId)
        {
            var url = Path.Combine("BillingAddress/GetUserBillingAddressesAsync", userId.ToString());

            return await _repositoryFactory.SendAsync<List<UserBillingAddress>>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertUserBillingAddressAsync(UserBillingAddress userBillingAddress)
        {
            return await _repositoryFactory.SendAsync<UserBillingAddress, bool>(HttpMethod.Post, "BillingAddress/SaveUserBillingAddressAsync", userBillingAddress);
        }

        public async Task<bool> UpdateUserBillingAddressAsync(UserBillingAddress userBillingAddress)
        {
            return await _repositoryFactory.SendAsync<UserBillingAddress, bool>(HttpMethod.Post, "BillingAddress/UpdateUserBillingAddressAsync", userBillingAddress);
        }
    }
}
