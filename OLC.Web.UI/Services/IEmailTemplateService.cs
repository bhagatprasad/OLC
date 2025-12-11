using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IEmailTemplateService
    {
        Task<List<EmailTemplate>> GetAllEmailTemplatesAsync();
        Task<EmailTemplate> GetEmailTemplateByIdAsync(long id);
        Task<bool> SaveEmailTemplateAsync(EmailTemplate emailtemplate);
        Task<bool> UpdateEmailTemplateAsync(EmailTemplate emailtemplate);
        Task<bool> DeleteEmailTemplateAsync(long id);
    }
}
