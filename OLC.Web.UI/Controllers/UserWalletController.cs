using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;
namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class UserWalletController : Controller
    {
        private readonly IUserWalletService _userWalletService;
        private readonly INotyfService _notyfService;
        public UserWalletController(IUserWalletService userWalletService, INotyfService notyfService)
        {
            _userWalletService = userWalletService;
            _notyfService = notyfService;
        }

        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUserWallets()
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
        public async Task<IActionResult> GetAllUsersWalletlog()
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
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetAllUserWalletlogByUser(long userId)
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
        public async Task<IActionResult> GetUserWallet(long userId)
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


        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAllExecutiveUserWalletDetails()
        {
            try
            {
                var response = await _userWalletService.GetAllExecutiveUserWalletDetailsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }

        }
    }
}