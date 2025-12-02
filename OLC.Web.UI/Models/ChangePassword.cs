using System.ComponentModel.DataAnnotations;

namespace OLC.Web.UI.Models
{
    public class ChangePassword
    {
        public long? UserId { get; set; }

        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Display(Name = "Conform Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password not matching.")]
        public string ConformPassword { get; set; }
    }
}
