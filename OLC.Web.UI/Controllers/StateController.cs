using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class StateController : Controller
    {
        private readonly IStateService _stateService;
        private readonly INotyfService _notyfService;
        public StateController(IStateService stateService, INotyfService notyfService)
        {
            _stateService = stateService;
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetStateByState(long stateId)
        {
            try
            {
                var response = await _stateService.GetStateByStateAsync(stateId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive)"))]
        public async Task<IActionResult> GetStatesByCountry(long countryId)
        {
            try
            {
                var response = await _stateService.GetStatesByCountryAsync(countryId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStatesList()
        {
            try
            {
                var response = await _stateService.GetStatesListAsync();
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
