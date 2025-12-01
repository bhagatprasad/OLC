using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete]
        public async Task<bool> DeleteRewardConfigurationAsync(long id)
        {
            var url = Path.Combine("api/RewardConfiguration/DeleteRewardConfigurationAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        [HttpGet]
        public async Task<List<RewardConfiguration>> GetAllRewardConfigurationsAsync()
        {
            return await _repositoryFactory.SendAsync<List<RewardConfiguration>>(HttpMethod.Get, "RewardConfiguration/GetAllRewardConfigurationsAsync");
        }

        public async Task<RewardConfiguration> GetRewardConfigurationByIdAsync(long id)
        {
            var url = Path.Combine("RewardConfiguration/GetRewardConfigurationByIdAsync", id.ToString());
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
