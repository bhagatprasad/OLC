using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System;
using System.Threading.Tasks;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly INotyfService _notyfService;

        public BankAccountController(IBankAccountService bankAccountService,
            INotyfService notyfService)
        {
            _bankAccountService = bankAccountService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("User"))]
        public IActionResult UserBankAccounts()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetAllUserBankAccountsAsync(long userId)
        {
            try
            {
                var accounts = await _bankAccountService.GetAllUserBankAccountByUserIdAsync(userId);
                return Json(new { data = accounts });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        //GET
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetAllUserBankAccountsAsync()
        {
            try
            {
                var bankAccounts= await _bankAccountService.GetAllUserBankAccountsAsync();
                return Json(new { data = bankAccounts });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserBankAccount(long id)
        {
            try
            {
                bool isSaved = false;

                if (id > 0)
                {
                    isSaved = await _bankAccountService.DeleteUserBankAccountAsync(id);

                    if (isSaved)
                        _notyfService.Success("Successfully deleted user bank account");
                    else
                        _notyfService.Warning("Unable to delete bank account");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to delete user bank account");

                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = ("User"))]
        public async Task<IActionResult> SaveUserBankAccount([FromBody] UserBankAccount userBankAccount)
        {
            try
            {
                bool isSaved = false;

                if (userBankAccount != null)
                {
                    if (userBankAccount.Id > 0)
                        isSaved = await _bankAccountService.UpdateUserBankAccountAsync(userBankAccount);
                    else
                        isSaved = await _bankAccountService.InsertUserBankAccountAsync(userBankAccount);

                    _notyfService.Success("Successfully saved user bank account");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save user bank account");

                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActivateBankAccount([FromBody] UserBankAccount userBankAccount)
        {
            try
            {
                bool isSaved = false;
                isSaved = await _bankAccountService.ActivateBankAccountAsync(userBankAccount);

                if (isSaved)
                    _notyfService.Success("Successfully Activated user Bank Account");
                else
                    _notyfService.Error("unable to Activate user Bank Account");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
