using OLC.Web.API.Models;
using OLC.Web.API.Manager;
using Microsoft.AspNetCore.Mvc; 
namespace OLC.Web.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserWalletDetailsController: ControllerBase
    {
        private readonly IUserWalletDetailsManager _userWalletDetailsManager;
        public UserWalletDetailsController(IUserWalletDetailsManager userWalletDetailsManager)
        {
            _userWalletDetailsManager = userWalletDetailsManager;
        }


        [HttpGet]
        [Route("GetUserWalletDetailsByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserWalletDetailsByUserIdAsync(long userId)
        {
            try
            {

                var result = await _userWalletDetailsManager.uspGetUserWalletDetailsByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
