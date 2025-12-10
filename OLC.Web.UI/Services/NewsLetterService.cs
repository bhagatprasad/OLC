using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class NewsLetterService :INewsLetterService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public NewsLetterService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<NewsLetter>> GetNewsLettersAsync()
        {
            return await _repositoryFactory.SendAsync<List<NewsLetter>>(HttpMethod.Get, "NewsLetter/GetNewsLettersAsync");
        }

        public async Task<bool> InsertNewsLetterAsync(NewsLetter newsLetter)
        {
            return await _repositoryFactory.SendAsync<NewsLetter, bool>(HttpMethod.Post, "NewsLetter/InsertNewsLetterAsync",newsLetter);
        }

        public async Task<bool> UpdateNewsLetterAsync(NewsLetter newsLetter)
        {
            return await _repositoryFactory.SendAsync<NewsLetter, bool>(HttpMethod.Post, "NewsLetter/UpdateNewsLetterAsync",newsLetter);
        }
    }
}
