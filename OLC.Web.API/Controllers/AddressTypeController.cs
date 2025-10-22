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
        [Route("GetUserAddressTypeByIdAsync/{addressTypeId}")]
        public async Task<IActionResult> GetUserAddressTypeByIdAsync(long addressTypeId)
        {
            try
            {
                var response = await _addressTypeManager.GetUserAdressTypeByIdAsync(addressTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserAddressAsync")]
        public async Task<IActionResult> GetUserAddressIdAsync()
        {
            try
            {
                var response = await _addressTypeManager.GetUserAddressTypeAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertUserAddressAsync")]
        public async Task<IActionResult> InsertUserAddressAsync(AddressType addressType)
        {
            try
            {
                var response = await _addressTypeManager.InsertUserAddressTypeAsync(addressType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateUserAddressAsync")]
        public async Task<IActionResult> UpdateUserAddressAsync(UpdateAddressType updateAddressType)
        {
            try
            {
                var response = await _addressTypeManager.UpdateUserAddressTypeAsync(updateAddressType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteUserAddressAsync/{Id}")]
        public async Task<IActionResult> DeleteUserAddressAsync(long Id)
        {
            try
            {
                var response = await _addressTypeManager.DeleteUserAddressTypeAsync(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
