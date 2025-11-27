using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class RewardConfigurationService:IRewardConfigurationService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public RewardConfigurationService(IRepositoryFactory repositoryFactory) 
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteRewardConfigurationAsync(long id)
        {
            var url = Path.Combine("RewardConfiguration/DeleteRewardConfigurationAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<RewardConfiguration> GetRewardConfigurationByIdAsync(long id)
        {
            var url = Path.Combine("RewardConfiguration/GetRewardConfigurationByIdAsync", id.ToString());
            return await _repositoryFactory.SendAsync<RewardConfiguration>(HttpMethod.Get, url);
        }

        public async Task<List<RewardConfiguration>> GetRewardConfigurationsAsync()
        {
            return await _repositoryFactory.SendAsync<List<RewardConfiguration>>(HttpMethod.Get, "RewardConfiguration/GetRewardConfigurationsAsync");
        }

        public async Task<bool> InsertRewardConfigurationAsync(RewardConfiguration rewardConfiguration)
        {
            return await _repositoryFactory.SendAsync<RewardConfiguration, bool>(HttpMethod.Post, "RewardConfiguration/InsertRewardConfigurationAsync");
        }

        public async Task<bool> UpdateRewardConfigurationAsync(RewardConfiguration rewardConfiguration)
        {
            return await _repositoryFactory.SendAsync<RewardConfiguration, bool>(HttpMethod.Post, "RewardConfiguration/UpdateRewardConfigurationAsync");
        }
    }
}
