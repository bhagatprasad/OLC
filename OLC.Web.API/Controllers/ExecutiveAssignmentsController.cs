
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
        public async Task<IActionResult> InsertExecutiveAssignments(ExecutiveAssignments executiveAssignments)
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
        public async Task<IActionResult> UpdateExecutiveAssignments(ExecutiveAssignments executiveAssignments)
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

        [HttpPost]
        [Route("AssignPaymentOrdersIntoExecutiveQueueAsync")]
        public async Task<IActionResult> AssignPaymentOrdersIntoExecutiveQueueAsync(PushPaymentOrderIntoQue pushPaymentOrderIntoQue)
        {
            try
            {
                var response = await _executiveAssignmentsmanager.AssignPaymentOrdersIntoExecutiveQueueAsync(pushPaymentOrderIntoQue);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteExecutiveAssignmentsAsync/{id}")]
        public async Task<IActionResult> DeleteExecutiveAssignments(long id)
        {
            try
            {
                var response = await _executiveAssignmentsmanager.DeleteExecutiveAssignmentsAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}