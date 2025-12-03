using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class WalletTypeService : IWalletTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public WalletTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteWalletType(long id)
        {
            var url = Path.Combine("WalletType/DeleteWalletTypeAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<WalletType>> GetAllWalletTypes()
        {
            return await _repositoryFactory.SendAsync<List<WalletType>>(HttpMethod.Get, "WalletType/GetAllWalletTypesAsync");
        }

        public async Task<WalletType> GetWalletTypeById(long id)
        {
            var url = Path.Combine("WalletType/GetWalletTypeByIdAsync", id.ToString());
            return await _repositoryFactory.SendAsync<WalletType>(HttpMethod.Get, url);
        }

        public async Task<bool> SaveWalletType(WalletType walletType)
        {
            return await _repositoryFactory.SendAsync<WalletType, bool>(HttpMethod.Post, "WalletType/InsertWalletTypeAsync", walletType);
        }

        public async Task<bool> UpdateWalletType(WalletType walletType)
        {
            return await _repositoryFactory.SendAsync<WalletType, bool>(HttpMethod.Post, "WalletType/UpdateWalletTypeAsync", walletType);
        }
    }
}
