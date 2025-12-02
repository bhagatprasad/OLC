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
            var url = Path.Combine("PaymentOrder/GetPaymentOrdersByUserIdAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<List<PaymentOrder>>(HttpMethod.Get, url);
        }

        public async Task<List<PaymentOrder>> GetPaymentOrdersAsync()
        {
            return await _repositoryFactory.SendAsync<List<PaymentOrder>>(HttpMethod.Get, "PaymentOrder/GetAllPaymentOrdersAsync");
        }

        public async Task<PaymentOrder> InsertPaymentOrderAsync(PaymentOrder paymentOrder)
        {
            return await _repositoryFactory.SendAsync<PaymentOrder, PaymentOrder>(HttpMethod.Post, "PaymentOrder/SavePaymentOrderAsync", paymentOrder);
        }

        public async Task<PaymentOrder> ProcessPaymentStatusAsync(ProcessPaymentStatus processPaymentStatus)
        {
            return await _repositoryFactory.SendAsync<ProcessPaymentStatus, PaymentOrder>(HttpMethod.Post, "PaymentOrder/ProcessPaymentStatusAsync", processPaymentStatus);
        }

        public async Task<List<UserPaymentOrder>> GetUserPaymentOrderListAsync(long userId)
        {
            var url = Path.Combine("PaymentOrder/GetUserPaymentOrderListAsync", userId.ToString());
            return await _repositoryFactory.SendAsync<List<UserPaymentOrder>>(HttpMethod.Get, url);
        }

        public async Task<List<ExecutivePaymentOrders>> GetExecutivePaymentOrdersAsync()
        {
            return await _repositoryFactory.SendAsync<List<ExecutivePaymentOrders>>(HttpMethod.Get, "PaymentOrder/GetExecutivePaymentOrdersAsync");
        }

        public async Task<List<UserPaymentOrder>> GetAllUserPaymentOrdersAsync()
        {
            return await _repositoryFactory.SendAsync<List<UserPaymentOrder>>(HttpMethod.Get, "PaymentOrder/GetAllUserPaymentOrdersAsync");
        }

        public async Task<List<PaymentOrderHistory>> GetPaymentOrderHistoryAsync(long paymentOrderId)
        {
            return await _repositoryFactory.SendAsync<List<PaymentOrderHistory>>(HttpMethod.Get, "PaymentOrder/GetPaymentOrderHistoryAsync");
        }

        public async Task<PaymentOrderDetails> GetExecutivePaymentOrderDetailsAsync(long paymentOrderId)
        {
            var url = Path.Combine("PaymentOrder/GetExecutivePaymentOrderDetailsAsync", paymentOrderId.ToString());
            return await _repositoryFactory.SendAsync<PaymentOrderDetails>(HttpMethod.Get, url);
        }


        public async Task<PaymentOrder> ProcessPaymentOrderAsync(ProcessPaymentOrder processPaymentOrder)
        {
            return await _repositoryFactory.SendAsync<ProcessPaymentOrder, PaymentOrder>(HttpMethod.Post, "PaymentOrder/ProcessPaymentOrderAsync", processPaymentOrder);
        }

        public async Task<bool> HandleDepositPaymentAsync(ProcessDepositePayment processDepositePayment)
        {
            return await _repositoryFactory.SendAsync<ProcessDepositePayment, bool>(HttpMethod.Post, "PaymentOrder/HandleDepositPaymentAsync", processDepositePayment);
        }

        public async Task<ExecutivePaymentOrders> GetPaymentOrderDetailsAsync(long paymentOrderId)
        {
            var url = Path.Combine("PaymentOrder/GetPaymentOrderDetailsAsync", paymentOrderId.ToString());
            return await _repositoryFactory.SendAsync<ExecutivePaymentOrders>(HttpMethod.Get, url);
        }
    }
}
