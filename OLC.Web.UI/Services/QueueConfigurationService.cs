using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class QueueConfigurationService :IQueueConfigurationService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public QueueConfigurationService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteQueueConfigurationAsync(long id)
        {
            var url = Path.Combine("QueueConfiguration/DeleteQueueConfigurationAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<QueueConfiguration>> GetAllQueueConfigurationsAsync()
        {
            return await _repositoryFactory.SendAsync<List<QueueConfiguration>>(HttpMethod.Get, "QueueConfiguration/GetAllQueueConfigurationsAsync");
        }

        public async Task<QueueConfiguration> GetQueueConfigurationByIdAsync(long id)
        {
            var url = Path.Combine("QueueConfiguration/GetQueueConfigurationByIdAsync", id.ToString());
            return await _repositoryFactory.SendAsync<QueueConfiguration>(HttpMethod.Get, url);
        }

        public async Task<bool> SaveQueueConfigurationAsync(QueueConfiguration queueConfiguration)
        {
            return await _repositoryFactory.SendAsync<QueueConfiguration, bool>(HttpMethod.Post, "QueueConfiguration/SaveQueueConfigurationAsync", queueConfiguration);
        }

        public async Task<bool> UpdateQueueConfigurationAsync(QueueConfiguration QueueConfiguration)
        {
            return await _repositoryFactory.SendAsync<QueueConfiguration, bool>(HttpMethod.Post, "QueueConfiguration/UpdateQueueConfigurationAsync", QueueConfiguration);
        }
    }
}
