using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class TransactionRewardController : Controller
    {
        private readonly ITransactionRewardService _transactionRewardService;
        private readonly INotyfService _notyfService;

        public TransactionRewardController(ITransactionRewardService transactionRewardService, INotyfService notyfService)
        {
            _transactionRewardService = transactionRewardService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult Index ()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetAllTransactionRewards()
        {
            try
            {
                var response = await _transactionRewardService.GetAllTransactionRewardsAsync();
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
        public async Task<IActionResult> GetAllTransactionRewardsByUserId(long userId)
        {
            try
            {
                var response = await _transactionRewardService.GetAllTransactionRewardsByUserIdAsync(userId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetTransactionRewardByPaymentOrderId(long paymentOrderId)
        {
            try
            {
                var response = await _transactionRewardService.GetTransactionRewardByPaymentOrderIdAsync(paymentOrderId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> InsertTransactionReward(TransactionReward transactionReward)
        {

            try
            {
                bool isSaved = false;

                if (transactionReward != null)
                {

                    isSaved = await _transactionRewardService.InsertTransactionRewardAsync(transactionReward);

                    _notyfService.Success("Successfully saved TransactionReward");
                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save TransactionReward");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

            
