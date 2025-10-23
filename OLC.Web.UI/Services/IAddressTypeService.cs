using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IAddressTypeService
    {
        Task<AddressType> GetAdressTypeByIdAsync(long addressTypeId);
        Task<List<AddressType>> GetAddressTypeAsync();
        Task<bool> InsertAddressTypeAsync(AddressType addressType);
        Task<bool> UpdateAddressTypeAsync(AddressType addressType);
        Task<bool> DeleteAddressTypeAsync(long Id);
    }
}
