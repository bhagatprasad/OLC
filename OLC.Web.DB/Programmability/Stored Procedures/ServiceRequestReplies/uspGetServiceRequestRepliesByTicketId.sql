CREATE PROCEDURE [dbo].[uspGetServiceRequestRepliesByTicketId]
	@TicketId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TicketId,
        ReplierId,
        Message,
        Status,
        IsInternal,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    FROM [dbo].[ServiceRequestReplies]
    WHERE TicketId = @TicketId
      AND IsActive = 1
    ORDER BY CreatedOn ASC;  
END;
GO