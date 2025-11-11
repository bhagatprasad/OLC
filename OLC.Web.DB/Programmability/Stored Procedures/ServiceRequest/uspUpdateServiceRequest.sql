CREATE PROCEDURE [dbo].[uspUpdateServiceRequest]
(
	@ticketId					bigint,
	@subject					nvarchar(max),
	@message					nvarchar(max),
	@category					nvarchar(max),
	@requestReference			nvarchar(max),
	@classification				nvarchar(max),
	@priority					nvarchar(max),
	@statusId					bigint,
	@modifiedBy					bigint
)
AS
BEGIN
	UPDATE [dbo].[ServiceRequest]
		SET 
		Subject				=@subject,
		Message				=@message,
		Category			=@category,
		RequestReference	=@requestReference,
		Classification		=@classification,
		Priority			=@priority,
		StatusId			=@statusId,
		ModifiedBy			=@modifiedBy,
		ModifiedOn			=GETDATE()
	WHERE
		TicketId			=@ticketId
END
