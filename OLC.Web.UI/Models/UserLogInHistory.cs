namespace OLC.Web.UI.Models
{
    public class UserLogInHistory
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string EmailAttempted { get; set; }
        public DateTimeOffset LoginAttemptTime { get; set; }
        public bool IsSuccess { get; set; }
        public bool StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public string? Notes { get; set; }
        public string? Problem { get; set; }
        public string? LoggedInFrom { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? FailedReason { get; set; }
        public int ConsecutiveFailures { get; set; }
        public int TotalFailures15Min { get; set; }
        public bool WasBlocked { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
