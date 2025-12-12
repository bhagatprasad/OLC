using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class QueueConfigurationController : Controller
    {
        private readonly IQueueConfigurationService _queueConfigurationService;
        private readonly INotyfService _notyfService;
        public QueueConfigurationController(IQueueConfigurationService queueConfigurationService, INotyfService notyfService)
        {
            _queueConfigurationService = queueConfigurationService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAllQueueConfigurations()
        {
            try
            {
                var response = await _queueConfigurationService.GetAllQueueConfigurationsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> SaveQueueConfiguration([FromBody] QueueConfiguration queueConfiguration)
        {
            try
            {
                bool isSaved = false;

                if (queueConfiguration != null)
                {
                    if (queueConfiguration.Id > 0)
                        isSaved = await _queueConfigurationService.UpdateQueueConfigurationAsync(queueConfiguration);
                    else
                        isSaved = await _queueConfigurationService.SaveQueueConfigurationAsync(queueConfiguration);

                    _notyfService.Success("Successfully saved QueueConfiguration");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save QueueConfiguration");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteQueueConfiguration(long id)
        {
            try
            {
                bool isSaved = false;
                if (id > 0)
                {
                    isSaved = await _queueConfigurationService.DeleteQueueConfigurationAsync(id);
                    if (isSaved)
                        _notyfService.Success("Succesfully Deleted QueueConfiguration");
                    else
                        _notyfService.Warning("Unable to delete QueueConfiguration");
                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete QueueConfiguration");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
