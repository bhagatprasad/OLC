using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class WalletTypeController : Controller
    {
        private readonly IWalletTypeService _walletTypeService;
        private readonly INotyfService _notyfService;
        public WalletTypeController(IWalletTypeService walletTypeService, INotyfService notyfService)
        {
            _walletTypeService = walletTypeService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetWalletTypes()
        {
            try
            {
                var response = await _walletTypeService.GetAllWalletTypes();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> SaveWalletType([FromBody] WalletType walletType)
        {
            try
            {
                bool isSaved = false;

                if (walletType != null)
                {
                    if (walletType.Id > 0)
                        isSaved = await _walletTypeService.UpdateWalletType(walletType);
                    else
                        isSaved = await _walletTypeService.SaveWalletType(walletType);

                    _notyfService.Success("Successfully saved WalletType");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save WalletType");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteWalletType(long id)
        {
            try
            {
                bool isSaved = false;
                if (id > 0)
                {
                    isSaved = await _walletTypeService.DeleteWalletType(id);
                    if (isSaved)
                        _notyfService.Success("Succesfully Deleted WalletType");
                    else
                        _notyfService.Warning("Unable to delete WalletType");
                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete WalletType");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
