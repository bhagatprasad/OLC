using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

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
