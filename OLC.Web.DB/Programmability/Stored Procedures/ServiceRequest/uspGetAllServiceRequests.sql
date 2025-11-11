CREATE PROCEDURE [dbo].[uspGetAllServiceRequests]
WITH RECOMPILE

AS
BEGIN
	SELECT 
	        [TicketId],
            [OrderId],
            [UserId],
            [Subject],
            [Message],	
            [Category],	
            [RequestReference],	
            [Classification],
            [Priority],	
            [StatusId],		
            [AssignTo],	
            [AssignBy],	
            [AssignedOn],		
            [CreatedBy],
	        [CreatedOn],
	        [ModifiedBy],
	        [ModifiedOn],
	        [IsActive]
    FROM [dbo].[ServiceRequest] 
END