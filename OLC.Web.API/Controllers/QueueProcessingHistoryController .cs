using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueProcessingHistoryController : ControllerBase
    {
        private readonly IQueueProcessingHistoryManager _historyManager;

        public QueueProcessingHistoryController(
            IQueueProcessingHistoryManager historyManager)
        {
            _historyManager = historyManager;
        }
       
    

    }
}
