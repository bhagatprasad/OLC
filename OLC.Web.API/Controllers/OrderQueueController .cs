using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderQueueController : ControllerBase
    {
        private readonly IOrderQueueManager _orderQueueManager;

        public OrderQueueController(IOrderQueueManager orderQueueManager)
        {
            _orderQueueManager = orderQueueManager;
        }

        [HttpPost]
        [Route("InsertOrderQueueAsync")]
        public async Task<IActionResult> InsertAccountTypeAsync(OrderQueue orderQueue)
        {
            try
            {
                var response = await _orderQueueManager.InsertOrderQueueAsync(orderQueue);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateOrderQueueAsync")]
        public async Task<IActionResult> UpdateOrderQueueAsync(OrderQueue orderQueue)
        {
            try
            {
                var response = await _orderQueueManager.UpdateOrderQueueAsync(orderQueue);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("GetOrderQueuesAsync")]
        public async Task<IActionResult> GetOrderQueuesAsync()
        {
            try
            {
                var response = await _orderQueueManager.GetOrderQueuesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("UpdateOrderQueueAsync/{orderQueueId}")]
        public async Task<IActionResult> UpdateOrderQueueAsync(long orderQueueId)
        {
            try
            {
                var response = await _orderQueueManager.DeleteOrderQueueAsync(orderQueueId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
