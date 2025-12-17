using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IBlockChainManager
    {
        Task<List<BlockChain>> GetBlockChainsAsync();
        Task<BlockChain> GetBlockChainByIdAsync(long id);
        Task<bool> InsertBlockChainAsync(BlockChain blockChain);
        Task<bool> UpdateBlockChainAsync(BlockChain blockChain);
        Task<bool> DeleteBlockChainAsync(long id);
    }
}
