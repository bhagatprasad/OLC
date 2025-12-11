using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class EmailTemplateController : Controller
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly INotyfService _notyfService;

        public EmailTemplateController(IEmailTemplateService emailTemplateService, INotyfService notyfService)
        {
            _emailTemplateService = emailTemplateService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAllEmailTemplates()
        {
            try
            {
                var response = await _emailTemplateService.GetAllEmailTemplatesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteEmailTemplate(long id)
        {
            try
            {
                bool isSaved = false;
                if (id > 0)
                {
                    isSaved = await _emailTemplateService.DeleteEmailTemplateAsync(id);
                    if (isSaved)
                        _notyfService.Success("Succesfully Deleted EmailTemplate");
                    else
                        _notyfService.Warning("Unable to delete EmailTemplate");
                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete EmailTemplate");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task <IActionResult> SaveEmailTemplate([FromBody] EmailTemplate emailTemplate)
        {
            try
            {
                bool isSaved = false;

                if (emailTemplate != null)
                {
                    if (emailTemplate.Id > 0)
                        isSaved = await _emailTemplateService.UpdateEmailTemplateAsync(emailTemplate);
                    else
                        isSaved = await _emailTemplateService.SaveEmailTemplateAsync(emailTemplate);

                    _notyfService.Success("Successfully saved EmailTemplate");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save EmailTemplate");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    } 
}
