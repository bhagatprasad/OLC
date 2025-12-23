using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class EmailRuleTypeController : Controller
    {
        private readonly IEmailRuleTypeService _emailRuleTypeService;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationUser _applicationUser;
        public EmailRuleTypeController(IEmailRuleTypeService emailRuleTypeService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _emailRuleTypeService = emailRuleTypeService;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;

            var currentUser = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");

            if (!string.IsNullOrEmpty(currentUser))
            {
                //convert string to c# class object will user JosnConvert.DeSerializeObject<ApplicationUser>(currentUser);
                //convert object to string is used JosnConvert.SerializeObject(currentUser);
                _applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(currentUser);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> Index()
        {
            List<EmailRuleType> emailRuleType = new List<EmailRuleType>();

            var campaigns = await _emailRuleTypeService.GetAllEmailRuleTypesAsync();

            if (campaigns.Any())
            {
                emailRuleType = campaigns.OrderByDescending(x => x.Id).ToList();
            }

            return View(emailRuleType);
        }

        [HttpGet]
        public IActionResult CreateEmailRuleType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmailRuleType(EmailRuleType emailRuleType)
        {

            if (ModelState.IsValid)
            {
                //write logic to insert data

                //write logic send data to api 

                emailRuleType.CreatedBy = _applicationUser.Id;
                emailRuleType.CreatedOn = DateTimeOffset.Now;
                emailRuleType.ModifiedBy = _applicationUser.Id;
                emailRuleType.ModifiedOn = DateTimeOffset.Now;

                var respone = await _emailRuleTypeService.InsertEmailRuleTypeAsync(emailRuleType);

                if (respone)
                {
                    _notyfService.Success("Sucessfully added new EmailRuleType");
                    return RedirectToAction("Index", "EmailRuleType", null);
                }

                ModelState.AddModelError("", "there are mandatory fields are missing ,please add data and submit again");
                return View(emailRuleType);
            }

            ModelState.AddModelError("", "there are mandatory feilds are missing ,please add data and submit again");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditEmailRuleType(long id)
        {
            EmailRuleType emailRuleType = new EmailRuleType();

            var response = await _emailRuleTypeService.GetEmailRuleTypeByIdAsync(id);

            if (response != null)
            {
                emailRuleType = response;
            }

            return View(emailRuleType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmailRuleType(EmailRuleType emailRuleType)
        {
            if (ModelState.IsValid)
            {
                //write logic to insert data
                //write logic send data to api 
                emailRuleType.CreatedBy = _applicationUser.Id;
                emailRuleType.CreatedOn = DateTimeOffset.Now;
                emailRuleType.ModifiedBy = _applicationUser.Id;
                emailRuleType.ModifiedOn = DateTimeOffset.Now;

                var respone = await _emailRuleTypeService.UpdateEmailRuleTypeAsync(emailRuleType);
                if (respone)
                {
                    _notyfService.Success("Sucessfully updated EmailRuleType");
                    return RedirectToAction("Index", "EmailRuleType", null);
                }
                ModelState.AddModelError("", "there are mandatory fields are missing ,please add data and submit again");
                return View(emailRuleType);
            }
            ModelState.AddModelError("", "there are mandatory feilds are missing ,please add data and submit again");
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAllEmailRuleTypes()
        {
            try
            {
                var response = await _emailRuleTypeService.GetAllEmailRuleTypesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetEmailRuleTypeById(long id)
        {
            try
            {
                var response = await _emailRuleTypeService.GetEmailRuleTypeByIdAsync(id);
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
        public async Task<IActionResult> SaveEmailRuleType(EmailRuleType emailRuleType)
        {
            try
            {
                bool isSaved = false;

                if (emailRuleType != null)
                {
                    if (emailRuleType.Id > 0)
                        isSaved = await _emailRuleTypeService.UpdateEmailRuleTypeAsync(emailRuleType);
                    else
                        isSaved = await _emailRuleTypeService.InsertEmailRuleTypeAsync(emailRuleType);

                    _notyfService.Success("Successfully saved email RuleType");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save email RuleType");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}