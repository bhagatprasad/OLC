using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IQueueProcessingHistoryManager
    {
        Task<long> InsertHistoryAsync(QueueProcessingHistory history);
        Task<List<QueueProcessingHistory>> GetHistoryByOrderAsync(long orderQueueId);
    }
}
