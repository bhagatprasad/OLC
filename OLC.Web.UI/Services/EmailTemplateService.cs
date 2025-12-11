using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class EmailTemplateService :IEmailTemplateService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public EmailTemplateService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteEmailTemplateAsync(long id)
        {
            var url = Path.Combine("EmailTemplate/DeleteEmailTemplateAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<EmailTemplate>> GetAllEmailTemplatesAsync()
        {
            return await _repositoryFactory.SendAsync<List<EmailTemplate>>(HttpMethod.Get, "EmailTemplate/GetAllTemplatesAsync");
        }

        public async Task<EmailTemplate> GetEmailTemplateByIdAsync(long id)
        {
            var url = Path.Combine("EmailTemplate/GetEmailTemplateByIdAsync",id.ToString());
            return await _repositoryFactory.SendAsync<EmailTemplate>(HttpMethod.Get, url);
        }

        public async Task<bool> SaveEmailTemplateAsync(EmailTemplate emailtemplate)
        {
            return await _repositoryFactory.SendAsync<EmailTemplate, bool>(HttpMethod.Post, "EmailTemplate/SaveEmailTemplateAsync",emailtemplate);
        }

        public async Task<bool> UpdateEmailTemplateAsync(EmailTemplate emailtemplate)
        {
            return await _repositoryFactory.SendAsync<EmailTemplate, bool>(HttpMethod.Post,
                "EmailTemplate/UpdateEmailTemplateAsync",emailtemplate);
        }
    }
}
