using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class DepositOrderService : IDepositOrdereService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public DepositOrderService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<DepositOrder>> GetAllDepositOrdersAsync()
        {
            return await _repositoryFactory.SendAsync<List<DepositOrder>>(HttpMethod.Get, "DepositOrder/GetAllDepositOrdersAsync");
        }

        public async Task<List<DepositOrder>> GetDepositOrderByOrderIdAsync(long paymentOrderId)
        {
            var url = Path.Combine("DepositOrder/GetDepositOrderByOrderIdAsync", paymentOrderId.ToString());
            return await _repositoryFactory.SendAsync<List<DepositOrder>>(HttpMethod.Get, url);
        }

        public async Task<List<DepositOrder>> GetDepositOrderByUserIdAsync(long userId)
        {
            var url = Path.Combine("DepositOrder/GetDepositOrderByUserIdAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<List<DepositOrder>>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertDepositOrderAsync(DepositOrder depositOrder)
        {
            return await _repositoryFactory.SendAsync<DepositOrder, bool>(HttpMethod.Post, "DepositOrder/InsertDepositOrderAsync",depositOrder);
        }

        public async Task<List<ExecutiveDepositOrderDetails>> GetAllExecutiveDepositOrderDetailsAsync()
        {
            var url = Path.Combine("DepositOrder/GetAllExecutiveDepositOrderDetailsAsync");
            return await _repositoryFactory.SendAsync<List<ExecutiveDepositOrderDetails>>(HttpMethod.Get, url);
        }
    }
}
