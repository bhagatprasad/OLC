using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
