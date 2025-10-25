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
        public async Task<IActionResult> UpdateUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            try
            {
                var response = await _creditCardManager.UpdateUserCreditCardAsync(userCreditCard);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("ActivateUserCreditCardAsync")]
        public async Task<IActionResult> ActivateUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            try
            {
                var response = await _creditCardManager.ActivateUserCreditCardAsync(userCreditCard);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("DeleteUserCreditAsync/{creditcardId}")]
        public async Task<IActionResult> DeleteUserCreditAsync(long creditcardId)
        {
            try
            {
                var response = await _creditCardManager.DeleteUserCreditAsync(creditcardId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
    }
}