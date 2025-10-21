namespace OLC.Web.UI.Models
{
    public class UserAccount : ApplicationUser
    {
        public bool? IsExternalUser { get; set; }
    }
}
