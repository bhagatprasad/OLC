using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusManager _statusManager;
        public StatusController(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        [HttpGet]
        [Route("GetStatusesAsync")]
        public async Task<IActionResult> GetStatusesAsync()
        {
            try
            {
                var response = await _statusManager.GetStatusesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetStatusByIdAsync/{statusId}")]
        public async Task<IActionResult> GetStatusByIdAsync(long statusId)
        {
            try
            {
                var response = await _statusManager.GetStatusByIdAsync(statusId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("SaveStatusAsync")]
        public async Task<IActionResult> SaveStatusAsync(Status status)
        {
            try
            {
                var response = await _statusManager.SaveStatusAsync(status);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
