using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class CardTypeController : Controller
    {
        private readonly ICardTypeService _cardTypeService;
        private readonly INotyfService _notyfService;

        public CardTypeController(ICardTypeService cardTypeService,
            INotyfService notyfService)
        {
            _cardTypeService = cardTypeService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetCardTypes()
        {
            try
            {
                var response = await _cardTypeService.GetCardTypeAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> InsertOrUpdateCardType([FromBody] CardType cardType)
        {
            try
            {
                bool isSucess = false;

                if (cardType.Id > 0)
                    isSucess = await _cardTypeService.InsertCardTypeAsync(cardType);
                else
                    isSucess = await _cardTypeService.UpdateCardTypeAsync(cardType);

                _notyfService.Success("Save operation successful");

                return Json(true);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }

}
