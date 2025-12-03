using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class UserLoginHistoryService : IUserLoginHistoryService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public UserLoginHistoryService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<UserLogInHistory>> GetAllUserActivityTodayAsync()
        {
           
            var url = Path.Combine("UserLogInHistory/GetAllUserActivityTodayAsync");
            return await _repositoryFactory.SendAsync<List<UserLogInHistory>>(HttpMethod.Get, url);
        
        }

        public async Task<List<UserLogInHistory>> GetAllUserLoginHistoryAsync()
        {

            var url = Path.Combine("UserLogInHistory/GetAllUserLoginHistoryAsync");
            return await _repositoryFactory.SendAsync<List<UserLogInHistory>>(HttpMethod.Get, url);
        }

        public async Task<UserLogInHistory> GetUserLoginActivityByUserIdAsync(long userId)
        {
            var url = Path.Combine("UserLogInHistory/GetAllUserLoginHistoryAsync",userId.ToString());
            return await _repositoryFactory.SendAsync<UserLogInHistory>(HttpMethod.Get, url);
        }
    }
}
