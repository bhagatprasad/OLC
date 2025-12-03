using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IPaymentOrderManager
    {
        Task<PaymentOrder> InsertPaymentOrderAsync(PaymentOrder paymentOrder);
        Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId);
        Task<List<PaymentOrder>> GetPaymentOrdersAsync();
        Task<List<PaymentOrderHistory>> GetPaymentOrderHistoryAsync(long paymentOrderId);
        Task<PaymentOrder> ProcessPaymentOrderAsync(ProcessPaymentOrder processPaymentOrder);
        Task<PaymentOrder> ProcessPaymentStatusAsync(ProcessPaymentStatus processPaymentStatus);
        Task<List<UserPaymentOrder>> GetUserPaymentOrderListAsync(long userId);
        Task<List<UserPaymentOrder>> GetAllUserPaymentOrdersAsync();
        Task<List<ExecutivePaymentOrders>> GetExecutivePaymentOrdersAsync();
        Task<PaymentOrderDetails> GetExecutivePaymentOrderDetailsAsync(long paymentOrderId);
        Task<ExecutivePaymentOrders> GetPaymentOrderDetailsAsync(long paymentOrderId);
        Task<List<ExecutivePaymentOrders>> GetExecutivePaymentOrderDetailsFilterAsync(PaymentOrderDetailsFilter paymentOrderDetailsFilter);
        Task<ExecutivePaymentOrderSum> GetAllExecutivePaymentOrderSumAsync(PaymentOrderDetailsFilter paymentOrderDetailsFilter);
        
    }
}
