using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class CampaignTypeController : Controller
    {
        private readonly ICampaignTypeServices _campaignTypeServices;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationUser _applicationUser;

        public CampaignTypeController(ICampaignTypeServices campaignTypeServices, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _campaignTypeServices = campaignTypeServices;
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
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> Index()
        {
            List<CampaignType> CampaignTypes = new List<CampaignType>();

            var campaigns = await _campaignTypeServices.GetAllCampaignTypesAsync();

            if (campaigns.Any())
            {
                CampaignTypes = campaigns.OrderByDescending(x => x.Id).ToList();
            }

            return View(CampaignTypes);
        }

        [HttpGet]
        public IActionResult CreateCampaignType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCampaignType(CampaignType campaignType)
        {
            if (ModelState.IsValid)
            {

                campaignType.CreatedBy = _applicationUser.Id;
                campaignType.CreatedOn = DateTimeOffset.Now;
                campaignType.ModifiedBy = _applicationUser.Id;
                campaignType.ModifiedOn = DateTimeOffset.Now;

                var respone = await _campaignTypeServices.InsertCampaignTypeAsync(campaignType);

                if (respone)
                {
                    _notyfService.Success("Sucessfully added new Campiang Type");
                    return RedirectToAction("Index", "CampaignType", null);
                }

                ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");
                return View(campaignType);
            }

            ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditCampaignType(long Id)
        {
            CampaignType campaignType = new CampaignType();

            var response = await _campaignTypeServices.GetCampaignTypeByIdAsync(Id);

            if (response != null)
            {
                campaignType = response;
            }

            return View(campaignType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCampaignType(CampaignType campaignType)
        {
            if (ModelState.IsValid)
            {

                campaignType.CreatedBy = _applicationUser.Id;
                campaignType.CreatedOn = DateTimeOffset.Now;
                campaignType.ModifiedBy = _applicationUser.Id;
                campaignType.ModifiedOn = DateTimeOffset.Now;

                var respone = await _campaignTypeServices.UpdateCampaignTypeAsync(campaignType);

                if (respone)
                {
                    _notyfService.Success("Sucessfully updated new Campiang Type");
                    return RedirectToAction("Index", "CampaignType", null);
                }

                ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");
                return View(campaignType);
            }

            ModelState.AddModelError("", "there are mandatry feild missing ,please add data and submit again");

            return View();
        }
    }
}
