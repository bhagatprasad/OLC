using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardConfigurationController : ControllerBase
    {
        private readonly IRewardConfigurationManager _rewardConfigurationManager;
        public RewardConfigurationController(IRewardConfigurationManager rewardConfigurationManager)
        {
            _rewardConfigurationManager = rewardConfigurationManager;
        }

        [HttpGet]
        [Route("GetRewardConfigurationsByIdAsync/{Id}")]
        public async Task<IActionResult> GetRewardConfigurationsById(long Id)
        {
            try
            {
                var response = await _rewardConfigurationManager.GetRewardConfigurationsByIdAsync(Id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        [Route("GetAllRewardConfigurationsAsync")]
        public async Task<IActionResult> GetAllRewardConfigurations()
        {
            try
            {
                var response = await _rewardConfigurationManager.GetAllRewardConfigurationsAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("SaveRewardConfigurationAsync")]
        public async Task<IActionResult> SaveRewardConfiguration(RewardConfiguration rewardConfiguration)
        {
            try
            {
                var response = await _rewardConfigurationManager.SaveRewardConfigurationAsync(rewardConfiguration);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateRewardConfigurationAsync")]
        public async Task<IActionResult> UpdateRewardConfiguration(RewardConfiguration rewardConfiguration)
        {
            try
            {
                var response = await _rewardConfigurationManager.UpdateRewardConfigurationAsync(rewardConfiguration);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteRewardConfigurationAsync/{Id}")]
        public async Task<IActionResult> DeleteRewardConfiguration(long Id)
        {
            try
            {
                var response = await _rewardConfigurationManager.DeleteRewardConfigurationAsync(Id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
}
