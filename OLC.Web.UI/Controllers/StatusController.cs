using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly INotyfService _notyfService;

        public StatusController(IStatusService statusService,
            INotyfService notyfService)
        {
            _statusService = statusService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetStatuses()
        {
            try
            {
                var response = await _statusService.GetStatusAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> InsertOrUpdateStatus([FromBody] Status status)
        {
            try
            {
                bool isSucess = false;

                if (status.Id > 0)
                    isSucess = await _statusService.UpdateStatusAsync(status);
                else
                    isSucess = await _statusService.InsertStatusAsync(status);

                _notyfService.Success("Save operation successful");

                return Json(true);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
