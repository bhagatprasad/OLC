using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IPaymentReasonService
    {
        Task<List<PaymentReason>> GetPaymentReasonsAsync();
    }
}
