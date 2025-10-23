using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Security.Cryptography.X509Certificates;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankManager _bankManager;

        public BankController(IBankManager bankManager)
        {
            _bankManager = bankManager;
        }
        [HttpGet]
        [Route("GetBankAsync")]
        public async Task<IActionResult> GetBankAsync()
        {
            try
            {
                var responce = await _bankManager.GetBankAsync();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetBankByIdAsync/{id}")]
        public async Task<IActionResult> GetBankByIdAsync(long id)
        {
            try
            {
                var responce = await _bankManager.GetBankByIdAsync(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertBankAsync")]
        public async Task<IActionResult> InsertBankAsync(Bank bank)
        {
            try
            {
                var response = await _bankManager.InsertBankAsync(bank);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateBankAsync")]
        public async Task<IActionResult> UpdateBankAsync(Bank bank)
        {
            try
            {
                var responce = await _bankManager.UpdateBankAsync(bank);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpDelete]
        [Route("DeleteBankAsync/{Id}")]
        public async Task<IActionResult> DeleteBankAsync(long Id)
        {
            try
            {
                var responce = await _bankManager.DeleteBankAsync(Id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }    
    } 
}
