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
        
        public async Task<bool> InsertOrderQueuesAsync(OrderQueue orderQueue)
        {
            return await _repositoryFactory.SendAsync<OrderQueue, bool>(HttpMethod.Post, "OrderQueue/InsertOrderQueuesAsync", orderQueue);

        }
    }
}
