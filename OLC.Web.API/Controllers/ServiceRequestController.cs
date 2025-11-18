using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController:ControllerBase
    {
        private readonly IServiceRequestManager _serviceRequestManager;
        public ServiceRequestController(IServiceRequestManager serviceRequestManager)
        {
            _serviceRequestManager = serviceRequestManager;
        }

        [HttpGet]
        [Route("GetServiceRequestByIdAsync/{ticketId}")]
        public async Task<IActionResult> GetServiceRequestByIdAsync(long ticketId)
        {
            try
            {
                var response = await _serviceRequestManager.GetServiceRequestByIdAsync(ticketId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllServiceRequestsAsync")]
        public async Task<IActionResult> GetAllServiceRequestsAsync()
        {
            try
            {
                var response = await _serviceRequestManager.GetAllServiceRequestsAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertServiceRequestAsync")]
        public async Task<IActionResult> InsertServiceRequestAsync(ServiceRequest serviceRequest)
        {
            try
            {
                var response = await _serviceRequestManager.InsertServiceRequestAsync(serviceRequest);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateServiceRequestAsync")]
        public async Task<IActionResult> UpdateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            try
            {
                var response = await _serviceRequestManager.UpdateServiceRequestAsync(serviceRequest);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteServiceRequestAsync/{ticketId}")]
        public async Task<IActionResult> DeleteServiceRequestAsync(long ticketId)
        {
            try
            {
                var response = await _serviceRequestManager.DeleteServiceRequestAsync(ticketId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("InsertServiceRequestRepliesAsync")]
        public async Task<IActionResult> InsertServiceRequestReplies(ServiceRequestReplies serviceRequestReplies)
        {
            try
            {
                var response = await _serviceRequestManager.InsertServiceRequestRepliesAsync(serviceRequestReplies);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetServiceRequestRepliesByTicketIdAsync/{ticketId}")]
        public async Task<IActionResult> GetServiceRequestRepliesByTicketId(long ticketId)
        {
            try
            {
                var response = await _serviceRequestManager.GetServiceRequestRepliesByTicketIdAsync(ticketId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetServiceRequestByUserAsync/{userId}")]
        public async Task<IActionResult> GetServiceRequestByUserAsync(long userId)
        {
            try
            {
                var response = await _serviceRequestManager.GetServiceRequestByUserIdAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllServiceRequestsWithRepliesAsync")]
        public async Task<IActionResult> GetAllServiceRequestsWithRepliesAsync()
        {
            try
            {
                var response = await _serviceRequestManager.GetAllServiceRequestsWithRepliesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllServiceRequestRepliesAsync")]
        public async Task<IActionResult> GetAllServiceRequestRepliesAsync()
        {
            try
            {
                var response = await _serviceRequestManager.GetAllServiceRequestRepliesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("CancelServiceRequestByTicketIdAsync")]
        public async Task<IActionResult> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest)
        {
            try
            {
                var response = await _serviceRequestManager.CancelServiceRequestByTicketIdAsync(serviceRequest);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AssingingServiceRequestAsync")]
        public async Task<IActionResult> AssingingServiceRequestAsync(ServiceRequest serviceRequest)
        {
            try
            {
                var response = await _serviceRequestManager.AssingingServiceRequestAsync(serviceRequest);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    } 
}
