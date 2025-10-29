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
        [Route("GetAccountTypeByIdAsync/{accountTypeId}")]
        public async Task<IActionResult> GetAccountTypeByIdAsyn(long accountTypeId)
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
        public async Task<IActionResult> GetAccountTypeAsyn()
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
        [Route("InsertAccountTypeAsync")]
        public async Task<IActionResult> InsertAccountTypeAsync(AccountType accountType)
        {
            try
            {
                var response = await _accountTypeManager.InsertAccountTypeAsync(accountType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateAccountTypeAsync")]
        public async Task<IActionResult> UpdateAccountTypeAsync(AccountType accountType)
        {
            try
            {
                var response = await _accountTypeManager.UpdateAccountTypeAsync(accountType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteAccountTypeAsync/{accountTypeId}")]
        public async Task<IActionResult> DeleteAccountTypeAsync(long accountTypeId)
        {
            try
            {
                var response = await _accountTypeManager.DeleteAccoutntTypeAsync(accountTypeId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
