using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetRolesListAsync")]
        public async Task<IActionResult> GetRolesListAsync()
        {
            try
            {
                var response = await _roleManager.GetRolesListAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetRoleByIdAsync/{roleId}")]
        public async Task<IActionResult> GetRoleByIdAsync(long roleId)
        {
            try
            {
                var response = await _roleManager.GetRoleByIdAsync(roleId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertRoleAsync")]
        public async Task<IActionResult> InsertRoleAsync(Role role)
        {
            try
            {
                var response = await _roleManager.InsertRoleAsync(role);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An Error occured while inserting the role.");
            }
        }

        [HttpPost]
        [Route("UpdateRoleAsync")]
        public async Task<IActionResult> UpdateRoleAsync(Role role)
        {
            try
            {
                var response = await _roleManager.UpdateRoleAsync(role);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteRoleAsync/{roleId}")]
        public async Task<IActionResult> DeleteRoleAsync(long roleId)
        {
            try
            {
                var response = await _roleManager.DeleteRoleAsync(roleId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
