using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IOrderQueueManager
    {
        Task<bool> InsertOrderQueueAsync(long paymentOrderId);
        
    }
}
