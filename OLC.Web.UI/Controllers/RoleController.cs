using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System.Threading.Tasks;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly INotyfService _notyfService;

        public RoleController(IRoleService roleService, INotyfService notyfService)
        {
            _roleService = roleService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> Index()
        {

            var response = await _roleService.GetRolesAsync();

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            role.CreatedBy = -1;
            role.CreatedOn = DateTime.Now;
            role.ModifiedBy=-1;
            role.ModifiedOn = DateTime.Now;
            role.IsActive = true;


            var response = await _roleService.InsertRoleAsync(role);
            if (response)
            {
                _notyfService.Success("Successfully added role");
                return RedirectToAction("Index", "Role", null);
            }

            _notyfService.Error("Unable to add role,please try again");

            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long roleId)
        {
            var response = await _roleService.GetRoleByIdAsync(roleId);
            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Role role)
        {
            role.CreatedBy = -1;
            role.CreatedOn = DateTime.Now;
            role.ModifiedBy = -1;
            role.ModifiedOn = DateTime.Now;
            role.IsActive = true;
           

            var response = await _roleService.UpdateRoleAsync(role);
            if (response)
            {
                _notyfService.Success("Successfully updated role");
                return RedirectToAction("Index", "Role", null);
            }

            _notyfService.Error("Unable to update role,please try again");

            return View(role);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var response = await _roleService.GetRolesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveRole([FromBody] Role role)
        {
            try
            {
                bool isSaved = false;

                if (role != null)
                {
                    if (role.Id > 0)
                        isSaved = await _roleService.UpdateRoleAsync(role);
                    else
                        isSaved = await _roleService.InsertRoleAsync(role);

                    _notyfService.Success("Successfully saved role");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save role");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRole(long roleId)
        {
            try
            {
                bool isSaved = false;
                if (roleId > 0)
                {
                    isSaved = await _roleService.DeleteRoleAsync(roleId);
                    if (isSaved)
                        _notyfService.Success("Successfully deleted role");
                    else
                        _notyfService.Warning("Unable to delete role");
                    return Json(isSaved);
                }
                _notyfService.Error("Invalid role ID");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
