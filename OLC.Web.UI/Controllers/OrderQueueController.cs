using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = "Administrator,Executive")]
    public class OrderQueueController : Controller
    {
        private readonly IOrderQueueService _orderQueueService;

        public OrderQueueController(IOrderQueueService orderQueueService)
        {
            _orderQueueService = orderQueueService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetOrderQueues()
        {
            try
            {
                var response = await _orderQueueService.GetOrderQueuesAsync();
                return Json(new { data = response });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrderQueues([FromBody] OrderQueue orderQueue)
        {
            try
            {
                if (orderQueue == null)
                    return BadRequest();

                bool result = await _orderQueueService.InsertOrderQueuesAsync(orderQueue);

                return Json(new
                {
                    success = result
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
