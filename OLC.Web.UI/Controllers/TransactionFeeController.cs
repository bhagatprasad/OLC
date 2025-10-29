using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    public class TransactionFeeController : Controller
    {
        private readonly ITransactionFeeService _transactionFeeService;
        private readonly INotyfService _notyfService;
        public TransactionFeeController(ITransactionFeeService transactionFeeService,
            INotyfService notyfService)
        {
            _transactionFeeService = transactionFeeService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionFeesList()
        {
            try
            {
                var response = await _transactionFeeService.GetTransactionFeesListAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
