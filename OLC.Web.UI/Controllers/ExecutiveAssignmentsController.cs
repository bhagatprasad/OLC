using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ExecutiveAssignmentsController: Controller
    {
        private readonly IExecutiveAssignmentsService _executiveAssignmentsService;
        private readonly INotyfService _notyfService;

        public ExecutiveAssignmentsController(IExecutiveAssignmentsService executiveAssignmentsService, INotyfService notyfService)
        {
            _executiveAssignmentsService = executiveAssignmentsService;
            _notyfService = notyfService;
        }

        [HttpGet("/ExecutiveAssignments")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AccountTypes()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertExecutiveAssignments(ExecutiveAssignments executiveAssignments)
        {

            try
            {
                bool isSaved = false;

                if (executiveAssignments != null)
                {

                    if (executiveAssignments.Id > 0)
                        isSaved = await _executiveAssignmentsService.UpdateExecutiveAssignmentsAsync(executiveAssignments);
                    else
                        isSaved = await _executiveAssignmentsService.InsertExecutiveAssignmentsAsync(executiveAssignments);

                    _notyfService.Success("Successfully saved to ExecutiveAssignments");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to insert to ExecutiveAssignments");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteExecutiveAssignments(long id)
        {
            try
            {
                var response = await _executiveAssignmentsService.DeleteExecutiveAssignmentsAsync(id);
                _notyfService.Success("Successfully deleted from ExecutiveAssignments");
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
