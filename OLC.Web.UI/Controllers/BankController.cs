using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class BankController : Controller
    {
        private readonly IBankService _bankService;
        private readonly INotyfService _notyfService;

        public BankController(IBankService bankService, INotyfService notyfService)
        {
            _bankService = bankService;
            _notyfService = notyfService;
        }

        [HttpGet("/Bank")]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult Banks()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetBanks([FromQuery] long Id)
        {
            try
            {
                var response = await _bankService.GetBankAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveBank([FromBody] Bank bank)
        {
            try
            {
                bool isSaved = false;

                if (bank != null)
                {
                    if (bank.Id > 0)
                        isSaved = await _bankService.UpdateBankAsync(bank);
                    else
                        isSaved = await _bankService.InsertBankAsync(bank);

                    _notyfService.Success("Successfully saved bank");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save bank");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBank(long bankId)
        {
            try
            {
                bool isSaved = false;
                if (bankId > 0)
                {
                    isSaved = await _bankService.DeleteBankAsync(bankId);
                    if (isSaved)
                        _notyfService.Success("Successfully deleted bank");
                    else
                        _notyfService.Warning("Unable to delete bank");
                    return Json(isSaved);
                }
                _notyfService.Error("Invalid bank ID");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}