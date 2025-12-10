using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutivesController : ControllerBase
    {
        private readonly IExecutivesManager _executivesManager;

        public ExecutivesController(IExecutivesManager executivesManager)
        {
            _executivesManager = executivesManager;
        }

        [HttpGet]
        [Route("GetAllExecutivesAsync")]
        public async Task<IActionResult> GetAllExecutivesAsync()
        {
            try
            {
                var response = await _executivesManager.GetAllExecutivesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetExecutiveByUserId/{userId}")]
        public async Task<IActionResult> GetExecutiveByUserId(long userId)
        {
            try
            {
                var response = await _executivesManager.GetExecutiveByUserId(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertExecutuveAsyncs")]
        public async Task<IActionResult> InsertExecutuveAsyncs(Executives executives)
        {
            try
            {
                var response = await _executivesManager.InsertExecutuveAsyncs(executives);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
