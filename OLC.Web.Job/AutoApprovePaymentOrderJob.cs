using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.Job
{
    public class AutoApprovePaymentOrderJob
    {
        private readonly IPaymentOrderManager _paymentOrderManager;

        public AutoApprovePaymentOrderJob(IPaymentOrderManager paymentOrderManager)
        {
            _paymentOrderManager = paymentOrderManager;
        }

        public async Task AutoApproveAsync(long paymentOrderId, long userId)
        {
            var processPaymentStatus = new ProcessPaymentStatus
            {
                PaymentOrderId = paymentOrderId,
                OrderStatusId = 5,
                PaymentStatusId = 1,
                Description = "Auto approved by system",
                UserId = userId.ToString()
            };

            await _paymentOrderManager.ProcessPaymentStatusAsync(processPaymentStatus);
        }
    }

}
