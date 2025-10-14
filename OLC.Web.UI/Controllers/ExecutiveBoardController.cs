using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Administrator"))]
    public class ExecutiveBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
