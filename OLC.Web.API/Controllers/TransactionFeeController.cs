using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionFeeController : Controller
    {
        private readonly ITransactionFeeManager _transactionFeeManager;
        public TransactionFeeController(ITransactionFeeManager transactionFeeManager)
        {
            _transactionFeeManager = transactionFeeManager;
        }

        [HttpGet]
        [Route("GetTransactionFeesListAsync")]
        public async Task<IActionResult> GetTransactionFeesListAsync()
        {
            try
            {
                var responce = await _transactionFeeManager.GetTransactionFeesListAsync();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetTransactionFeeByIdAsync/{transactionFeeId}")]
        public async Task<IActionResult> GetTransactionByIdAsync(long transactionFeeId)
        {
            try
            {
                var responce = await _transactionFeeManager.GetTransactionFeeByIdAsync(transactionFeeId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertTransactionFeeAsync")]
        public async Task<IActionResult> InsertTransactionFeeAsync(TransactionFee transactionFee)
        {
            try
            {
                var responce = await _transactionFeeManager.InsertTransactionFeeAsync(transactionFee);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
            [HttpPost]
            [Route("UpdateTransactionFeeAsync")]
            public async Task<IActionResult> UpdateTransactionFeeAsync(TransactionFee transactionFee)
            {
                try
                {
                    var responce = await _transactionFeeManager.UpdateTransactionFeeAsync(transactionFee);
                    return Ok(responce);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        [HttpDelete]
        [Route("DeleteTransactionFeeAsync")]
        public async Task<IActionResult> DeleteTransactionFeeAsync(long transactionFeeId)
        {
            try
            {
                var responce = await _transactionFeeManager.DeleteTransactionFeeAsync(transactionFeeId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}