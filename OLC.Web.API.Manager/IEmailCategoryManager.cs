using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IEmailCategoryManager
    {
        Task<List<EmailCategory>> GetEmailCategoriesAsync ();
        Task<EmailCategory> GetEmailCategoryAsync(long id);
        Task<bool> InsertEmailCategoryAsync (EmailCategory emailCategory);
        Task<bool> UpdateEmailCategoryAsync (EmailCategory emailCategory);
        Task<bool> DeleteEmailCategoryAsync (long id);

    }
}
