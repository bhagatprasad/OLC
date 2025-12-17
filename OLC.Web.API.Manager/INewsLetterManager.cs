using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface INewsLetterManager
    {
        Task<List<NewsLetter>> GetNewsLettersAsync();
        Task<bool> InsertNewsLetterAsync(NewsLetter newNewsLetter);
        Task<bool> UpdateNewsLetterAsync(NewsLetter newsLetter);
    }
}
