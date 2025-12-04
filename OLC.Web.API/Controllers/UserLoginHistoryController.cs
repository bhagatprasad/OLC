using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginHistoryController: ControllerBase
    {
        private readonly  IUserLoginHistoryManager _userLoginHistoryManager;

        public UserLoginHistoryController(IUserLoginHistoryManager userLoginHistoryManager)
        {
            _userLoginHistoryManager = userLoginHistoryManager;
        }
        [HttpGet]
        [Route("GetAllUserLoginHistoryAsync")]
        public async Task<IActionResult> GetAllUserLoginHistory()
        {
            try
            {
                var responce = await _userLoginHistoryManager.GetAllUserLoginHistoryAsync();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetAllUserActivityTodayAsync")]
        public async Task<IActionResult> GetAllUserActivityToday()
        {
            try
            {
                var responce = await _userLoginHistoryManager.GetAllUserActivityTodayAsync();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetUserLoginActivityByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserLoginActivityByUserIdAsync(long userId)
        {
            try
            {
                var responce = await _userLoginHistoryManager.GetUserLoginActivityByUserIdAsync(userId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
