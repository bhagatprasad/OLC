﻿using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IUserService
    {
        Task<List<UserAccount>> GetUserAccountsAsync();
    }
}
