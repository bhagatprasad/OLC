using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class UserWalletDetailsController: Controller
    {
        private readonly IUserWalletDetailsService _userWalletDetailsService;
        private readonly INotyfService _notyfService;
        public UserWalletDetailsController(IUserWalletDetailsService userWalletDetailsService,INotyfService notyfService)
        {
            _userWalletDetailsService = userWalletDetailsService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetUserWalletDetailsByUserId(long userId)
        {
            try
            {
                var response = await _userWalletDetailsService.GetUserWalletDetailsByUserIdAsync(userId);
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
