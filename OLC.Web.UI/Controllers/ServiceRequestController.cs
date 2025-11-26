using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestService _serviceRequest;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationUser _applicationUser;
        public ServiceRequestController(IServiceRequestService serviceRequest, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _serviceRequest = serviceRequest;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;

            string sessionInfo = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");

            if (!string.IsNullOrEmpty(sessionInfo))
            {
                _applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(sessionInfo);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = new List<Category>
{
    new Category { Id = 1, Name = "Technical Support" },
    new Category { Id = 2, Name = "Billing Inquiry" },
    new Category { Id = 3, Name = "Account Management" },
    new Category { Id = 4, Name = "Feature Request" },
    new Category { Id = 5, Name = "Bug Report" }
};
            // Sample Priorities
            var priorities = new List<Priority>
{
    new Priority { Id = 1, Name = "Low" },
    new Priority { Id = 2, Name = "Medium" },
    new Priority { Id = 3, Name = "High" },
    new Priority { Id = 4, Name = "Urgent" },
    new Priority { Id = 5, Name = "Critical" }
};

            ServiceRequestModel model = new ServiceRequestModel();

            model.categories = categories;

            model.priorities = priorities;

            return View(model);

        }


        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
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
        public async Task<IActionResult> GetServiceRequestByTicketId(long ticketId)
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

        [HttpPost]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> InsertOrUpdateServiceRequest([FromBody] ServiceRequest serviceRequest)
        {
            try
            {
                bool isSucess = false;

                if (serviceRequest.TicketId > 0)
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
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetServiceRequestByUserId(long userId)
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

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetServiceRequestWithReplies(long ticketId)
        {
            try
            {
                var response = await _serviceRequest.GetServiceRequestWithRepliesAsync(ticketId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }


        [HttpPost]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> AssingingServiceRequestAsync(ServiceRequest serviceRequest)
        {
            try
            {
                var response = await _serviceRequest.AssingingServiceRequestAsync(serviceRequest);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult ServiceRequestDetails(long ticketId)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> CancelServiceRequestByTicketIdAsync(ServiceRequest serviceRequest)
        {
            try
            {
                var response = await _serviceRequest.CancelServiceRequestByTicketIdAsync(serviceRequest);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertServiceRequestReply([FromBody] ServiceRequestReplies serviceRequestReplies)
        {
            try
            {
                var response = await _serviceRequest.InsertServiceRequestRepliesAsync(serviceRequestReplies);
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
