using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IPaymentOrderManager
    {
        Task<bool> InsertPaymentOrderAsync (PaymentOrder paymentOrder);
        Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId);
        Task<List<PaymentOrder>> GetPaymentOrdersAsync();
    }
}
