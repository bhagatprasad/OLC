using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        [Route("RegisterUserAsync")]
        public async Task<IActionResult> RegisterUserAsync(UserRegistration userRegistration)
        {
            try
            {
                if (string.IsNullOrEmpty(userRegistration.Password))
                    return BadRequest("Password feild required");

                var response = await _accountManager.RegisterUserAsync(userRegistration);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AuthenticateUserAsync")]
        public async Task<IActionResult> AuthenticateUserAsync(UserAuthentication userAuthentication)
        {
            try
            {
                var response = await _accountManager.AuthenticateUserAsync(userAuthentication);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("GenarateUserClaimsAsync")]
        public async Task<IActionResult> GenarateUserClaimsAsync(AuthResponse authResponse)
        {
            try
            {
                var response = await _accountManager.GenarateUserClaimsAsync(authResponse);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
