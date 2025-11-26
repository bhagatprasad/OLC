using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeManager _transactionTypeManager;
        public TransactionTypeController(ITransactionTypeManager transactionTypeManager)
        {
            _transactionTypeManager = transactionTypeManager;
        }

        [HttpGet]
        [Route("GetTransactionTypeAsync")]
        public async Task<IActionResult> GetTransactionTypeAsync()
        {
            try
            {
                var response = await _transactionTypeManager.GetTransactionTypeAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertTransactionTypeAsync")]
        public async Task<IActionResult> InsertTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                var response = await _transactionTypeManager.InsertTransactionTypeAsync(transactionType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
     

        [HttpGet]
        [Route("GetTransactionTypeByIdAsync/{id}")]
        public async Task<IActionResult> GetTransactionTypeByIdAsync(long id)
        {
            try
            {
                var response = await _transactionTypeManager.GetTransactionTypeByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteTransactionTypeAsync/{id}")]
        public async Task <IActionResult> DeleteTransactionTypeAsync(long id)
        {
            try
            {
                var response = await _transactionTypeManager.DeleteTransactionTypeAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateTransactionTypeAsync")]
        public async Task<IActionResult> UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                if (transactionType == null)
                    return BadRequest("Invalid transaction type data.");

                var response = await _transactionTypeManager.UpdateTransactionTypeAsync(transactionType);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the transaction type.");
            }
        }
        //Active Method.
        [HttpPost]
        [Route("ActivateTransactionTypeAsync")]
        public async Task<IActionResult> ActivateTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                var response = await _transactionTypeManager.ActivateTransactionTypeAsync(transactionType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
