using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyfService;

        public OrderQueueController(IOrderQueueService orderQueueService,INotyfService notyfService)
        {
            _orderQueueService = orderQueueService;
            _notyfService = notyfService;
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

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetPaymentOrderQueue()
        {
            try
            {
                var response = await _orderQueueService.GetPaymentOrderQueueAsync();
                return Json(new { data = response });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrderQueues([FromBody] OrderQueue orderQueue)
        {
            try
            {
                bool isSaved = false;

                if (orderQueue != null)
                {
                    if (orderQueue.Id > 0)
                        isSaved = await _orderQueueService.UpdateOrderQueueAsync(orderQueue);
                    else
                        isSaved = await _orderQueueService.InsertOrderQueueAsync(orderQueue);

                    _notyfService.Success("Successfully saved order queue");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save order queue");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrderQueue(long orderQueueId)
        {
            try
            {
                bool isSaved = false;
                if (orderQueueId > 0)
                {
                    isSaved = await _orderQueueService.DeleteOrderQueueAsync(orderQueueId);
                    if (isSaved)
                        _notyfService.Success("Successfully deleted order queue");
                    else
                        _notyfService.Warning("Unable to delete order queue");
                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete order queue");
                return Json(isSaved);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
