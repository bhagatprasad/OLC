CREATE PROCEDURE [dbo].[uspDeleteServiceRequest]
(
	@ticketId		bigint
)
AS
BEGIN
	DELETE [dbo].[ServiceRequest]
	WHERE
		TicketId	=@ticketId
END