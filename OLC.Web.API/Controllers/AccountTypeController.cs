using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;


namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeManager _accountTypeManager;
        public AccountTypeController(IAccountTypeManager accountTypeManager)
        {
            _accountTypeManager = accountTypeManager;
        }

        [HttpGet]
        [Route("GetUserAccountTypeByIdAsync/{accountTypeId}")]
        public async Task<IActionResult> GetUserAccountTypeByIdAsyn(long accountTypeId)
        {
            try
            {
                var response = await _accountTypeManager.GetAccountTypeByIdAsync(accountTypeId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAccountTypeAsync")]
        public async Task<IActionResult> GetUserAccountTypeAsyn()
        {
            try
            {
                var response = await _accountTypeManager.GetAccountTypeAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertUserAccountTypeAsync")]
        public async Task<IActionResult> InsertAccountTypeAsync(AccountType accountType)
        {
            try
            {
                var response = await _accountTypeManager.InsertUserAccountTypeAsync(accountType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateUserAccountTypeAsync")]
        public async Task<IActionResult> UpdateUserAccountTypeAsync(AccountType accountType)
        {
            try
            {
                var response = await _accountTypeManager.UpdateUserAccountTypeAsync(accountType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteUserAccoutntTypeAsync/{accountTypeId}")]
        public async Task<IActionResult> DeleteUserAccoutntTypeAsync(long accountTypeId)
        {
            try
            {
                var response = await _accountTypeManager.DeleteUserAccoutntTypeAsync(accountTypeId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
