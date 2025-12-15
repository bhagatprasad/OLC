CREATE TABLE [dbo].[OrderQueue]
(
    [Id]                        BIGINT              PRIMARY KEY IDENTITY(1,1)     NOT NULL,
    [PaymentOrderId]            BIGINT                                                NULL,
    [OrderReference]            NVARCHAR(50)        DEFAULT NEWID()                   NULL,
    [QueueStatus]               NVARCHAR(20)        DEFAULT 'Pending'                 NULL,
    -- Values: Pending, Assigned, Processing, Completed, Failed
    [Priority]                  INT                 DEFAULT 5                     NOT NULL,
    [AssignedExecutiveId]       BIGINT                                                NULL,
    [AssignedOn]                DATETIMEOFFSET                                        NULL,
    [ProcessingStartedOn]       DATETIMEOFFSET                                        NULL,
    [ProcessingCompletedOn]     DATETIMEOFFSET                                        NULL,
    [RetryCount]                INT                 DEFAULT 0                     NOT NULL,
    [MaxRetries]                INT                 DEFAULT 3                     NOT NULL,
    [FailureReason]             NVARCHAR(MAX)                                         NULL,
    [Metadata]                  NVARCHAR(MAX)                                         NULL,
    [CreatedBy]                 BIGINT                                                NULL,
    [CreatedOn]                 DATETIMEOFFSET       DEFAULT GETDATE()                NULL,
    [ModifiedBy]                BIGINT                                                NULL,
    [ModifiedOn]                DATETIMEOFFSET       DEFAULT GETDATE()                NULL,
    [IsActive]                  BIT                 DEFAULT 1                         NULL
);
