using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IOrderQueueService
    {
        Task<List<OrderQueue>> GetOrderQueuesAsync();
        Task<bool> InsertOrderQueuesAsync(OrderQueue orderQueue);
    }
}
