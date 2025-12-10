using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IExecutivesManager
    {
        Task<Executives> GetExecutiveByUserId(long userId);
        Task<List<Executives>> GetAllExecutivesAsync();
        Task<bool> InsertExecutuveAsyncs(Executives executive);
    }
}
