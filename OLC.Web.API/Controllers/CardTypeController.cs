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
        [Route("GetCardTypeByIdAsync/{Id}")]
        public async Task<IActionResult> GetCardTypeByIdAsync(long  Id)
        {
            try
            {
                var response = await _cardTypeManager.GetCardTypeByIdAsync(Id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCardTypeAsync")]
        public async Task<IActionResult> GetCardTypeAsync()
        {
            try
            {
                var response = await _cardTypeManager.GetCardTypeAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertCardTypeAsync")]
        public async Task<IActionResult> InsertCardTypeAsync(CardType cardType)
        {
            try
            {
                var response = await _cardTypeManager.InsertCardTypeAsync(cardType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateCardType")]
        public async Task<IActionResult> UpdateCardTypeAsync(CardType cardType)
        {
            try
            {
                var response = await _cardTypeManager.UpdateCardTypeAsync(cardType);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("DaleteCardTypeAsync/{Id}")]
        public async Task<IActionResult> DeleteCardTypeAsync(long Id)
        {
            try
            {
                var response = await _cardTypeManager.DeleteCardTypeAsync(Id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

