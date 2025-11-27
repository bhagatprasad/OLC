using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class RewardConfigurationController : Controller
    {
        private readonly IRewardConfigurationServices _rewardConfigurationServices;
        private readonly INotyfService _notyfService;


        public RewardConfigurationController(IRewardConfigurationServices rewardConfiguration,
               INotyfService notyfService)
        {
            _rewardConfigurationServices = rewardConfiguration;
            _notyfService = notyfService;
        }


    }
}
