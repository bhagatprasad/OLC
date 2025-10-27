using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using Stripe;
using Stripe.Checkout;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    public class StripeController : Controller
    {
        private readonly INotyfService _notyfService;
        public StripeController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStripeSession([FromBody] CreateSessionRequest request)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51SJ2Nu32ZfCJ3T7ZvHRGxGZ1vVpZQzYTNWejuB9sQrwyJ9J0srziK7R40YojN36uo8zKn0AussEd2vYMb5cc9NWf00cYMmzEh7";

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = request.Currency ?? "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Payment Transfer",
                                Description = $"Transfer to bank account"
                            },
                            UnitAmount = (long)(request.Amount * 100), // Convert to cents
                        },
                        Quantity = 1,
                    },
                },
                    Mode = "payment",
                    SuccessUrl = request.SuccessUrl,
                    CancelUrl = request.CancelUrl,
                    CustomerEmail = request.CustomerEmail,
                    Metadata = request.Metadata
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return Ok(new { success = true, session = new { id = session.Id, url = session.Url } });
            }
            catch (StripeException e)
            {
                return BadRequest(new { success = false, error = e.Message });
            }
        }
    }
}
