using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class CareerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
