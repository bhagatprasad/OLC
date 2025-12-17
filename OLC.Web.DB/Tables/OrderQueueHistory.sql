CREATE TABLE [dbo].[OrderQueueHistory]
(
    [Id]                       BIGINT IDENTITY(1,1) PRIMARY KEY,  
    [OrderQueueId]             BIGINT               NOT NULL,
    [PaymentOrderId]           BIGINT               NULL,
	[ExecutiveId]              BIGINT               NULL,
    [OrderReference]           NVARCHAR(50)         NULL, 
	[Priority]                 INT                  DEFAULT 5                     NOT NULL,
	[AssignedOn]               DATETIMEOFFSET       NULL,
    [ProcessingStartedOn]      DATETIMEOFFSET       NULL,
    [ProcessingCompletedOn]    DATETIMEOFFSET       NULL,
    [QueueStatus]              NVARCHAR(20)         NULL,
	[RetryCount]               INT                  NULL,	
	[FailureReason]            NVARCHAR(MAX)        NULL,    
    [Metadata]                 NVARCHAR(MAX)        NULL,
    [Description]              NVARCHAR(255)        NULL,
	[ActionType]               NVARCHAR(30)         NOT NULL, -- INSERT, UPDATE, STATUS_CHANGE, ASSIGN, RETRY, COMPLETE, FAIL 
    [CreatedBy]                BIGINT               NULL,
    [CreatedOn]                DATETIMEOFFSET       NULL DEFAULT GETDATE(),
    [ModifiedOn]               DATETIMEOFFSET       NULL DEFAULT GETDATE(),
    [ModifiedBy]               BIGINT               NULL,
    [IsActive]                 BIT                  NOT NULL DEFAULT 1  
);
