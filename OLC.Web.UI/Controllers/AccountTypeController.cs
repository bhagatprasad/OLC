using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly INotyfService _notyfService;

        public AccountTypeController(IAccountTypeService accountTypeService, INotyfService notyfService)
        {
            _accountTypeService = accountTypeService;
            _notyfService = notyfService;
        }

        [HttpGet("/AccountType")]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult AccountTypes()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAccountTypes([FromQuery] long Id) 
        {
            try
            {
                var response = await _accountTypeService.GetAccountTypeAsync();
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
        public async Task<IActionResult> SaveAccountType([FromBody] AccountType accountType)
        {
            try
            {
                bool isSaved = false;

                if (accountType != null)
                {
                    if (accountType.Id > 0)
                        isSaved = await _accountTypeService.UpdateAccountTypeAsync(accountType);
                    else
                        isSaved = await _accountTypeService.InsertAccountTypeAsync(accountType);

                    _notyfService.Success("Successfully saved account type");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save account type");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAccountType(long accountTypeId)
        {
            try
            {
                bool isSaved = false;  
                if (accountTypeId > 0)
                {
                    isSaved = await _accountTypeService.DeleteAccountTypeAsync(accountTypeId); 
                    if (isSaved)  
                        _notyfService.Success("Successfully deleted account type"); 
                    else
                        _notyfService.Warning("Unable to delete account type");  
                    return Json(isSaved); 
                }
                _notyfService.Error("Unable to delete account type"); 
                return Json(isSaved);  
            }
            catch (Exception ex)  
            {
                 
                return StatusCode(StatusCodes.Status500InternalServerError);  
            }
        }
    }
}


