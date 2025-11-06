using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOrderController : ControllerBase
    {
        private readonly IPaymentOrderManager _paymentOrderManager;
        public PaymentOrderController(IPaymentOrderManager paymentOrderManager)
        {
            _paymentOrderManager = paymentOrderManager;
        }

        [HttpPost]
        [Route("SavePaymentOrderAsync")]
        public async Task<IActionResult> SavePaymentOrderAsync(PaymentOrder paymentOrder)
        {
            try
            {
                var response = await _paymentOrderManager.InsertPaymentOrderAsync(paymentOrder);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetPaymentOrdersByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetPaymentOrdersByUserIdAsync(long userId)
        {
            try
            {
                var response = await _paymentOrderManager.GetPaymentOrdersByUserIdAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllPaymentOrdersAsync")]
        public async Task<IActionResult> GetAllPaymentOrdersAsync()
        {
            try
            {
                var response = await _paymentOrderManager.GetPaymentOrdersAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        [Route("GetPaymentOrderHistoryAsync/{paymentOrderId}")]
        public async Task<IActionResult> GetPaymentOrderHistoryAsync(long paymentOrderId)
        {
            try
            {
                var response = await _paymentOrderManager.GetPaymentOrderHistoryAsync(paymentOrderId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ProcessPaymentOrderAsync")]
        public async Task<IActionResult> ProcessPaymentOrderAsync(ProcessPaymentOrder processPaymentOrder)
        {
            try
            {
                var response = await _paymentOrderManager.ProcessPaymentOrderAsync(processPaymentOrder);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("ProcessPaymentStatusAsync")]
        public async Task<IActionResult> ProcessPaymentStatusAsync(ProcessPaymentStatus processPaymentStatus)
        {
            try
            {
                var response = await _paymentOrderManager.ProcessPaymentStatusAsync(processPaymentStatus);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserPaymentOrderListAsync/{userId}")]
        public async Task<IActionResult> GetUserPaymentOrderListAsync(long userId)
        {
            try
            {
                var response = await _paymentOrderManager.GetUserPaymentOrderListAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("GetAllUserPaymentOrdersAsync")]
        public async Task<IActionResult> GetAllUserPaymentOrdersAsync()
        {
            try
            {
                var response = await _paymentOrderManager.GetAllUserPaymentOrdersAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetExecutivePaymentOrdersAsync")]
        public async Task<IActionResult> GetExecutivePaymentOrdersAsync()
        {
            try
            {
                var response = await _paymentOrderManager.GetExecutivePaymentOrdersAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetExecutivePaymentOrderDetailsAsync/{paymentOrderId}")]
        public async Task<IActionResult> GetExecutivePaymentOrderDetailsAsync(long paymentOrderId)
        {
            try
            {
                var response = await _paymentOrderManager.GetExecutivePaymentOrderDetailsAsync(paymentOrderId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
