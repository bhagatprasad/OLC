using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IOrderQueueService
    {
        Task<bool> InsertOrderQueueAsync(OrderQueue orderQueue);
        Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue);
        Task<List<OrderQueue>> GetOrderQueuesAsync();
        Task<List<OrderQueue>> GetPaymentOrderQueueAsync();
        Task<bool> DeleteOrderQueueAsync(long orderQueueId);
    }
}
