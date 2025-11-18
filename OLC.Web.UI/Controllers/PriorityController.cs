using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    public class PriorityController : Controller
    {

        private readonly IPriorityService _priorityService;
        private readonly INotyfService _notyfService;

        public PriorityController(IPriorityService priorityService, INotyfService notyfService)
        {
            _priorityService = priorityService;
            _notyfService = notyfService;
        }
        [HttpGet("/Priority")]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult AddressTypes()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetPriorities()
        {
            try
            {
                var response = await _priorityService.GetPriorityAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }



        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SavePriority([FromBody] Priority priority)
        {
            try
            {
                bool isSaved = false;

                if (priority != null)
                {
                    if (priority.Id > 0)
                        isSaved = await _priorityService.UpdatePriorityAsync(priority);
                    else
                        isSaved = await _priorityService.InsertPriorityAsync(priority);

                    _notyfService.Success("Successfully saved Priority");
                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save Priority");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePriority(long priorityId)

        {
            try
            {
                bool isSaved = false;
                if (priorityId > 0)
                {
                    isSaved = await _priorityService.DeletePriorityAsync(priorityId);

                    if (isSaved)
                        _notyfService.Success("Successfully deleted Priority");
                    else
                        _notyfService.Warning("Unable to delete Priority");

                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete Priority");
                return Json(isSaved);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
