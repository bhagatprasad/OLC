using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class EmailCampaignController : Controller
    {
        private readonly IEmailCampaingService _emailCampaingService;
        private readonly INotyfService _notyfService;

        public EmailCampaignController(IEmailCampaingService emailCampaingService, INotyfService notyfService)
        {
            _emailCampaingService = emailCampaingService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> Index()
        {
            List<EmailCampaign> emailCampaigns = new List<EmailCampaign>();

            var campaigns = await _emailCampaingService.GetAllEmailCampaignsAsync();

            if (campaigns.Any())
            {
                emailCampaigns = campaigns;
            }

            return View(emailCampaigns);
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
