using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly INotyfService _notyfService;
        public AccountTypeController(IAccountTypeService accountTypeService,
            INotyfService notyfService)
        {
            _accountTypeService = accountTypeService;
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
        public async Task<IActionResult> GetAccountTypes()
        {
            try
            {
                var response = await _accountTypeService.GetAccountTypeAsync();
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
        public async Task<IActionResult> InsertOrUpdateAccount([FromBody] AccountType accountType)
        {
            try
            {
                bool isSucess = false;

                if (accountType.Id > 0)
                    isSucess = await _accountTypeService.InsertAccountTypeAsync(accountType);
                else
                    isSucess = await _accountTypeService.UpdateAccountTypeAsync(accountType);

                _notyfService.Success("Save operation sucessfull");

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
