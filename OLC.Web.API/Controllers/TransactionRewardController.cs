using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using Twilio.Http;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionRewardController : ControllerBase
    {
        private readonly ITransactionRewardManager _transactionRewardManager;
        public TransactionRewardController(ITransactionRewardManager transactionRewardManager)
        {
            _transactionRewardManager = transactionRewardManager;
        }
        [HttpPost]
        [Route("InsertTransactionRewardAsync")]

        public async Task<IActionResult> InserTransactionRewardAsync(TransactionReward transactionReward)
        {
            try
            {
                var response = await _transactionRewardManager.InsertTransactionRewardAsync(transactionReward);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllTransactionRewardsAsync")]
        public async Task<IActionResult> GetAllTransactionRewardsAsync()
        {
            try
            {
                var response = await _transactionRewardManager.GetAllTransactionRewardsAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllTransactionRewardsAsyncByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetAllTransactionRewardsAsyncByUserIdAsync(long userId)
        {
            try
            {
                var response = await _transactionRewardManager.GetAllTransactionRewardsByUserIdAsync(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetTransactionRewardByPaymentOrderIdAsync/{paymentOrderId}")]
        public async Task<IActionResult> GetTransactionRewardByPaymentOrderIdAsync(long paymentOrderId)
        {
            try
            {
                var response = await _transactionRewardManager.GetTransactionRewardByPaymentOrderIdAsync(paymentOrderId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
