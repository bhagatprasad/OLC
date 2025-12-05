using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;


namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWalletController : ControllerBase
    {
        private readonly IUserWalletManager _userWalletManager;
        public UserWalletController(IUserWalletManager userWalletManager)
        {
            _userWalletManager = userWalletManager;
        }

        [HttpGet]
        [Route("GetUserWalletByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserWalletByUserIdAsync(long userId)
        {
            try
            {
                var response = await _userWalletManager.GetUserWalletByUserIdAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetAllUserWalletsAsync")]
        public async Task<IActionResult> GetAllUserWalletsAsync()
        {
            try
            {
                var response = await _userWalletManager.GetAllUserWalletsAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("SaveUserWalletAsync")]
        public async Task<IActionResult> SaveUserWalletAsync(UserWallet userWallet)
        {
            try
            {
                var response = await _userWalletManager.SaveUserWalletAsync(userWallet);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateUserWalletBalanceAsync")]
        public async Task<IActionResult> UpdateUserWalletBalanceAsync(UserWallet userWallet)
        {
            try
            {
                var response = await _userWalletManager.UpdateUserWalletBalanceAsync(userWallet);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InserUserWalletLogAsync")]
        public async Task<IActionResult> InserUserWalletLogAsync(UserWalletLog userWalletLog)
        {
            try
            {
                var response = await _userWalletManager.InsertUserWalletLogAsyn(userWalletLog);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllUsersWalletlogAsync")]
        public async Task<IActionResult> GetAllUsersWalletlogAsync()
        {
            try
            {
                var response = await _userWalletManager.GetAllUsersWalletlogAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserWalletlogByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserWalletlogByUserIdAsync(long userId)
        {
            try
            {
                var response = await _userWalletManager.GetAllUserWalletlogByUserIdAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
            [HttpGet]
            [Route("GetUserWalletDetailsByUserIdAsync/{userId}")]

            public async Task<IActionResult> GetUserWalletDetailsByUserIdAsync(long userId)
            {
                try
                {

                    var result = await _userWalletManager.uspGetUserWalletDetailsByUserIdAsync(userId);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
          
    }
}
