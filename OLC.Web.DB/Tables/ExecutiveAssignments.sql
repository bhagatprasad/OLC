CREATE TABLE [dbo].[ExecutiveAssignments]
(
    [Id]                    BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [ExecutiveId]           BIGINT              NOT NULL,
    [OrderQueueId]          BIGINT              NOT NULL,
    [AssignmentStatus]      NVARCHAR(20)        NOT NULL DEFAULT 'Active', -- 'Active', 'Completed', 'Cancelled'
    [AssignedAt]            DATETIMEOFFSET      NOT NULL DEFAULT GETUTCDATE(),
    [StartedAt]             DATETIMEOFFSET      NULL,
    [CompletedAt]           DATETIMEOFFSET      NULL,
    [Notes]                 NVARCHAR(MAX)       NULL
);