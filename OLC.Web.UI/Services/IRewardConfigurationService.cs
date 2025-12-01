using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IRewardConfigurationService
    {
        Task<List<RewardConfiguration>> GetAllRewardConfigurationsAsync();
        Task<RewardConfiguration> GetRewardConfigurationByIdAsync(long id);
        Task<bool> SaveRewardConfigurationAsync(RewardConfiguration rewardConfiguration);
        Task<bool> UpdateRewardConfigurationAsync(RewardConfiguration rewardConfiguration);
        Task<bool> DeleteRewardConfigurationAsync(long id);
    }
}
