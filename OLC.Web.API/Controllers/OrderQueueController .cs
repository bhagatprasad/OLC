using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

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
    }
}
