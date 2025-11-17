CREATE PROCEDURE [dbo].[uspCancelServiceRequest]
(
	@ticketId		bigint,
	@modifiedBy		bigint,
	@statusId		bigint
)
AS
BEGIN 
UPDATE [dbo].[ServiceRequest]
SET 
	StatusId		= @statusId , 
	ModifiedBy		= @modifiedBy,
	ModifiedOn		= GETDATE()
WHERE
	TicketId = @ticketId
END
	
