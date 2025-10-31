using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class PaymentReasonService : IPaymentReasonService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PaymentReasonService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<PaymentReason>> GetPaymentReasonsAsync()
        {
            return await _repositoryFactory.SendAsync<List<PaymentReason>>(HttpMethod.Get, "PaymentReason/GetPaymentReasonsAsync");
        }
    }
}
