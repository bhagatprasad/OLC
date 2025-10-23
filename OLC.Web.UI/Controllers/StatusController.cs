using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
