using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IPaymentOrderManager
    {
        Task<bool> InsertPaymentOrderAsync(PaymentOrder paymentOrder);
        Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId);
        Task<List<PaymentOrder>> GetPaymentOrdersAsync();
        Task<List<PaymentOrderHistory>> GetPaymentOrderHistoryAsync(long paymentOrderId);
        Task<bool> InsertPaymentOrderHistoryAsync(PaymentOrderHistory paymentOrderHistory);
    }
}
