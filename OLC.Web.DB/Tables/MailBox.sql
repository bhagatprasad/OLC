CREATE TABLE [dbo].[MailBox]
(
	-- Basic Identification
    [Id]					BIGINT PRIMARY KEY IDENTITY(1,1)	NOT NULL,
    [MessageId]				UNIQUEIDENTIFIER DEFAULT NEWID(),
    [ReferenceId]			VARCHAR(100)						NULL,
	-- Template Information
    [TemplateId]			BIGINT								NULL,
    [TemplateCode]			VARCHAR(100)						NULL,
	-- Sender Information
    [SenderEmail]			VARCHAR(MAX)						NULL,
    [SenderName]			VARCHAR(255)						NULL,
    [SenderType]			VARCHAR(50)							NULL,
	-- Recipient Information
    [RecipientEmail]		VARCHAR(MAX)						NOT NULL,
    [RecipientName]			VARCHAR(255)						NULL,
    [RecipientType]			VARCHAR(50)							NULL,
    [UserId]				BIGINT								NULL,
	-- Email Content
    [Subject]				VARCHAR(MAX)						NULL,
    [HtmlContent]			NVARCHAR(MAX)						NULL,
    [PlainContent]			NVARCHAR(MAX)						NULL,
    [Variables]				NVARCHAR(MAX)						NULL,
	-- Attachments Information
    [HasAttachments]		BIT DEFAULT 0,
    [AttachmentPaths]		NVARCHAR(MAX)						NULL,
	-- Email Metadata
    [Category]				VARCHAR(50)							NULL,
    [CampaignId]			VARCHAR(100)						NULL,	
    [Tags]					NVARCHAR(MAX)						NULL,	
	-- Status & Delivery
    [Status]				VARCHAR(50)							NOT NULL DEFAULT 'pending',
    [DeliveryStatus]		VARCHAR(50)							NULL,
    [Priority]				VARCHAR(20)							NULL DEFAULT 'normal',
	-- Scheduling
    [ScheduledFor]			DATETIMEOFFSET						NULL,
    [SentOn]				DATETIMEOFFSET						NULL,
    [DeliveredOn]			DATETIMEOFFSET						NULL,
	  -- Delivery Tracking
    [Provider]				VARCHAR(100)						NULL,
    [ProviderMessageId]		VARCHAR(255)						NULL,
    [ProviderResponse]		NVARCHAR(MAX)						NULL,
    -- Failure Information
    [FailureReason]			NVARCHAR(MAX)						NULL,
    [FailureCode]			VARCHAR(100)						NULL,
    [RetryCount]			INT DEFAULT 0,
    [MaxRetries]			INT DEFAULT 3,
    [NextRetry]				DATETIMEOFFSET						NULL,
    -- Analytics
    [OpenCount]				INT DEFAULT 0,
    [FirstOpenedOn]			DATETIMEOFFSET						NULL,
    [LastOpenedOn]			DATETIMEOFFSET						NULL,
    [ClickCount]			INT DEFAULT 0,
    [FirstClickedOn]		DATETIMEOFFSET						NULL,
    [LastClickedOn]			DATETIMEOFFSET						NULL,
    -- IP & Device Tracking
    [SenderIP]				VARCHAR(45)							NULL,
    [OpenedIP]				VARCHAR(45)							NULL,
    [ClickedIP]				VARCHAR(45)							NULL,
    [UserAgent]				NVARCHAR(MAX)						NULL,
    [DeviceType]			VARCHAR(50)							NULL, -- 'desktop', 'mobile', 'tablet'
    -- Audit & Compliance
    [CreatedBy]				BIGINT								NULL,
    [CreatedOn]				DATETIMEOFFSET						NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [UpdatedBy]				BIGINT								NULL,
    [UpdatedOn]				DATETIMEOFFSET						NULL,
    [Archived]				BIT DEFAULT 0,
    [ArchivedOn]			DATETIMEOFFSET						NULL,
    -- Unsubscribe & Preferences
    [UnsubscribeToken]		UNIQUEIDENTIFIER					NULL,
    [IsUnsubscribed]		BIT DEFAULT 0,
    [UnsubscribedOn]		DATETIMEOFFSET						NULL,
    [UnsubscribeReason]		VARCHAR(255)						NULL,
    -- Encryption & Security
    [IsEncrypted]			BIT DEFAULT 0,
    [EncryptionKey]			VARCHAR(100)						NULL,
);

