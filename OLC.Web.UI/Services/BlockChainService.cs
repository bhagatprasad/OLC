using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class BlockChainService : IBlockChainService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public BlockChainService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteBlockChainAsync(long id)
        {
            var url = Path.Combine("BlockChain/DeleteBlockChainAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<BlockChain>> GetBlockChainsAsync()
        {
            return await _repositoryFactory.SendAsync<List<BlockChain>>(HttpMethod.Get, "BlockChain/GetBlockChainsAsync");
        }

        public async Task<BlockChain> GetBlockChainByIdAsync(long id)
        {
            var url = Path.Combine("BlockChain/GetBlockChainByIdAsync",id.ToString());
            return await _repositoryFactory.SendAsync<BlockChain>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertBlockChainAsync(BlockChain blockChain)
        {
            return await _repositoryFactory.SendAsync<BlockChain, bool>(HttpMethod.Post, "BlockChain/InsertBlockChainAsync",blockChain);
        }

        public async Task<bool> UpdateBlockChainAsync(BlockChain blockChain)
        {
            return await _repositoryFactory.SendAsync<BlockChain,bool>(HttpMethod.Post, "BlockChain/UpdateBlockChainAsync",blockChain);
        }
    }
}
