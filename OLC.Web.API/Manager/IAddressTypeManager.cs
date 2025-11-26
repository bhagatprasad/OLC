using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAddressTypeManager
    {
        Task<AddressType> GetAdressTypeByIdAsync(long addressTypeId);
        Task<List<AddressType>> GetAddressTypeAsync();
        Task<bool> InsertAddressTypeAsync(AddressType addressType);
        Task<bool> UpdateAddressTypeAsync(AddressType addressType);
        Task<bool> DeleteAddressTypeAsync(long addressTypeId);
        Task<bool> ActivateAddressTypeAsync(AddressType addressType);
    }
}

