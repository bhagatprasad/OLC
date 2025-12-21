using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IEmailRuleTypeService
    {
        Task<List<EmailRuleType>> GetAllEmailRuleTypesAsync();
        Task<EmailRuleType> GetEmailRuleTypeByIdAsync(long id);
        Task<bool> InsertEmailRuleTypeAsync(EmailRuleType emailRuleType);

        Task<bool> UpdateEmailRuleTypeAsync(EmailRuleType emailRuleType);
        Task<bool> DeleteEmailRuleTypeAsync(long id);

    }
}
