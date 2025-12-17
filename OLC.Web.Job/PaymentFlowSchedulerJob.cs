using Hangfire;
using OLC.Web.API.Manager;

namespace OLC.Web.Job
{
    public class PaymentFlowSchedulerJob
    {
        private readonly IPaymentOrderManager _paymentOrderRepository;

        public PaymentFlowSchedulerJob(IPaymentOrderManager paymentOrderRepository)
        {
            _paymentOrderRepository = paymentOrderRepository;
        }

        public async Task RunAsync()
        {
            // 1️⃣ Orders needing auto approval
            var toApprove = await _paymentOrderRepository
                .GetOrdersForAutoApprovalAsync(transactionFeeId: 1);

            foreach (var order in toApprove)
            {
                BackgroundJob.Enqueue<AutoApprovePaymentOrderJob>(
                    x => x.AutoApproveAsync(order.Id.Value, order.ModifiedBy ?? 0));
            }

            // 2️⃣ Approved orders → payment
            var approvedOrders = await _paymentOrderRepository
                .GetApprovedOrdersForDepositAsync();

            foreach (var order in approvedOrders)
            {
                BackgroundJob.Enqueue<StripeDepositPaymentJob>(
                    x => x.ProcessDepositAsync(order));
            }
        }
    }

}
