using Microsoft.AspNetCore.Authentication.Cookies;
using OLC.Web.UI.Models;
using System.Security.Claims;

namespace OLC.Web.UI.Helper
{
    public class UserPrincipal
    {
        public static ClaimsPrincipal GenarateUserPrincipal(ApplicationUser user)
        {

            var claims = new List<Claim>
          {
             new Claim("Id", user.Id.ToString()),
             new Claim("Email", user.Email),
             new Claim("FirstName", user.FirstName),
             new Claim("LastName", user.LastName),
             new Claim("RoleId", user.RoleId.ToString()),
             new Claim(ClaimTypes.Role,MapRoleIdToRoleName(user.RoleId))
          };
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return principal;
        }
        private static string MapRoleIdToRoleName(long? roleId)
        {
            // New role mappings
            switch (roleId)
            {
                case 1:
                    return "Administrator";
                case 2:
                    return "User";
                case 3:
                    return "Executive";
                case 4:
                    return "Manager";
                case 5:
                    return "Supervisor";
                case 6:
                    return "Analyst";
                case 7:
                    return "Support";
                case 8:
                    return "Viewer";
                case 9:
                    return "Editor";
                case 10:
                    return "Auditor";
                case 11:
                    return "Developer";
                case 12:
                    return "Finance";
                case 13:
                    return "HR";
                case 14:
                    return "Sales";
                case 15:
                    return "Marketing";
                default:
                    return "Unknown";
            }
        }
    }
}
