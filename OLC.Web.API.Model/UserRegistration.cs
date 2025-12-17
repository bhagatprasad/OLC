namespace OLC.Web.API.Models
{
    public class UserRegistration
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public long? RoleId { get; set; }
    }
}
