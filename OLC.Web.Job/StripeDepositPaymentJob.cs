using Hangfire;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using Stripe;

namespace OLC.Web.Job
{
    public class StripeDepositPaymentJob
    {
        private readonly IDepositOrderManager _depositOrderManager;
        private readonly IPaymentOrderManager _paymentOrderManager;

        public StripeDepositPaymentJob(
            IDepositOrderManager depositOrderManager,
            IPaymentOrderManager paymentOrderManager)
        {
            _depositOrderManager = depositOrderManager;
            _paymentOrderManager = paymentOrderManager;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task ProcessDepositAsync(PaymentOrder order)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51SJ2Nu32ZfCJ3T7ZvHRGxGZ1vVpZQzYTNWejuB9sQrwyJ9J0srziK7R40YojN36uo8zKn0AussEd2vYMb5cc9NWf00cYMmzEh7";

                // 🔹 Stripe refund / deposit
                var refundService = new RefundService();
                var refund = await refundService.CreateAsync(new RefundCreateOptions
                {
                    Charge = order.StripePaymentChargeId,
                    Amount = Convert.ToInt64(order.TotalAmountToDepositToCustomer)
                });

                var depositOrder = new DepositOrder
                {
                    PaymentOrderId = order.Id,
                    OrderReference = order.OrderReference,
                    DepositeAmount = order.TotalAmountToDepositToCustomer,
                    ActualDepositeAmount = order.TotalAmountToDepositToCustomer,
                    PendingDepositeAmount = 0,
                    StripeDepositeChargeId = refund.Id,
                    StripeDepositeIntentId = refund.PaymentIntentId,
                    IsPartialPayment = 0,
                    CreatedBy = order.ModifiedBy,
                    IsActive = true
                };

                await _depositOrderManager.InsertDepositOrderAsync(depositOrder);
            }
            catch (Exception ex)
            {
                throw; // Required for Hangfire retry
            }
        }
    }

}
