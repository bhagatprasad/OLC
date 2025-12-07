using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface INewsLetterService
    {
        Task<List<NewsLetter>> GetNewsLettersAsync();
        Task<bool> InsertNewsLetterAsync(NewsLetter newsLetter);
        Task<bool> UpdateNewsLetterAsync(NewsLetter newsLetter);
    }
}
