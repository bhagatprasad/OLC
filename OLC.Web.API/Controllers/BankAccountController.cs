using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager; 
using OLC.Web.API.Models;  
namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IUserBankAccountManager _userBankAccountManager;

        public BankAccountController(IUserBankAccountManager userBankAccountManager)
        {
            _userBankAccountManager = userBankAccountManager;
        }

        [HttpGet]
        [Route("GetUserBankAccountsByUserId/{userId}")]
        public async Task<IActionResult> GetUserBankAccountsByUserIdAsync(long userId)
        {
            try
            {
                var response = await _userBankAccountManager.GetAllUserBankAccountByUserIdAsync(userId);
                return Ok(response);  
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving bank accounts.");
            }
        }

        [HttpGet]
        [Route("GetUserBankAccountById/{id}")]
        public async Task<IActionResult> GetUserBankAccountByIdAsync(long id)
        {
            try
            {
                var response = await _userBankAccountManager.GetUserBankAccountByIdAsync(id);
                if (response != null)
                {
                    return Ok(response);  
                }
                return NotFound();  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the bank account.");
            }
        }

        [HttpGet]
        [Route("GetAllUserBankAccounts")]
        public async Task<IActionResult> GetAllUserBankAccountsAsync()
        {
            try
            {
                var response = await _userBankAccountManager.GetAllUserBankAccountsAsync();
                return Ok(response);  // Returns 200 OK with all bank accounts
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving all bank accounts.");
            }
        }

        [HttpPost]
        [Route("InsertUserBankAccount")]
        public async Task<IActionResult> InsertUserBankAccountAsync(UserBankAccount userBankAccount)
        {
            try
            {
                var response = await _userBankAccountManager.InsertUserBankAccountAsync(userBankAccount);
                return Ok(response); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while inserting the bank account.");
            }
        }

        [HttpPost]
        [Route("UpdateUserBankAccount")]
        public async Task<IActionResult> UpdateUserBankAccountAsync(UserBankAccount userBankAccount)
        {
            try
            {
                var response = await _userBankAccountManager.UpdateUserBankAccountAsync(userBankAccount);
                return Ok(response);  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the bank account.");
            }
        }

        [HttpDelete]
        [Route("DeleteUserBankAccount/{id}")]
        public async Task<IActionResult> DeleteUserBankAccountAsync(long id)
        {
            try
            {
                var response = await _userBankAccountManager.DeleteUserBankAccountAsync(id);
                return Ok(response);  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the bank account.");
            }
        }
    }
}
