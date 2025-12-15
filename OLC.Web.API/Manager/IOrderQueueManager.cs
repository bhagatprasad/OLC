using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IOrderQueueManager
    {
        Task<long> InsertOrderQueueAsync(OrderQueue orderQueue);
        Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue);
        Task<bool> DeleteOrderQueueAsync(long orderQueueId, string reason);
    }
}
