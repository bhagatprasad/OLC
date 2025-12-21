using Microsoft.AspNetCore.Mvc;

namespace OLC.Web.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected long? CurrentUser
        {
            get
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                return !string.IsNullOrEmpty(userIdClaim?.Value) ? Convert.ToInt64(userIdClaim.Value) : 0;
            }
        }

        protected long? CurrentUserRole
        {
            get
            {
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
                return !string.IsNullOrEmpty(roleClaim.Value) ? Convert.ToInt64(roleClaim.Value) : 0;
            }
        }
    }
}
