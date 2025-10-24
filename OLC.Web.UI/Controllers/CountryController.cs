using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;
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
    }
}
