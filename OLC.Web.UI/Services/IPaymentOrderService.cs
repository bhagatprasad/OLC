using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IPaymentOrderService
    {
        Task<bool> InsertPaymentOrderAsync(PaymentOrder paymentOrder);
        Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId);
        Task<List<PaymentOrder>> GetPaymentOrdersAsync();
    }
}
