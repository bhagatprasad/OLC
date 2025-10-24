using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

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
            [Route("GetTransactionFeeByIdAsync/{feeId}")]
            public async Task<IActionResult> GetTransactionByIdAsync(long feeId)
            {
                try
                {
                    var responce = await _transactionFeeManager.GetTransactionFeeByIdAsync(feeId);
                    return Ok(responce);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }        
    } 
}