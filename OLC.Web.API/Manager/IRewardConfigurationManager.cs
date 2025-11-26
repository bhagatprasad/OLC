using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IRewardConfigurationManager
    {
        Task<bool> SaveRewardConfigurationAsync(RewardConfiguration rewardConfiguration);
        Task<bool> UpdateRewardConfigurationAsync(RewardConfiguration rewardConfiguration);        
        Task<bool> DeleteRewardConfigurationAsync(long Id);
        Task<RewardConfiguration>GetRewardConfigurationsByIdAsync(long Id);
        Task<List<RewardConfiguration>> GetAllRewardConfigurationsAsync();

    }
}
