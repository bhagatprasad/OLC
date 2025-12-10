using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IBlockChainService
    {
        Task<List<BlockChain>> GetBlockChainsAsync();
        Task<BlockChain> GetBlockChainByIdAsync(long id);
        Task<bool> InsertBlockChainAsync(BlockChain blockChain);
        Task<bool> UpdateBlockChainAsync(BlockChain blockChain);
        Task<bool> DeleteBlockChainAsync(long id);
    }
}
