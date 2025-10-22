using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAddressTypeManager
    {
        Task<GetAddressType> GetUserAdressTypeByIdAsync(long addressTypeId);
        Task<List<GetAddressType>> GetUserAddressTypeAsync();
        Task<bool> InsertUserAddressTypeAsync(AddressType addressType);
        Task<bool> UpdateUserAddressTypeAsync(UpdateAddressType updateAddressType);
        Task<bool> DeleteUserAddressTypeAsync(long Id);
    }
}
