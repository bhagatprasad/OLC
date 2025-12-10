CREATE TABLE [dbo].[ExecutiveAssignments]
(
	[Id]                BIGINT              NOT NULL PRIMARY KEY IDENTITY(1,1),
    [UserId]            BIGINT              NOT NULL,
    [PaymentOrderId]    BIGINT              NOT NULL,
    [AssignedOn]        DATETIMEOFFSET      NULL DEFAULT GETUTCDATE(),
    [AssignedBy]        BIGINT              NULL,
    [Status]            NVARCHAR(20)        NULL DEFAULT 'Assigned', -- 'Assigned', 'InProgress', 'Completed', 'Rejected'
    [StartedOn]         DATETIMEOFFSET      NULL,
    [CompletedOn]       DATETIMEOFFSET      NULL,
    [Notes]             NVARCHAR(500)       NULL,   
    [CreatedBy]         BIGINT              NULL,
    [CreatedOn]         DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [ModifiedBy]        BIGINT              NULL,
    [ModifiedOn]        DATETIMEOFFSET      NULL DEFAULT GETDATE(),
    [IsActive]          BIT                 NULL DEFAULT 1
)
