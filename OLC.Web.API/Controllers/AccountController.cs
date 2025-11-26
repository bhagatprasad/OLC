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

        [HttpPost]
        [Route("ForgotPasswordAsync")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            try
            {
                var response = await _accountManager.ForgotPasswordAsync(forgotPassword);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        [Route("ResetPasswordAsync")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPassword resetPassword)
        {
            try
            {
                var response = await _accountManager.ResetPasswordAsync(resetPassword);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        [Route("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            try
            {
                var response = await _accountManager.ChangePasswordAsync(changePassword);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        [HttpPost]
        [Route("LoginOrRegisterExternalUserAsync")]
        public async Task<IActionResult> LoginOrRegisterExternalUserAsync(ExternalUserInfo externalUserInfo)
        {
            try
            {
                var response = await _accountManager.LoginOrRegisterExternalUserAsync(externalUserInfo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
    }
}
