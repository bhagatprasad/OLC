using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IDepositOrdereService
    {
        Task<List<DepositOrder>> GetAllDepositOrdersAsync();
        Task<bool> InsertDepositOrderAsync(DepositOrder depositOrder);
        Task<List<DepositOrder>> GetDepositOrderByOrderIdAsync(long paymentOrderId);
        Task<List<DepositOrder>> GetDepositOrderByUserIdAsync(long userId);
        Task<List<ExecutiveDepositOrderDetails>> uspGetAllExecutiveDepositOrderDetailsAsync();
    }
}
