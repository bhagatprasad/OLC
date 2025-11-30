using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace OLC.Web.UI.Controllers
{
    public class RewardConfigurationController : Controller
    {
        private readonly INotyfService _notyfService;

        public RewardConfigurationController(IRewardConfigurationService rewardConfigurationService, INotyfService notyfService)
        {
            _rewardConfigurationService = rewardConfigurationService;
            _notyfService = notyfService;
        }
        
        public IActionResult RewardConfigurations()
        {
            return View();
        }

        [HttpGet("GetRewardConfigurations")]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetRewardConfigurations()
        {
            try
            {
                var response = await _rewardConfigurationService.GetAllRewardConfigurationsAsync();

                if (response == null)
                    response = new List<RewardConfiguration>();

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error("Failed to load Reward Configurations");
                return Json(new { data = new List<RewardConfiguration>() }); 
            }
        }



        [HttpPost]
        [Authorize(Roles =("Administrator"))]
        public async Task<IActionResult> SaveRewardConfiguration([FromBody] RewardConfiguration rewardConfiguration)
        {
            try
            {
                bool isSaved = false;

                if (rewardConfiguration != null)
                {
                    if (rewardConfiguration.Id > 0)
                        isSaved = await _rewardConfigurationService.UpdateRewardConfigurationAsync(rewardConfiguration);
                    else
                        isSaved = await _rewardConfigurationService.SaveRewardConfigurationAsync(rewardConfiguration);

        {
        }
        }


        [HttpDelete]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> DeleteRewardConfiguration(long id)
        {
            try
            {
                bool isSaved = false;
                if (id > 0)
                {
                    isSaved = await _rewardConfigurationService.DeleteRewardConfigurationAsync(id);
                    if (isSaved)
                        _notyfService.Success("Succesfully Deleted Reward Configuration");
                    else
                        _notyfService.Warning("Unable to delete Reward Configuration");
                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete Reward Configuration");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
