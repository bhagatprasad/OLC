using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class ExecutiveBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
