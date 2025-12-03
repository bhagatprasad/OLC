using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IWalletTypeService
    {
        Task<List<WalletType>> GetAllWalletTypes();
        Task<WalletType> GetWalletTypeById(long id);
        Task<bool> SaveWalletType(WalletType walletType);
        Task<bool> UpdateWalletType(WalletType walletType);
        Task<bool> DeleteWalletType(long id);
    }
}
