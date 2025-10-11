using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
