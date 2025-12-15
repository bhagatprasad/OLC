CREATE TABLE [dbo].[QueueProcessingHistory]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
       
    [OrderQueueId] BIGINT NOT NULL,
    [ExecutiveId] BIGINT NULL,
        
    [FromStatus] NVARCHAR(20) NULL,
    [ToStatus] NVARCHAR(20) NOT NULL,

    -- Examples: 'Assigned', 'ProcessingStarted', 'Completed', 'Failed', 'Retried'
    [Action] NVARCHAR(50) NOT NULL,

    [ActionTimestamp] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),

    [Details] NVARCHAR(MAX) NULL,

    [IPAddress] NVARCHAR(45) NULL
);


