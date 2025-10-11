using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class ContactusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
