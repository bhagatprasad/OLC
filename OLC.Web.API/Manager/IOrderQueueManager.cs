using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IOrderQueueManager
    {
        Task<bool> InsertOrderQueueAsync(OrderQueue orderQueue);
        Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue);
        Task<bool> DeleteOrderQueueAsync(long orderQueueId);
        Task<List<OrderQueue>> GetOrderQueuesAsync ();
    }
}
