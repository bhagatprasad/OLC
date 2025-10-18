using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Linq.Expressions;

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
        [Route("insertTransactionTypeAsync")]
        public async Task<IActionResult> insertTransactionTypeAsync(TransactionType transactionType)
        {
            try
            {
                var response = await _transactionTypeManager.insertTransactionTypeAsync(transactionType);
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

        [HttpGet]
        [Route("deleteTransactionTypeAsync/{id}")]
        public async Task <IActionResult> deleteTransactionTypeAsync(long id)
        {
            try
            {
                var response = await _transactionTypeManager.deleteTransactionTypeAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("updateTransactionTypeAsync")]
        public async Task<IActionResult> updateTransactionTypeAsync([FromBody] TransactionType transactionType)
        {
            try
            {
                if (transactionType == null)
                    return BadRequest("Invalid transaction type data.");

                var response = await _transactionTypeManager.updateTransactionTypeAsync(transactionType);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the transaction type.");
            }
        }
    }
}
