using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OLC.Web.UI.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _countryService.GetAllCountriesAsync();
            return View(response);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCounry(long countryid)
        {
            try
            {
                var response = await _countryService.DeleteCountryAsync(countryid);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCountriesList()
        {
            try
            {
                var response = await _countryService.GetAllCountriesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCountryByCountryId(long countryid)
        {
            try
            {
                var response = await _countryService.GetCountryByCountryIdAsync(countryid);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertCountry (Country country)
        {
            try
            {
                var response = await _countryService.InsertCountryAsync(country);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCountry(Country country)
        {
            try
            {
                var response = await _countryService.InsertCountryAsync(country);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
