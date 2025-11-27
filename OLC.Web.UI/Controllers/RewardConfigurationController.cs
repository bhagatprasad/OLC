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
    [Authorize(Roles ="Administrator,Executive,User")]
    public class RewardConfigurationController : Controller
    {
        private readonly IRewardConfigurationService _rewardConfigurationService;
        private readonly INotyfService _notyfService;

        public RewardConfigurationController(IRewardConfigurationService rewardConfigurationService, INotyfService notyfService)
        {
            _rewardConfigurationService = rewardConfigurationService;
            _notyfService = notyfService;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Executive")]
        public IActionResult RewardConfigurations()
        {
            return View();
        }

        //[HttpGet]
        //public async Task <IActionResult> GetRewardConfiguration(long id)
        //{
        //    try
        //    {
        //        var rewardConfigurations=new List<RewardConfiguration>();
        //        rewardConfigurations.Add(new RewardConfiguration)
        //            {
        //            Id = 1,
        //            RewardName = "Diwali Cashback Bonanza",
        //            RewardType = "Cashback",
        //            RewardValue = 500.00m,
        //            RewardPercentage = 10.00m,
        //            MinimumTransactionAmount = 3000.00m,
        //            MaximumReward = 1000.00m,
        //            ValidFrom = new DateTime(2025, 10, 15),
        //            ValidTo = new DateTime(2025, 11, 20, 23, 59, 59),
        //            IsActive = true,
        //            CreatedBy = "admin@shop.com",
        //            CreatedOn = new DateTime(2025, 9, 20, 14, 30, 0),
        //            ModifiedBy = "marketing@shop.com",
        //            ModifiedOn = new DateTime(2025, 11, 1, 10, 15, 0)
        //            });
        //        var response = await _rewardConfigurationService.GetRewardConfigurationByIdAsync(id);
        //        return Json(new { data = response });
        //    }
        //    catch (Exception ex)
        //    {
        //        _notyfService.Error(ex.Message);
        //        throw;
        //    }
        //}



       
        

        [HttpGet("GetRewardConfigurations")]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> GetRewardConfigurations()
        {
            var list = GetDummyData(); // Replace with real DB later
            return Ok(new { data = list });
        }

        // GET: /RewardConfiguration/GetRewardConfiguration/5   → For Edit/View/Activate modal
        [HttpGet("GetRewardConfiguration/{id}")]
        public async Task<IActionResult> GetRewardConfiguration(long id)
        {
            var list = GetDummyData();
            var item = list.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound(new { success = false, message = "Not found" });

            return Ok(new { data = item });
        }

        // Dummy data (30 records ready – you said DB has 30)
        private List<RewardConfiguration> GetDummyData()
        {
            var data = new List<RewardConfiguration>();
            for (int i = 1; i <= 30; i++)
            {
                data.Add(new RewardConfiguration
                {
                    Id = i,
                    RewardName = i % 3 == 0 ? $"Festive Offer {i}" : i % 2 == 0 ? $"Weekend Bonus {i}" : $"Diwali Cashback {i}",
                    RewardType = i % 4 == 0 ? "Percentage" : i % 3 == 0 ? "FixedAmount" : i % 2 == 0 ? "Discount" : "Cashback",
                    RewardValue = (i % 5 + 1) * 100m,
                    MinimumTransactionAmount = i * 500m,
                    MaximumReward = i % 7 == 0 ? (decimal?)null : (i * 100m),
                    IsActive = i % 6 != 0, // 5 inactive ones for testing activate button
                    ValidFrom = DateTimeOffset.Now.AddDays(-30 + i),
                    ValidTo = DateTimeOffset.Now.AddDays(30 + i),
                    CreatedBy = 101,
                    CreatedOn = DateTimeOffset.Now.AddDays(-60 + i),
                    
                });
            }
            return data;
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
                        isSaved = await _rewardConfigurationService.InsertRewardConfigurationAsync(rewardConfiguration);

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
