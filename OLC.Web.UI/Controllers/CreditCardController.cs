using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly INotyfService _notyfService;
        public CreditCardController(ICreditCardService creditCardService,
            INotyfService notyfService)
        {
            _creditCardService = creditCardService;
            _notyfService= notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserCreditCards()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUserCreditCards(long userId)
        {
            try
            {
                var cards = await _creditCardService.GetUserCreditCardsAsync(userId);
                return Json(new { data = cards });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

    }
}
