namespace OLC.Web.API.Models
{
    public class UserAccount : ApplicationUser
    {
        public bool? IsExternalUser { get; set; }
    }
}
