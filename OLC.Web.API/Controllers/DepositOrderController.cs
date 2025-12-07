using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositOrderController : ControllerBase
    {
        private readonly IDepositOrderManager _depositOrderManager;
        public DepositOrderController(IDepositOrderManager depositOrderManager)
        {
            _depositOrderManager = depositOrderManager;
        }

        [HttpPost]
        [Route("InsertDepositOrderAsync")]
        public async Task<IActionResult> InsertDepositOrderAsync([FromBody] DepositOrder depositOrder)
        {
            try
            {
                var response = await _depositOrderManager.InsertDepositOrderAsync(depositOrder);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetDepositOrderByOrderIdAsync/{paymentOrderId}")]
        public async Task<IActionResult> GetDepositOrderByOrderIdAsync(long paymentOrderId)
        {
            try
            {
                var response = await _depositOrderManager.GetDepositOrderByOrderIdAsync(paymentOrderId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllDepositOrdersAsync")]
        public async Task<IActionResult> GetAllDepositOrdersAsync()
        {
            try
            {
                var response = await _depositOrderManager.GetAllDepositOrdersAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetDepositOrderByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetDepositOrderByUserIdAsync(long userId)
        {
            try
            {
                var response = await _depositOrderManager.GetDepositOrderByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("uspGetAllExecutiveDepositOrderDetailsAsync")]
        public async Task<IActionResult> uspGetAllExecutiveDepositOrderDetailsAsync()
        {
            try
            {
                var response = await _depositOrderManager.uspGetAllExecutiveDepositOrderDetailsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
