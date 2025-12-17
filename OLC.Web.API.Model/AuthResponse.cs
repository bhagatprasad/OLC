namespace OLC.Web.API.Models
{
    public class AuthResponse
    {
        public string? Email { get; set; }
        public bool ValidUser { get; set; }
        public bool ValidPassword { get; set; }
        public bool IsActive { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
