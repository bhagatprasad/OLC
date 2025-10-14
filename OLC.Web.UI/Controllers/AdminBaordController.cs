using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class AdminBaordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
