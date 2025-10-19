using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class BankAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserBankAccounts()
        {
            return View();
        }
    }
}
