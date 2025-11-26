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

        [HttpGet("/TransactionType")]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult TransactionTypes()
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveTransactionType([FromBody] TransactionType transactionType)
        {
            try
            {
                bool isSaved = false;

                if (transactionType != null)
                {
                    if (transactionType.Id > 0)
                        isSaved = await _transactionTypeService.UpdateTransactionTypeAsync(transactionType);
                    else
                        isSaved = await _transactionTypeService.InsertTransactionTypeAsync(transactionType);

                    _notyfService.Success("Successfully saved Transaction type");
                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save transaction type");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTransactionType(long transactionTypeId)

        {
            try
            {
                bool isSaved = false;
                if (transactionTypeId > 0)
                {
                    isSaved = await _transactionTypeService.DeleteTransactionTypeAsync(transactionTypeId);

                    if (isSaved)
                        _notyfService.Success("Successfully deleted Transaction type");
                    else
                        _notyfService.Warning("Unable to delete Transaction type");

                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete Transaction type");
                return Json(isSaved);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ActivateTransactionType([FromBody] TransactionType transactionType)
        {
            try
            {
                bool isActivate = false;

                isActivate = await _transactionTypeService.ActivateTransactionTypeAsync(transactionType);

                if (isActivate)
                    _notyfService.Success("Successfully activated Transaction type");
                else
                    _notyfService.Error("Unable to activate Transaction type");

                return Json(isActivate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
