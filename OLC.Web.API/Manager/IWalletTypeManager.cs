using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IWalletTypeManager
    {
        Task<List<WalletType>> GetAllWalletTypesAsync();
        Task<WalletType> GetWalletTypeByIdAsync(long id);
        Task<bool> InsertWalletTypeAsync(WalletType walletType);
        Task<bool> UpdateWalletTypeAsync(WalletType walletType);
        Task <bool> DeleteWalletTypeAsync(long id);
    }
}
