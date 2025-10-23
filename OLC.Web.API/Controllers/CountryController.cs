using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController:ControllerBase
    {
        private readonly ICountryManager _countryManager;
        public CountryController(ICountryManager countryManager)
        {
            _countryManager = countryManager;
        }

        [HttpGet]
        [Route("GetCountryByCountryIdAsync/{countryId}")]
        public async Task<IActionResult> GetCountryByCountryIdAsync(long countryId)
        {
            try
            {
                var response = await _countryManager.GetCountryByCountryIdAsync(countryId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetAllCountriesAsync")]
        public async Task<IActionResult> GetAccountTypeAsyn()
        {
            try
            {
                var response = await _countryManager.GetAllCountriesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("InsertCountryAsync")]
        public async Task<IActionResult> InsertCountryAsync(Country country)
        {
            try
            {
                var response = await _countryManager.InsertCountryAsync(country);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateCountryAsync")]
        public async Task<IActionResult> UpdateCountryAsync(Country country)
        {
            try
            {
                var response = await _countryManager.UpdateCountryAsync(country);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("DeleteCounryAsync/{countryId}")]
        public async Task<IActionResult> DeleteCountryAsync(long countryId)
        {
            try
            {
                var response = await _countryManager.DeleteCountryAsync(countryId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
