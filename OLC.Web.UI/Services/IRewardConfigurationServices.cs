using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IRewardConfigurationServices
    {
        Task<bool> SaveRewardConfigurationAsync(RewardConfiguration rewardConfiguration);
        Task<bool> UpdateRewardConfigurationAsync(RewardConfiguration rewardConfiguration);
        Task<bool> DeleteRewardConfigurationAsync(long Id);
        Task<RewardConfiguration> GetRewardConfigurationsByIdAsync(long Id);
        Task<List<RewardConfiguration>> GetAllRewardConfigurationsAsync();
    }
}
