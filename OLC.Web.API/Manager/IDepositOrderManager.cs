using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IDepositOrderManager
    {
        Task<List<DepositOrder>> GetAllDepositOrdersAsync();
        Task<bool> InsertDepositOrderAsync(DepositOrder depositOrder);
        Task<List<DepositOrder>> GetDepositOrderByOrderIdAsync(long paymentOrderId);
        Task<List<DepositOrder>> GetDepositOrderByUserIdAsync(long userId);
    }
}
