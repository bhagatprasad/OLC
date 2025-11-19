using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityManager _priorityManager;

        public PriorityController(IPriorityManager PriorityManager)
        {
            _priorityManager = PriorityManager;
        }

        [HttpGet]
        [Route("GetPriorityByIdAsync/{priorityId}")]
        public async Task<IActionResult> GetPriorityByIdAsync(long priorityId)
        {
            try
            {
                var response = await _priorityManager.GetPriorityByIdAsync(priorityId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetPriorityAsync")]
        public async Task<IActionResult> GetPriorityAsync()
        {
            try
            {
                var response = await _priorityManager.GetPriorityAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("InsertPriorityAsync")]
        public async Task<IActionResult> InsertPriorityAsync(Priority priority)
        {
            try
            {
                var response = await _priorityManager.InsertPriorityAsync(priority);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdatePriorityAsync")]
        public async Task<IActionResult> UpdatePriorityAsync(Priority priority)
        {
            try
            {
                var response = await _priorityManager.UpdatePriorityAsync(priority);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeletePriorityAsync/{priorityId}")]
        public async Task<IActionResult> DeletePriorityAsync(long priorityId)
        {
            try
            {
                var response = await _priorityManager.DeletePriorityAsync(priorityId);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
