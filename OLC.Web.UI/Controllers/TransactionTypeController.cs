using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class TransactionTypeController : Controller
    {
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly INotyfService _notyfService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService,
            INotyfService notyfService)
        {
            _transactionTypeService = transactionTypeService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetTransactionTypes()
        {
            try
            {
                var response = await _transactionTypeService.GetTransactionTypeAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> InsertOrUpdateTransactionType([FromBody] TransactionType transactionType)
        {
            try
            {
                bool isSucess = false;

                if (transactionType.Id > 0)
                    isSucess = await _transactionTypeService.UpdateTransactionTypeAsync(transactionType);
                else
                    isSucess = await _transactionTypeService.InsertTransactionTypeAsync(transactionType);

                _notyfService.Success("Save operation successful");

                return Json(true);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
