using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
namespace OLC.Web.UI.Controllers
{
    public class UserWalletController : Controller
    {
        private readonly IUserWalletService _userWalletService;
        private readonly INotyfService _notyfService;
        public UserWalletController(IUserWalletService userWalletService, INotyfService notyfService)
        {
            _userWalletService = userWalletService;
            _notyfService = notyfService;
        }

        [Authorize(Roles = ("Administrator"))]
        public IActionResult UserWallet()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUserWalletsAsync()
        {
            try
            {
                var wallet = await _userWalletService.GetAllUserWalletsAsync();
                return Json(new { data = wallet });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUsersWalletlogAsync()
        {
            try
            {
                var walletLog = await _userWalletService.GetAllUsersWalletlogAsync();
                return Json(new { data = walletLog });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUserWalletlogByUserIdAsync(long userId)
        {
            try
            {
                var walletLog = await _userWalletService.GetAllUserWalletlogByUserIdAsync(userId);
                return Json(new { data = walletLog });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetUserWalletByUserIdAsync(long userId)
        {
            try
            {
                var wallet = await _userWalletService.GetUserWalletByUserIdAsync(userId);
                return Json(new { data = wallet });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }        

    }
}
