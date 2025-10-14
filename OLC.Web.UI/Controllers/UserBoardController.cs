using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class UserBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
