CREATE PROCEDURE [dbo].[uspAssigningServiceRequest]
(
    @statusId     BIGINT,
    @ticketId     BIGINT,
    @assignTo     BIGINT,
    @assignedBy   BIGINT,
    @assignedOn   DATETIMEOFFSET,
    @modifiedBy   BIGINT
    
)
AS
BEGIN
    UPDATE [dbo].[ServiceRequest]
    SET
        StatusId   = @statusId,
        AssignTo   = @assignTo,
        AssignBy   = @assignedBy,
        AssignedOn = @assignedOn,
        ModifiedBy = @modifiedBy,
        ModifiedOn = GETDATE()
    WHERE
        TicketId = @ticketId;
END