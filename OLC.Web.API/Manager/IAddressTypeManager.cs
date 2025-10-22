using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAddressTypeManager
    {
        Task<GetAddressType> GetUserAdressTypeByIdAsync(long Id);
        Task<List<GetAddressType>> GetUserAddressTypeAsync(long CreatedBy);
        Task<bool> InsertUserAddressTypeAsync(AddressType addressType);
        Task<bool> UpdateUserAddressTypeAsync(UpdateAddressType updateAddressType);
        Task<bool> DeleteUserAddressTypeAsync(long Id);
    }
}
