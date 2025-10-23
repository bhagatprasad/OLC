using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityManager _cityManager;
        public CityController(ICityManager cityManager)
        {
            _cityManager = cityManager;
        }

        [HttpGet]
        [Route("GetCitiesListAsync")]
        public async Task<IActionResult> GetCitiesListAsync()
        {
            try
            {
                var responce = await _cityManager.GetCitiesListAsync();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetCitiesByCountryAsync/{countryId}")]
        public async Task<IActionResult> GetCitiesByCountryAsync(long countryId)
        {
            try
            {
                var responce = await _cityManager.GetCitiesByCountryAsync(countryId);
                return Ok(responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetCitiesByStateAsync/{stateId}")]

        public async Task<IActionResult> GetCitiesByStateAsync(long stateId)
        {
            try
            {
                var responce = await _cityManager.GetCitiesByStateAsync(stateId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetCityByIdAsync/{cityId}")]
        public async Task<IActionResult> GetCityByIdAsync(long cityId)
        {
            try
            {
                var responce = await _cityManager.GetCityByIdAsync(cityId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
    

