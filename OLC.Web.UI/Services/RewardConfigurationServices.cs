using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class RewardConfigurationServices: IRewardConfigurationServices
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public RewardConfigurationServices(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteRewardConfigurationAsync(long Id)
        {
            var url = Path.Combine("RewardConfiguration/DeleteRewardConfigurationAsync", Id.ToString());

            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<RewardConfiguration>> GetAllRewardConfigurationsAsync()
        {
            return await _repositoryFactory.SendAsync<List<RewardConfiguration>>(HttpMethod.Get, "RewardConfiguration/GetAllRewardConfigurationsAsync");
        }

        public async Task<RewardConfiguration> GetRewardConfigurationsByIdAsync(long Id)
        {
            var url = Path.Combine("RewardConfiguration/GetRewardConfigurationsByIdAsync", Id.ToString());
            return await _repositoryFactory.SendAsync<RewardConfiguration>(HttpMethod.Get, url);
        }

        public async Task<bool> SaveRewardConfigurationAsync(RewardConfiguration rewardConfiguration)
        {
            return await _repositoryFactory.SendAsync<RewardConfiguration, bool>(HttpMethod.Post, "RewardConfiguration/SaveRewardConfigurationAsync", rewardConfiguration);
        }

        public async Task<bool> UpdateRewardConfigurationAsync(RewardConfiguration rewardConfiguration)
        {
            return await _repositoryFactory.SendAsync<RewardConfiguration, bool>(HttpMethod.Post, "RewardConfiguration/UpdateRewardConfigurationAsync", rewardConfiguration);
        }
    }
}
