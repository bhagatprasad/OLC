using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequest _serviceRequest;
        private readonly INotyfService _notyfService;
        public ServiceRequestController(IServiceRequest serviceRequest, INotyfService notyfService)
        {
            _serviceRequest = serviceRequest;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("User"))]
        public async Task<IActionResult> GetAllServiceRequests()
        {
            try
            {
                var response = await _serviceRequest.GetAllServiceRequestsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetServiceRequestByIdAsync(long ticketId)
        {
            try
            {
                var response = await _serviceRequest.GetServiceRequestByIdAsync(ticketId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        [Authorize(Roles = ("User"))]
        public IActionResult Create(string category)
        {
            var model = new ServiceRequest
            {
                Category = category
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> InsertOrUpdateServiceRequest([FromBody] ServiceRequest serviceRequest)
        {
            try
            {
                bool isSucess = false;

                if (serviceRequest.UserId > 0)
                    isSucess = await _serviceRequest.UpdateServiceRequestAsync(serviceRequest);
                else
                    isSucess = await _serviceRequest.InsertServiceRequestAsync(serviceRequest);

                _notyfService.Success("Save operation successful");

                return Json(true);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        [Authorize(Roles =("User"))]
        public async Task<IActionResult>GetServiceRequestByUserId(long userId)
        {
            try
            {
                var response = await _serviceRequest.GetServiceRequestByUserId(userId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
