using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class TermsandConditionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
