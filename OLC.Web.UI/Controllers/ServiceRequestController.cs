using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class ServiceRequestController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
