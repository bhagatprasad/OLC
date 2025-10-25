using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;
using System.Runtime.InteropServices;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class CityController:Controller
    {
        private readonly ICityService _cityService;
        private readonly INotyfService _notyfService;

        public CityController(ICityService cityService , INotyfService notyfService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Authorize(Roles =("Administrator,Executive"))]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles =("Administrator,Executive"))]
        public async Task<IActionResult> GetCitiesByCountry(long countryId)
        {
            try
            {
                var response = await _cityService.GetCitiesByCountryAsync(countryId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize(Roles =("Administrator,Executive"))]
        public async Task<IActionResult> GetCitiesByState(long stateId)
        {
            try
            {
                var response = await _cityService.GetCitiesByStateAsync(stateId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Authorize(Roles =("Administrator,Executive"))]
        public async Task<IActionResult> GetCityById(long cityId)
        {
            try
            {
                var response = await _cityService.GetCityByIdAsync(cityId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> GetCitiesList()
        {
            try
            {
                var response = await _cityService.GetCitiesListAsync();
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }                  
    }
}
