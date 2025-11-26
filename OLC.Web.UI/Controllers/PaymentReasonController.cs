using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    public class PaymentReasonController : Controller
    {
        private readonly IPaymentReasonService _paymentReasonService;
        private readonly INotyfService _notyfService;
        public PaymentReasonController(IPaymentReasonService paymentReasonService,
            INotyfService notyfService)
        {
            _paymentReasonService = paymentReasonService;

            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentReasons()
        {
            try
            {
                var response = await _paymentReasonService.GetPaymentReasonsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
