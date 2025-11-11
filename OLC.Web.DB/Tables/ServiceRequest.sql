CREATE TABLE [dbo].[ServiceRequest]
(
	[TicketId]                      bigint          IDENTITY(1,1) PRIMARY KEY,
    [OrderId]                       bigint                 NULL,
    [UserId]                        bigint                 NULL,
    [Subject]                       nvarchar(MAX)          NULL, 
    [Message]                       nvarchar(MAX)		   NULL,	
    [Category]                      nvarchar(MAX)		   NULL,	
    [RequestReference]              nvarchar(MAX)		   NULL,	
    [Classification]                nvarchar(MAX)          NULL,
    [Priority]                      nvarchar(MAX)		   NULL,	
    [StatusId]                      bigint				   NULL,		
    [AssignTo]                      bigint				   NULL,	
    [AssignBy]                      bigint				   NULL,	
    [AssignedOn]                    datetimeoffset		   NULL,		
    [CreatedBy]                     bigint                 NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    bigint                 NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)