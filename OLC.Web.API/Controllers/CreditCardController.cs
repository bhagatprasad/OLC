using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardManager _creditCardManager;
        public CreditCardController(ICreditCardManager creditCardManager)
        {
            _creditCardManager = creditCardManager;
        }

        [HttpGet]
        [Route("GetUserCreditCardsAsync/{userId}")]
        public async Task<IActionResult> GetUserCreditCardsAsync(long userId)
        {
            try
            {
                var response = await _creditCardManager.GetUserCreditCardsAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetUserCreditCardByCardIdAsync/{creditCardId}")]
        public async Task<IActionResult> GetUserCreditCardByCardIdAsync(long creditCardId)
        {
            try
            {
                var response = await _creditCardManager.GetUserCreditCardByCardIdAsync(creditCardId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("SaveUserCreditCardAsync")]
        public async Task<IActionResult> SaveUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            try
            {
                var response = await _creditCardManager.InsertUserCreditCardAsync(userCreditCard);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateUserCreditCardAsync")]
        public async Task<IActionResult> UpdateUserCreditCardAsync(UpdateUserCreditCard updateUserCreditCard)
        {
            try
            {
                var response = await _creditCardManager.UpdateUserCreditCardAsync(updateUserCreditCard);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("DeleteUserCreditAsync")]
        public async Task<IActionResult> DeleteUserCreditAsync(DeleteUserCreditCard deleteUserCreditCard)
        {
            try
            {
                var response = await _creditCardManager.DeleteUserCreditAsync(deleteUserCreditCard);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
