using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class AddressTypeService : IAddressTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public AddressTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteAddressTypeAsync(long addressTypeId)
        {
            var url = $"AddressType/DeleteAddressType?addressTypeId={addressTypeId}";
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<AddressType>> GetAddressTypeAsync()
        {
            return await _repositoryFactory.SendAsync<List<AddressType>>(HttpMethod.Get, "AddressType/GetAddressTypeAsync");
        }

        public async Task<AddressType> GetAddressTypeByIdAsync(long addressTypeId)
        {
            var url = Path.Combine("AddressType/GetAddressTypeByIdAsync", addressTypeId.ToString());
            return await _repositoryFactory.SendAsync<AddressType>(HttpMethod.Get, url);
        }

        // New: Single save method
        public async Task<bool> SaveAddressTypeAsync(AddressType addressType)
        {
            // Handle insert/update logic here or call the appropriate API
            if (addressType.Id > 0)
            {
                // Update
                return await _repositoryFactory.SendAsync<AddressType, bool>(HttpMethod.Post, "AddressType/UpdateAddressTypeAsync", addressType);
            }
            else
            {
                // Insert
                return await _repositoryFactory.SendAsync<AddressType, bool>(HttpMethod.Post, "AddressType/InsertAddressTypeAsync", addressType);
            }
        }

        // Keep for compatibility, but they can call SaveAddressTypeAsync internally if needed
        public async Task<bool> InsertAddressTypeAsync(AddressType addressType)
        {
            return await SaveAddressTypeAsync(addressType);
        }

        public async Task<bool> UpdateAddressTypeAsync(AddressType addressType)
        {
            return await SaveAddressTypeAsync(addressType);
        }
    }
}