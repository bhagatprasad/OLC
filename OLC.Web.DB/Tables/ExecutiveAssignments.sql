CREATE TABLE [dbo].[ExecutiveAssignments]
(
    [Id]                    BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserId]                BIGINT              NOT NULL,
	[PaymentOrderId]        BIGINT              NOT NULL,
    [ExecutiveId]           BIGINT              NOT NULL,
    [OrderQueueId]          BIGINT              NOT NULL,
    [AssignmentStatus]      NVARCHAR(20)        NOT NULL DEFAULT 'Assigned', --  'InProgress', 'Completed', 'Rejected'
    [AssignedAt]            DATETIMEOFFSET      NOT NULL DEFAULT SYSDATETIMEOFFSET(),
	[AssignedBy]            BIGINT              NULL,
    [StartedAt]             DATETIMEOFFSET      NULL,
    [CompletedAt]           DATETIMEOFFSET      NULL,
    [Notes]                 NVARCHAR(MAX)       NULL,
	[CreatedBy]             BIGINT              NULL,
    [CreatedOn]             DATETIMEOFFSET      NULL DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            BIGINT              NULL,
    [ModifiedOn]            DATETIMEOFFSET      NULL DEFAULT SYSDATETIMEOFFSET(),
    [IsActive]              BIT                 NULL DEFAULT 1
);