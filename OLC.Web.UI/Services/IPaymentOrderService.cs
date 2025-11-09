using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IPaymentOrderService
    {
        Task<PaymentOrder> InsertPaymentOrderAsync(PaymentOrder paymentOrder);
        Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId);
        Task<List<PaymentOrder>> GetPaymentOrdersAsync();
        Task<PaymentOrder> ProcessPaymentStatusAsync(ProcessPaymentStatus processPaymentStatus);
        Task<List<UserPaymentOrder>> GetUserPaymentOrderListAsync(long userId);
        Task<List<ExecutivePaymentOrders>> GetExecutivePaymentOrdersAsync();
        Task<List<UserPaymentOrder>> GetAllUserPaymentOrdersAsync();
        Task<List<PaymentOrderHistory>> GetPaymentOrderHistoryAsync(long paymentOrderId);
        Task<PaymentOrderDetails> GetExecutivePaymentOrderDetailsAsync(long paymentOrderId);
        Task<PaymentOrder> ProcessPaymentOrderAsync(ProcessPaymentOrder processPaymentOrder);
    }
}
