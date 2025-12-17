using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IQueueConfigurationManager
    {
        Task<List<QueueConfiguration>> GetAllQueueConfigurationsAsync();
        Task<QueueConfiguration> GetQueueConfigurationByIdAsync(long id);
        Task<bool> SaveQueueConfigurationAsync(QueueConfiguration queueConfiguration);
        Task<bool> DeleteQueueConfigurationAsync(long id);
        Task<bool> UpdateQueueConfigurationAsync(QueueConfiguration queueConfiguration);
    }
}
