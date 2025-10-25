using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly INotyfService _notyfService;
        public CountryController(ICountryService countryService,
            INotyfService notyfService)
        {
            _countryService = countryService;
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Roles = ("Administrator"))]
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
                return Json(new { data = response });

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
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> SaveCountry([FromBody] Country country)
        {
            try
            {
                bool isSaved = false;

                if (country != null)
                {
                    if (country.Id > 0)
                        isSaved = await _countryService.UpdateCountryAsync(country);
                    else
                        isSaved = await _countryService.InsertCountryAsync(country);

                    _notyfService.Success("Country save successful");

                    return Json(isSaved);
                }

                _notyfService.Warning("Country save unsuccessful");

                return Json(isSaved);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
