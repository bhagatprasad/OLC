using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class UserLoginHistoryController : Controller
    {
        private readonly IUserLoginHistoryService _userLoginHistoryService;
        private readonly INotyfService _notyfService;

        public UserLoginHistoryController(IUserLoginHistoryService userLoginHistoryService, INotyfService notyfService)
        {
            _userLoginHistoryService = userLoginHistoryService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUserLoginHistory()
        {
            try
            {
                var response = await _userLoginHistoryService.GetAllUserLoginHistoryAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
            [HttpGet]
            public async Task<IActionResult> GetAllUserActivityToday()
            {
                try
                {
                    var response = await _userLoginHistoryService.GetAllUserActivityTodayAsync();
                    return Json(new { data = response });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        
        [HttpGet]
        public async Task<IActionResult> GetUserLoginActivityByUserId(long userId)
        {
            try
            {
                var response = await _userLoginHistoryService.GetUserLoginActivityByUserIdAsync(userId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    } 
}
