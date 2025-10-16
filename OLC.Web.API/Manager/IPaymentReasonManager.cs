using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IPaymentReasonManager
    {
        Task<List<PaymentReason>> GetPaymentReasonsAsync();

    }
}
