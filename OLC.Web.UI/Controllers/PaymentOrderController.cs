using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Helper;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using Stripe;
using Stripe.Checkout;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class PaymentOrderController : Controller
    {
        private readonly IPaymentOrderService _paymentOrderService;
        private readonly INotyfService _notyfService;
        public PaymentOrderController(IPaymentOrderService paymentOrderService, INotyfService notyfService)
        {
            _paymentOrderService = paymentOrderService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetPaymentOrdersByUserId(long userId)
        {
            try
            {
                var response = await _paymentOrderService.GetPaymentOrdersByUserIdAsync(userId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Executive")]
        public async Task<IActionResult> GetAllPaymentOrders()
        {
            try
            {
                var response = await _paymentOrderService.GetPaymentOrdersAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PlacePaymentOrder([FromBody] PaymentOrder paymentOrder)
        {
            try
            {


                var response = await _paymentOrderService.InsertPaymentOrderAsync(paymentOrder);

                if (response == null)
                    _notyfService.Warning("Unable to place your order , please try again");
                else
                    _notyfService.Success($"Your order placed successfully , the order refernace {response.OrderReference.ToString()}");

                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Success(string session_id)
        {
            PaymentSuccessViewModel successModel = new PaymentSuccessViewModel();
            try
            {
                if (string.IsNullOrEmpty(session_id))
                {
                    ViewBag.Error = "Invalid session ID";
                    return View(successModel);
                }

                // Initialize Stripe with secret key
                StripeConfiguration.ApiKey = "sk_test_51SJ2Nu32ZfCJ3T7ZvHRGxGZ1vVpZQzYTNWejuB9sQrwyJ9J0srziK7R40YojN36uo8zKn0AussEd2vYMb5cc9NWf00cYMmzEh7";

                // Retrieve the session from Stripe to verify payment status
                var service = new SessionService();
                Session session = await service.GetAsync(session_id);

                if (session == null)
                {
                    ViewBag.Error = "Payment session not found";
                    return View(successModel);
                }

                // Check if payment was successful
                if (session.PaymentStatus == "paid")
                {
                    // Retrieve the Payment Intent to get the charge ID
                    string chargeId = null;
                    if (!string.IsNullOrEmpty(session.PaymentIntentId))
                    {
                        var paymentIntentService = new PaymentIntentService();
                        var paymentIntent = await paymentIntentService.GetAsync(session.PaymentIntentId);

                        // Get the latest charge ID (no expansion needed for the ID)
                        chargeId = paymentIntent.LatestChargeId;  // e.g., "ch_123456789"
                    }

                    // Extract metadata from the session
                    var metadata = session.Metadata;

                    // Prepare success view model
                    successModel = new PaymentSuccessViewModel
                    {
                        SessionId = session_id,
                        PaymentIntentId = session.PaymentIntentId,
                        AmountTotal = session.AmountTotal.HasValue ? session.AmountTotal.Value / 100m : 0, // Convert from cents
                        Currency = session.Currency?.ToUpper() ?? "USD",
                        CustomerEmail = session.CustomerEmail,
                        CustomerName = session.CustomerDetails?.Name,
                        PaymentMethod = session.PaymentMethodTypes?.FirstOrDefault() ?? "card",
                        Created = session.Created,
                        UserId = metadata?.ContainsKey("userId") == true ? metadata["userId"] : null,
                        CreditCardId = metadata?.ContainsKey("creditCardId") == true ? metadata["creditCardId"] : null,
                        BankAccountId = metadata?.ContainsKey("bankAccountId") == true ? metadata["bankAccountId"] : null,
                        BillingAddressId = metadata?.ContainsKey("billingAddressId") == true ? metadata["billingAddressId"] : null,
                        PaymentOrderId = metadata?.ContainsKey("PaymentOrderId") == true ? Convert.ToInt64(metadata["PaymentOrderId"]) : 0,
                        OrderReference = metadata?.ContainsKey("OrderReference") == true ? metadata["OrderReference"] : null,
                        ChargeId = chargeId
                    };

                    // Here you can update your database with the payment success (include chargeId)
                    await UpdateOrderStatus(successModel);

                    ViewBag.SuccessModel = successModel;
                    ViewBag.IsSuccess = true;
                }
                else
                {
                    ViewBag.Error = "Payment was not successful";
                    ViewBag.IsSuccess = false;
                }

                return View(successModel);
            }
            catch (StripeException ex)
            {
                ViewBag.Error = $"Stripe error: {ex.Message}";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return View();
            }
        }


        // GET: /Payment/Cancel
        [HttpGet]
        public IActionResult Cancel()
        {
            try
            {
                // You can retrieve the session_id from query string if needed
                var sessionId = HttpContext.Request.Query["session_id"].ToString();

                if (!string.IsNullOrEmpty(sessionId))
                {
                    // Optional: Retrieve session details to show what was cancelled
                    ViewBag.SessionId = sessionId;
                }

                // You can also get other query parameters
                ViewBag.ReturnUrl = Url.Action("Index", "UserBoard");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return View();
            }
        }

        // Helper method to update order status in your database
        private async Task<PaymentOrder> UpdateOrderStatus(PaymentSuccessViewModel successModel)
        {
            try
            {
                ProcessPaymentStatus processPaymentStatus = new ProcessPaymentStatus();
                processPaymentStatus.PaymentOrderId = successModel.PaymentOrderId;
                processPaymentStatus.ChargeId = successModel.ChargeId;
                processPaymentStatus.OrderStatusId = 42;
                processPaymentStatus.PaymentStatusId = 24;
                processPaymentStatus.Description = "Payment successfull";
                processPaymentStatus.PaymentMethod = successModel.PaymentMethod;
                processPaymentStatus.PaymentIntentId = successModel.PaymentIntentId;
                processPaymentStatus.SessionId = successModel.SessionId;
                processPaymentStatus.UserId = successModel.UserId;
                return await _paymentOrderService.ProcessPaymentStatusAsync(processPaymentStatus);

            }
            catch (Exception ex)
            {
                // Log the error but don't throw - we don't want to show error to user if DB update fails
                Console.WriteLine($"Error updating order status: {ex.Message}");
                throw ex;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Executive, User")]
        public async Task<IActionResult> GetUserPaymentOrderList(long userId)
        {
            try
            {
                var response = await _paymentOrderService.GetUserPaymentOrderListAsync(userId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Executive")]
        public async Task<IActionResult> GetExecutivePaymentOrders()
        {
            try
            {
                var response = await _paymentOrderService.GetExecutivePaymentOrdersAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetPaymentOrderDetails(long paymentOrderId)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Executive, User")]
        public async Task<IActionResult> GetExecutivePaymentOrderDetails(long paymentOrderId)
        {
            try
            {
                var response = await _paymentOrderService.GetExecutivePaymentOrderDetailsAsync(paymentOrderId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "AdminiStrator,Executive")]
        public async Task<IActionResult> ProcessPaymentOrder([FromBody] ProcessPaymentOrder processPaymentOrder)
        {
            try
            {


                var response = await _paymentOrderService.ProcessPaymentOrderAsync(processPaymentOrder);

                if (response != null)
                {
                    var paymentOrders = await _paymentOrderService.GetExecutivePaymentOrdersAsync();
                    return Json(new { data = paymentOrders });
                }

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "AdminiStrator,Executive")]
        public async Task<IActionResult> HandleDepositPayment([FromBody] ProcessDepositePayment processDepositePayment)
        {
            try
            {

                StripeConfiguration.ApiKey = "sk_test_51SJ2Nu32ZfCJ3T7ZvHRGxGZ1vVpZQzYTNWejuB9sQrwyJ9J0srziK7R40YojN36uo8zKn0AussEd2vYMb5cc9NWf00cYMmzEh7";

                var refundOptions = new RefundCreateOptions
                {
                    Charge = processDepositePayment.StripePaymentChargeId,
                    Amount = UitlityConverter.ConvertToCents(processDepositePayment.DepositeAmount),
                };

                var refundService = new RefundService();

                var refund = await refundService.CreateAsync(refundOptions);

                if (refund != null)
                {
                    processDepositePayment.StripeDepositeChargeId = refund.Id;
                    processDepositePayment.StripeDepositeIntentId = refund.PaymentIntentId;
                }


                var response = await _paymentOrderService.HandleDepositPaymentAsync(processDepositePayment);

                if (response != null)
                {
                    var paymentOrders = await _paymentOrderService.GetExecutivePaymentOrdersAsync();
                    return Json(new { data = paymentOrders });
                }

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateRefund(string chargeId, long amountInCents)
        {
            // Initialize Stripe with secret key
            StripeConfiguration.ApiKey = "sk_test_51SJ2Nu32ZfCJ3T7ZvHRGxGZ1vVpZQzYTNWejuB9sQrwyJ9J0srziK7R40YojN36uo8zKn0AussEd2vYMb5cc9NWf00cYMmzEh7";

            var refundOptions = new RefundCreateOptions
            {
                Charge = chargeId,
                Amount = amountInCents
            };
            var refundService = new RefundService();
            var refund = await refundService.CreateAsync(refundOptions);
            return Json(new { RefundId = refund.Id, Status = refund.Status });
        }
    }
}
