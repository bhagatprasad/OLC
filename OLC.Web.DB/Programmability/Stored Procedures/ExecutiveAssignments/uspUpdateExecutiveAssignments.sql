CREATE PROCEDURE [dbo].[uspUpdateExecutiveAssignments]
(
    @id                 BIGINT,
    @userId             BIGINT,
    @paymentOrderId     BIGINT,
    @executiveId        BIGINT,
    @orderQueueId       BIGINT,
    @assignmentStatus   NVARCHAR(20),
    @assignedBy         BIGINT,
    @startedAt          DATETIMEOFFSET,
    @completedAt        DATETIMEOFFSET,
    @notes              NVARCHAR(MAX),
    @modifiedBy         BIGINT,
    @isActive           BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ExecutiveAssignments]
    SET
        UserId           = @userId,
        PaymentOrderId   = @paymentOrderId,
        ExecutiveId      = @executiveId,
        OrderQueueId     = @orderQueueId,
        AssignmentStatus = @assignmentStatus,
        AssignedBy       = @assignedBy,
        StartedAt        = @startedAt,
        CompletedAt      = @completedAt,
        Notes            = @notes,
        ModifiedBy       = @modifiedBy,
        ModifiedOn       = GETDATE(),
        IsActive         = @isActive
    WHERE Id = @id;        
END;
GO
