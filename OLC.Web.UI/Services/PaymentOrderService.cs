using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PaymentOrderService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId)
        {
            var url = Path.Combine("PaymentOrder/GetPaymentOrdersByUserIdAsync",userId.ToString());
            return await _repositoryFactory.SendAsync<List<PaymentOrder>>(HttpMethod.Get, url);
        }
       
        public async Task<List<PaymentOrder>> GetPaymentOrdersAsync()
        {
            return await _repositoryFactory.SendAsync<List<PaymentOrder>>(HttpMethod.Get, "PaymentOrder/GetAllPaymentOrdersAsync");
        }
        public async Task<bool> InsertPaymentOrderAsync(PaymentOrder paymentOrder)
        {
            return await _repositoryFactory.SendAsync<PaymentOrder, bool>(HttpMethod.Post, "PaymentOrder/SavePaymentOrderAsync", paymentOrder);
        }
    }
}
