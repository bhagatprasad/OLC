CREATE PROCEDURE  [dbo].[uspInsertExecutiveAssignments]
(
    @userId            BIGINT,
    @paymentOrderId    BIGINT,
    @executiveId       BIGINT,
    @orderQueueId      BIGINT,
    @assignedBy        BIGINT = NULL,
    @startedAt         DATETIMEOFFSET = NULL,
    @completedAt       DATETIMEOFFSET = NULL,
    @notes             NVARCHAR(MAX) = NULL,
    @createdBy         BIGINT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[ExecutiveAssignments]
    (
        UserId,
        PaymentOrderId,
        ExecutiveId,
        OrderQueueId,
        AssignedBy,
        StartedAt,
        CompletedAt,
        Notes,
        CreatedBy
    )
    VALUES
    (
        @userId,
        @paymentOrderId,
        @executiveId,
        @orderQueueId,
        @assignedBy,
        @startedAt,
        @completedAt,
        @notes,
        @createdBy
    );
    
END
GO

