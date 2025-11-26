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
        [Route("InsertAddressTypeAsync")]
        public async Task<IActionResult> InsertAddressTypeAsync(AddressType addressType)
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
        [Route("UpdateAddressTypeAsync")]
        public async Task<IActionResult> UpdateAddressTypeAsync(AddressType addressType)
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

        [HttpPost]
        [Route("ActivateAddressTypeAsync")]
        public async Task<IActionResult> ActivateAddressTypeAsync(AddressType addressType)
        {
            try
            {
                var response = await _addressTypeManager.ActivateAddressTypeAsync(addressType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("DeleteAddressTypeAsync/{addressTypeId}")]
        public async Task<IActionResult> DeleteAddressTypeAsync(long addressTypeId)
        {
            try
            {
                var response = await _addressTypeManager.DeleteAddressTypeAsync(addressTypeId);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }


}
