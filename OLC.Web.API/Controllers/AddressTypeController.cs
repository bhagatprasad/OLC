using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypeController : ControllerBase
    {
        private readonly IAddressTypeManager _addressTypeManager;

        public AddressTypeController(IAddressTypeManager addressTypeManager)
        {
            _addressTypeManager = addressTypeManager;
        }
        
        [HttpGet]
        [Route("GetAddressTypeByIdAsync/{addressTypeId}")]
        public async Task<IActionResult> GetAddressTypeByIdAsync(long addressTypeId)
        {
            try
            {
                var response = await _addressTypeManager.GetAdressTypeByIdAsync(addressTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAddressTypeAsync")]
        public async Task<IActionResult> GetAddressTypeAsync()
        {
            try
            {
                var response = await _addressTypeManager.GetAddressTypeAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertAddressAsync")]
        public async Task<IActionResult> InsertAddressAsync(AddressType addressType)
        {
            try
            {
                var response = await _addressTypeManager.InsertAddressTypeAsync(addressType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateAddressAsync")]
        public async Task<IActionResult> UpdateAddressAsync(AddressType addressType)
        {
            try
            {
                var response = await _addressTypeManager.UpdateAddressTypeAsync(addressType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteAddressAsync/{Id}")]
        public async Task<IActionResult> DeleteAddressAsync(long Id)
        {
            try
            {
                var response = await _addressTypeManager.DeleteAddressTypeAsync(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
