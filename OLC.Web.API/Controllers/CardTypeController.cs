using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CardTypeController : ControllerBase
    {
        private readonly ICardTypeManager _cardTypeManager;
        public CardTypeController(ICardTypeManager cardTypeManager)
        {
            _cardTypeManager = cardTypeManager;
        }

        [HttpGet]
        [Route("GetUserCardTypeByIdAsync/{Id}")]
        public async Task<IActionResult> GetUserCardTypeByIdAsync(long  Id)
        {
            try
            {
                var response = await _cardTypeManager.GetUserCardTypeByIdAsync(Id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserCardTypeAsync")]
        public async Task<IActionResult> GetUserCardTypeAsync()
        {
            try
            {
                var response = await _cardTypeManager.GetUserCardTypeAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertUserCardTypeAsync")]
        public async Task<IActionResult> InsertUserCardTypeAsync(CardType cardType)
        {
            try
            {
                var response = await _cardTypeManager.InsertUserCardTypeAsync(cardType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateUserCardType")]
        public async Task<IActionResult> UpdateUserCardTypeAsync(CardType cardType)
        {
            try
            {
                var response = await _cardTypeManager.UpdateUserCardTypeAsync(cardType);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("DaleteUserCardTypeAsync/{Id}")]
        public async Task<IActionResult> DeleteUserCardTypeAsync(long Id)
        {
            try
            {
                var response = await _cardTypeManager.DeleteUserCardTypeAsync(Id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

