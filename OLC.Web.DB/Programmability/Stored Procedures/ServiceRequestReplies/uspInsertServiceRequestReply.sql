CREATE PROCEDURE [dbo].[uspInsertServiceRequestReply]
	    @ticketId     BIGINT,
        @replierId    BIGINT,
        @message      NVARCHAR(MAX),
        @status       VARCHAR(50),
        @isInternal   BIT = 0,
        @createdBy    BIGINT,
        @modifiedBy   BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;

   INSERT INTO [dbo].[ServiceRequestReplies]
    (
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
    )
    VALUES
    (
        @ticketId,
        @replierId,
        @message,
        @status,
        @isInternal,
        @createdBy,
        GETDATE(),
        @modifiedBy,
        GETDATE(),
        1
    );

END;
