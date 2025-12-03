using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletTypeController : ControllerBase
    {
        private readonly IWalletTypeManager _walletTypeManager;

        public WalletTypeController(IWalletTypeManager walletTypeManager)
        {
            _walletTypeManager = walletTypeManager;
        }

        [HttpGet]
        [Route("GetAllWalletTypesAsync")]
        public async Task<IActionResult> GetWalletTypesAsync()
        {
            try
            {
                var response = await _walletTypeManager.GetAllWalletTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetWalletTypeByIdAsync/{id}")]
        public async Task<IActionResult> GetWalletTypeByIdAsync(long id)
        {
            try
            {
                var response = await _walletTypeManager.GetWalletTypeByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertWalletTypeAsync")]
        public async Task<IActionResult> InsertWalletTypeAsync(WalletType walletType)
        {
            try
            {
                var response = await _walletTypeManager.InsertWalletTypeAsync(walletType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateWalletTypeAsync")]
        public async Task<IActionResult> UpdateWalletTypeAsync(WalletType walletType)
        {
            try
            {
                var response = await _walletTypeManager.UpdateWalletTypeAsync(walletType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteWalletTypeAsync/{id}")]
        public async Task<IActionResult> DeleteWalletTypeAsync(long id)
        {
            try
            {
                var response = await _walletTypeManager.DeleteWalletTypeAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } 
    }
}
