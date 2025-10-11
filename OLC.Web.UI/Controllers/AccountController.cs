using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class AccountController : Controller
    {
       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
