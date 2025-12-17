using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class OrderQueueService: IOrderQueueService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public OrderQueueService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<OrderQueue>> GetOrderQueuesAsync()
        {
            return await _repositoryFactory.SendAsync<List<OrderQueue>>(HttpMethod.Get, "OrderQueue/GetOrderQueuesAsync");
        }

        public async Task<List<OrderQueue>> GetPaymentOrderQueueAsync()
        {
            return await _repositoryFactory.SendAsync<List<OrderQueue>>(HttpMethod.Get, "OrderQueue/GetPaymentOrderQueueAsync");
        }

        public async Task<bool> InsertOrderQueueAsync(OrderQueue orderQueue)
        {
            return await _repositoryFactory.SendAsync<OrderQueue, bool>(HttpMethod.Post, "OrderQueue/InsertOrderQueuesAsync", orderQueue);
        }

        public async Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue)
        {
            return await _repositoryFactory.SendAsync<OrderQueue, bool>(HttpMethod.Post, "OrderQueue/UpdateOrderQueueAsync",orderQueue);
        }

        public async Task<bool> DeleteOrderQueueAsync(long orderQueueId)
        {
            var url = Path.Combine("OrderQueue / DeleteOrderQueueAsync", orderQueueId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async  Task<List<OrderQueueHistory>> GetOrderQueueHistoryByPaymentOrderIdAsync(long paymentOrderId)
        {
            var url = Path.Combine("OrderQueue/GetOrderQueueHistoryByPaymentOrderIdAsync", paymentOrderId.ToString());
            return await _repositoryFactory.SendAsync<List<OrderQueueHistory>>(HttpMethod.Get, url);
        }
    }
}
