using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateManager _stateManager;
        public StateController(IStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        [HttpGet]
        [Route("GetStateByStateAsync/{stateId}")]
        public async Task<IActionResult> GetStateByStateAsync(long stateId)
        {
            try
            {
                var response = await _stateManager.GetStateByStateAsync(stateId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetStatesByCountryAsync/{countryId}")]
        public async Task<IActionResult> GetStatesByCountryAsync(long countryId)
        {
            try
            {
                var response = await _stateManager.GetStatesByCountryAsync(countryId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("GetStatesListAsync")]
        public async Task<IActionResult> GetStatesListAsync()
        {
            try
            {
                var response = await _stateManager.GetStatesListAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
