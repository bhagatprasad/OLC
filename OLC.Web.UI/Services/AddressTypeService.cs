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

        public async Task<bool> ActivateAddressTypeAsync(AddressType addressType)
        {
            return await _repositoryFactory.SendAsync<AddressType, bool>(HttpMethod.Post, "AddressType/ActivateAddressTypeAsync", addressType);
        }

        public async Task<bool> DeleteAddressTypeAsync(long addressTypeId)
        {
            var url = Path.Combine("AddressType/DeleteAddressTypeAsync", addressTypeId.ToString());
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
        
        public async Task<bool> InsertAddressTypeAsync(AddressType addressType)
        {
            return await _repositoryFactory.SendAsync<AddressType, bool>(HttpMethod.Post, "AddressType/InsertAddressTypeAsync", addressType);
        }

        public async Task<bool> UpdateAddressTypeAsync(AddressType addressType)
        {
            return await _repositoryFactory.SendAsync<AddressType, bool>(HttpMethod.Post, "AddressType/UpdateAddressTypeAsync", addressType);
        }
    }
}