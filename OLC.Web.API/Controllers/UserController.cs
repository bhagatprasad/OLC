using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IUserKycManager _userKycManager;
        public UserController(IUserManager userManager,IUserKycManager userKycManager)
        {
            _userManager = userManager;
            _userKycManager = userKycManager; 
        }
        [HttpGet]
        [Route("GetUserAccountsAsync")]
        public async Task<IActionResult> GetUserAccountsAsync()
        {
            try
            {
                var response = await _userManager.GetUserAccountsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("InserUserKycAsync")]
        public async Task<IActionResult> InserUserKycAsync(UserKyc userKyc)
        {
            try
            {
                var response = await _userKycManager.InsertUserKycAsync(userKyc);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ProcessUserKyAsync")]
        public async Task<IActionResult> ProcessUserKyAsync(UserKyc userKyc)
        {
            try
            {
                var response = await _userKycManager.ProcessUserKycAsync(userKyc);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserKycByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserKycByUserIdAsync(long userId)
        {
            try
            {
                var response = await _userKycManager.GetUserKycByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllUsersKycAsync")]
        public async Task<IActionResult> GetAllUsersKycAsync()
        {
            try
            {
                var response = await _userKycManager.GetAllUsersKycAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
