using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly INotyfService _notyfService;
        public CreditCardController(ICreditCardService creditCardService,
            INotyfService notyfService)
        {
            _creditCardService = creditCardService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("User"))]
        public IActionResult UserCreditCards()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetUserCreditCards(long userId)
        {
            try
            {
                var cards = await _creditCardService.GetUserCreditCardsAsync(userId);
                return Json(new { data = cards });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserCredit(long creditCardId)
        {
            try
            {
                bool isSaved = false;

                if (creditCardId > 0)
                {

                    isSaved = await _creditCardService.DeleteUserCreditAsync(creditCardId);

                    if (isSaved)
                        _notyfService.Success("Successfully saved user credit card");
                    else
                        _notyfService.Warning("Unable to delete credit card");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save user credit card");

                return Json(isSaved);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize(Roles = ("User"))]
        public async Task<IActionResult> SaveUserCreditCard([FromBody] UserCreditCard userCreditCard)
        {
            try
            {
                bool isSaved = false;

                if (userCreditCard != null)
                {
                    if (userCreditCard.Id > 0)
                        isSaved = await _creditCardService.UpdateUserCreditCardAsync(userCreditCard);
                    else
                        isSaved = await _creditCardService.InsertUserCreditCardAsync(userCreditCard);

                    _notyfService.Success("Successfully saved user credit card");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save user credit card");

                return Json(isSaved);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
