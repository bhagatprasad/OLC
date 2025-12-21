using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
   public interface IEmailRuleTypeManager
    {
        Task<List<EmailRuleType>> GetAllEmailRuleTypesAsync();
        Task<EmailRuleType> GetEmailRuleTypeByIdAsync(long id);
        Task<bool> InsertEmailRuleTypeAsync(EmailRuleType emailRuleType);

        Task<bool> UpdateEmailRuleTypeAsync(EmailRuleType emailRuleType);
        Task<bool> DeleteEmailRuleTypeAsync(long id);
    }
}
