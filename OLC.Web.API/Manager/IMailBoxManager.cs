using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IMailBoxManager
    {
        Task<List<MailBox>> GetAllMailBoxesAsync();
        Task<MailBox> GetMailBoxByIdAsync (long id);
        Task<bool> InsertMailBoxAsync(MailBox mailbox);
    }
}
