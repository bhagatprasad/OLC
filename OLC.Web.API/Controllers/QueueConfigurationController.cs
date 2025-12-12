using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueConfigurationController : ControllerBase
    {
        private readonly IQueueConfigurationManager _queueConfigurationManager;

        public QueueConfigurationController(IQueueConfigurationManager queueConfigurationManager)
        {
            _queueConfigurationManager = queueConfigurationManager;
        }

        [HttpGet]
        [Route("GetAllQueueConfigurationsAsync")]
        public async Task<IActionResult> GetAllQueueConfigurationsAsync()
        {
            try
            {
                var response = await _queueConfigurationManager.GetAllQueueConfigurationsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetQueueConfigurationByIdAsync/{id}")]
        public async Task<IActionResult> GetQueueConfigurationByIdAsync(long id)
        {
            try
            {
                var response = await _queueConfigurationManager.GetQueueConfigurationByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("SaveQueueConfigurationAsync")]
        public async Task<IActionResult> SaveQueueConfigurationAsync(QueueConfiguration queueConfiguration)
        {
            try
            {
                var response = await _queueConfigurationManager.SaveQueueConfigurationAsync(queueConfiguration);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteQueueConfigurationAsync/{id}")]
        public async Task<IActionResult> DeleteQueueConfigurationAsync(long id)
        {
            try
            {
                var response = await _queueConfigurationManager.DeleteQueueConfigurationAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateQueueConfigurationAsync")]
        public async Task<IActionResult> UpdateQueueConfigurationAsync(QueueConfiguration queueConfiguration)
        {
            try
            {
                var response = await _queueConfigurationManager.UpdateQueueConfigurationAsync(queueConfiguration);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
