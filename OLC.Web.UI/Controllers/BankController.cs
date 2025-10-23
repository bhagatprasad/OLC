using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class BankController : Controller
    {
        private readonly IBankService _bankService;
        private readonly INotyfService _notyfService;

        public BankController(IBankService bankService,
            INotyfService notyfService)
        {
            _bankService = bankService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetBanks()
        {
            try
            {
                var response = await _bankService.GetBanksListAsync();
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
        public async Task<IActionResult> InsertOrUpdateBank([FromBody] Bank bank)
        {
            try
            {
                bool isSucess = false;

                if (bank.Id > 0)
                    isSucess = await _bankService.UpdateBankAsync(bank);
                else
                    isSucess = await _bankService.InsertBankAsync(bank);

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
