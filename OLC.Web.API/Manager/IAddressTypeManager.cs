using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAddressTypeManager
    {
        Task<AddressType> GetUserAdressTypeByIdAsync(long addressTypeId);
        Task<List<AddressType>> GetUserAddressTypeAsync();
        Task<bool> InsertUserAddressTypeAsync(AddressType addressType);
        Task<bool> UpdateUserAddressTypeAsync(UpdateAddressType updateAddressType);
        Task<bool> DeleteUserAddressTypeAsync(long Id);
    }
}
