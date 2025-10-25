using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingAddressController : ControllerBase
    {
        private readonly IBillingAddressManager _billingAddressManager;

        public BillingAddressController(IBillingAddressManager billingAddressManager)
        {
            _billingAddressManager = billingAddressManager;
        }

        [HttpGet]
        [Route("GetUserBillingAddressesAsync/{userId}")]
        public async Task<IActionResult> GetUserBillingAddressesAsync(long userId)
        {
            try
            {
                var response = await _billingAddressManager.GetUserBillingAddressesAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserBillingAddressByIdAsync/{billingAddressId}")]
        public async Task<IActionResult> GetUserBillingAddressByIdAsync(long billingAddressId)
        {
            try
            {
                var response = await _billingAddressManager.GetUserBillingAddressByIdAsync(billingAddressId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("SaveUserBillingAddressAsync")]
        public async Task<IActionResult> SaveUserBillingAddressAsync(UserBillingAddress userBillingAddress)
        {
            try
            {
                var response = await _billingAddressManager.InsertUserBillingAddressAsync(userBillingAddress);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateUserBillingAddressAsync")]
        public async Task<IActionResult> UpdateUserBillingAddressAsync(UserBillingAddress userBillingAddress)
        {
            try
            {
                var response = await _billingAddressManager.UpdateUserBillingAddressAsync(userBillingAddress);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteUserBillingAddressAsync/{billingAddressId}")]
        public async Task<IActionResult> DeleteUserBillingAddressAsync(long billingAddressId)
        {
            try
            {
                var response = await _billingAddressManager.DeleteUserBillingAddressAsync(billingAddressId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
