using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using Stripe;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class EmailCampaignController : Controller
    {
        private readonly IEmailCampaingService _emailCampaingService;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationUser _applicationUser;

        public EmailCampaignController(IEmailCampaingService emailCampaingService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor, ApplicationUser applicationUser)
        {
            _emailCampaingService = emailCampaingService;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
            _applicationUser = applicationUser;

            var currentUser = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");

            if (!string.IsNullOrEmpty(currentUser))
            {
                //convert string to c# class object will user JosnConvert.DeSerializeObject<ApplicationUser>(currentUser);
                //convert object to string is used JosnConvert.SerializeObject(currentUser);
                _applicationUser = JsonConvert.DeserializeObject<ApplicationUser>(currentUser);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> Index()
        {
            List<EmailCampaign> emailCampaigns = new List<EmailCampaign>();

            var campaigns = await _emailCampaingService.GetAllEmailCampaignsAsync();

            if (campaigns.Any())
            {
                emailCampaigns = campaigns.OrderByDescending(x => x.Id).ToList();
            }

            return View(emailCampaigns);
        }

        [HttpGet]
        public IActionResult CreateEmailCampaign()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmailCampaign(EmailCampaign emailCampaign)
        {
            if (ModelState.IsValid)
            {
                //write logic to insert data

                //write logic send data to api 

                emailCampaign.CreatedBy = _applicationUser.Id;
                emailCampaign.CreatedOn = DateTimeOffset.Now;
                emailCampaign.ModifiedBy = _applicationUser.Id;
                emailCampaign.ModifiedOn = DateTimeOffset.Now;

                var respone = await _emailCampaingService.InsertEmailCampaignAsync(emailCampaign);

                if (respone)
                {
                    _notyfService.Success("Sucessfully added new Campiang");
                    return RedirectToAction("Index", "EmailCampaign", null);
                }

                ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");
                return View(emailCampaign);
            }

            ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditEmailCampaign(long campaignId)
        {
            EmailCampaign emailCampaign = new EmailCampaign();

            var response = await _emailCampaingService.GetEmailCampaignByIdAsync(campaignId);

            if (response != null)
            {
                emailCampaign = response;
            }

            return View(emailCampaign);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmailCampaign(EmailCampaign emailCampaign)
        {
            if (ModelState.IsValid)
            {
                //write logic to insert data

                //write logic send data to api 

                emailCampaign.CreatedBy = _applicationUser.Id;
                emailCampaign.CreatedOn = DateTimeOffset.Now;
                emailCampaign.ModifiedBy = _applicationUser.Id;
                emailCampaign.ModifiedOn = DateTimeOffset.Now;

                var respone = await _emailCampaingService.UpdateEmailCampaignAsync(emailCampaign);

                if (respone)
                {
                    _notyfService.Success("Campaign updations success");
                    return RedirectToAction("Index", "EmailCampaign", null);
                }

                ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");
                return View(emailCampaign);
            }

            ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");

            return View(emailCampaign);
        }


        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAllEmailCampaigns()
        {
            try
            {
                var response = await _emailCampaingService.GetAllEmailCampaignsAsync();
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
        public async Task<IActionResult> GetEmailCampaignById(long id)
        {
            try
            {
                var response = await _emailCampaingService.GetEmailCampaignByIdAsync(id);
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
        public async Task<IActionResult> SaveAccountType([FromBody] EmailCampaign emailCampaign)
        {
            try
            {
                bool isSaved = false;

                if (emailCampaign != null)
                {
                    if (emailCampaign.Id > 0)
                        isSaved = await _emailCampaingService.UpdateEmailCampaignAsync(emailCampaign);
                    else
                        isSaved = await _emailCampaingService.InsertEmailCampaignAsync(emailCampaign);

                    _notyfService.Success("Successfully saved email campaign");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save email campaign");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
