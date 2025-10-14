using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("User"))]
    public class UserBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
