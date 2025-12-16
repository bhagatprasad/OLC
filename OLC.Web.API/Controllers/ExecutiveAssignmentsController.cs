
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutiveAssignmentsController : ControllerBase
    {
        private readonly IExecutiveAssignmentsManager _executiveAssignmentsmanager;

        public ExecutiveAssignmentsController(IExecutiveAssignmentsManager executiveAssignmentsmanager)
        {
            _executiveAssignmentsmanager = executiveAssignmentsmanager;
        }

        [HttpPost]
        [Route("InsertExecutiveAssignmentsAsync")]
        public async Task<IActionResult> InsertExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            try
            {
                var response = await _executiveAssignmentsmanager.InsertExecutiveAssignmentsAsync(executiveAssignments);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateExecutiveAssignmentsAsync")]
        public async Task<IActionResult> UpdateExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            try
            {
                var response = await _executiveAssignmentsmanager.UpdateExecutiveAssignmentsAsync(executiveAssignments);
                return  Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}