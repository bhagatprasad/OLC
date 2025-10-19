using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.UI.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MakeNewPayment()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserTransactions()
        {
            return View();
        }
    }
}
