using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class CreditCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserCreditCards()
        {
            return View();
        }


    }
}
