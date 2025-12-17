CREATE PROCEDURE [dbo].[uspGetOrderQueueHistoryByPaymentOrderId]
(
    @PaymentOrderId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        OrderQueueId,
        PaymentOrderId,
        ExecutiveId,
        OrderReference,
        Priority,
        AssignedOn,
        ProcessingStartedOn,
        ProcessingCompletedOn,
        QueueStatus,
        RetryCount,
        FailureReason,
        Metadata,
        Description,
        ActionType,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    FROM [dbo].[OrderQueueHistory]
    WHERE PaymentOrderId = @PaymentOrderId
      AND IsActive = 1
    ORDER BY CreatedOn DESC;
END;
GO


