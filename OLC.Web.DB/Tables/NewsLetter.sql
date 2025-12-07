CREATE TABLE [dbo].[NewsLetter]
(
	Id                         BIGINT                NOT NULL PRIMARY KEY IDENTITY(1,1),
    Email                      VARCHAR(255)          NULL,
    SubscribedOn               DATETIME              NULL,
    UnsubscribedOn             DATETIME               NULL,
    [CreatedBy]                BIGINT                 NULL,
	[CreatedOn]                DATETIMEOFFSET         NULL,
	[ModifiedBy]               BIGINT                 NULL,
	[ModifiedOn]               DATETIMEOFFSET         NULL,
	[IsActive]                 BIT                    NULL
)

