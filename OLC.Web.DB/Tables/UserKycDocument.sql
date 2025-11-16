CREATE TABLE [dbo].[UserKycDocument]
(
    [Id]                    bigint              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [UserId]                bigint              NOT NULL,
    [DocumentType]          varchar(100)        NOT NULL,
    [DocumentNumber]        varchar(100)        NULL, 
    [DocumentFilePath]      nvarchar(500)       NULL,    
    [DocumentFileData]      varbinary(max)      NULL,    
    [VerificationStatus]    varchar(50)         NOT NULL DEFAULT 'Pending', 
    [VerifiedOn]            datetimeoffset      NULL,
    [VerifiedBy]            bigint              NULL,    
    [RejectionReason]       nvarchar(500)       NULL,
    [ExpiryDate]            date                NULL,    
    [CreatedBy]             bigint              NULL,
	[CreatedOn]             datetimeoffset      NULL,
	[ModifiedBy]            bigint              NULL,
	[ModifiedOn]            datetimeoffset      NULL,
	[IsActive]              bit                 NULL
);
