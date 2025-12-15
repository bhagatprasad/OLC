namespace OLC.Web.API.Models
{
    public class MailBox
    {
        public long Id { get; set; }
        public Guid MessageId { get; set; }
        public string? ReferenceId { get; set; }
        public long? TemplateId { get; set; }
        public string? TemplateCode { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderName { get; set; }
        public string? SenderType { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientType { get; set; }
        public long? UserId { get; set; }
        public string? Subject { get; set; }
        public string? HtmlContent { get; set; }
        public string? PlainContent { get; set; }
        public string? Variables { get; set; }
        public bool? HasAttachments { get; set; }
        public string? AttachmentPaths { get; set; }
        public string? Category { get; set; }
        public string? CampaignId { get; set; }
        public string? Tags { get; set; }
        public string? Status { get; set; }
        public string? DeliveryStatus { get; set; }
        public string? Priority { get; set; }
        public DateTimeOffset? ScheduledFor { get; set; }
        public DateTimeOffset? SentOn { get; set; }
        public DateTimeOffset? DeliveredOn { get; set; }
        public string? Provider { get; set; }
        public string? ProviderMessageId { get; set; }
        public string? ProviderResponse { get; set; }
        public string? FailureReason { get; set; }
        public string? FailureCode { get; set; }
        public int RetryCount { get; set; }
        public int MaxRetries { get; set; }
        public DateTimeOffset? NextRetry { get; set; }
        public int OpenCount { get; set; }
        public DateTimeOffset? FirstOpenedOn { get; set; }
        public DateTimeOffset? LastOpenedOn { get; set; }
        public int ClickCount { get; set; }
        public DateTimeOffset? FirstClickedOn { get; set; }
        public DateTimeOffset? LastClickedOn { get; set; }
        public string? SenderIP { get; set; }
        public string? OpenedIP { get; set; }
        public string? ClickedIP { get; set; }
        public string? UserAgent { get; set; }
        public string? DeviceType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public bool Archived { get; set; }
        public DateTimeOffset? ArchivedOn { get; set; }
        public Guid? UnsubscribeToken { get; set; }
        public bool? IsUnsubscribed { get; set; }
        public DateTimeOffset? UnsubscribedOn { get; set; }
        public string? UnsubscribeReason { get; set; }
        public bool? IsEncrypted { get; set; }
        public string? EncryptionKey { get; set; }
    }
}
