using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;


namespace OLC.Web.UI.Controllers
{
    [Route("RewardConfiguration")]
    [Authorize(Roles = "Administrator,Executive,User")]
    public class RewardConfigurationController : Controller
    {
        private readonly IRewardConfigurationService _rewardConfigurationService;
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

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetRewardConfigurations()
        {
            try
            {
                var response = await _rewardConfigurationService.GetAllRewardConfigurationsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }


        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
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

                    _notyfService.Success("Successfully saved Reward Configuration");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save Reward Configuration");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Administrator")]
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
