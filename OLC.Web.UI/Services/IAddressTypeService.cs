using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IAddressTypeService
    {
        
            Task<AddressType> GetAddressTypeByIdAsync(long addressTypeId);
            Task<List<AddressType>> GetAddressTypeAsync();
            Task<bool> SaveAddressTypeAsync(AddressType addressType);  
            Task<bool> InsertAddressTypeAsync(AddressType addressType);
            Task<bool> UpdateAddressTypeAsync(AddressType addressType);
            Task<bool> DeleteAddressTypeAsync(long addressTypeId);
        
    }    
}