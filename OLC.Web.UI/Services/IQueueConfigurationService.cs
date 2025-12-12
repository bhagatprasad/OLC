using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IQueueConfigurationService
    {
        Task<List<QueueConfiguration>> GetAllQueueConfigurationsAsync();
        Task<QueueConfiguration> GetQueueConfigurationByIdAsync(long id);
        Task<bool> SaveQueueConfigurationAsync(QueueConfiguration queueConfiguration);
        Task<bool> DeleteQueueConfigurationAsync(long id);
        Task<bool> UpdateQueueConfigurationAsync(QueueConfiguration queueConfiguration);
    }
}
