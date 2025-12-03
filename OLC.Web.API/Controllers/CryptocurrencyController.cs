using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptocurrencyController : ControllerBase
    {
        private readonly ICryptocurrencyManager _cryptocurrencyManager;

        public CryptocurrencyController(ICryptocurrencyManager cryptocurrency)
        {
            _cryptocurrencyManager = cryptocurrency;
        }

        [HttpGet]
        [Route("GetCryptocurrencyByIdAsync/{id}")]
        public async Task<IActionResult> GetCryptocurrencyByIdAsync(long id)
        {
            try
            {
                var response = await _cryptocurrencyManager.GetCryptocurrencyByIdAsync(id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllCryptocurrenciesAsync")]
        public async Task<IActionResult> GetAllCryptocurrenciesAsync()
        {
            try
            {
                var response = await _cryptocurrencyManager.GetAllCryptocurrenciesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InserCryptocurrencyAsync")]
        public async Task<IActionResult> InserCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            try
            {
                var response = await _cryptocurrencyManager.InserCryptocurrencyAsync(cryptocurrency);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateCryptocurrencyAsync")]
        public async Task<IActionResult> UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            try
            {
                var response = await _cryptocurrencyManager.UpdateCryptocurrencyAsync(cryptocurrency);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
