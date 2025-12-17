using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IEmailTemplateManager
    {
        Task<List<EmailTemplate>> GetAllTemplatesAsync();
        Task<EmailTemplate> GetEmailTemplateByIdAsync(long id);
        Task<bool> DeleteEmailTemplateAsync(long id);
        Task<bool> InsertEmailTemplateAsync(EmailTemplate emailtemplate);
        Task <bool> UpdateEmailTemplateAsync(EmailTemplate emailTemplate);
    }
}
