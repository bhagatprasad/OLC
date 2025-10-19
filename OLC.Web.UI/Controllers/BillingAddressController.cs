using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class BillingAddressController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserBillingAddress()
        {
            return View();
        }
    }
}
