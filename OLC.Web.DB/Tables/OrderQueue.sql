CREATE TABLE [dbo].[OrderQueue]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,

    [PaymentOrderId] BIGINT NOT NULL,
    [OrderReference] NVARCHAR(50) NOT NULL UNIQUE,

    [QueueStatus] NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    -- Values: Pending, Assigned, Processing, Completed, Failed

    [Priority] INT NOT NULL DEFAULT 5,

    [AssignedExecutiveId] BIGINT NULL,

    [AssignedOn] DATETIMEOFFSET NULL,
    [ProcessingStartedOn] DATETIMEOFFSET NULL,
    [ProcessingCompletedOn] DATETIMEOFFSET NULL,

    [RetryCount] INT NOT NULL DEFAULT 0,
    [MaxRetries] INT NOT NULL DEFAULT 3,

    [FailureReason] NVARCHAR(MAX) NULL,
    [Metadata] NVARCHAR(MAX) NULL,

    [CreatedOn] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedOn] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE()
);
