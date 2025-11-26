using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Executive"))]
    public class ExecutiveBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
