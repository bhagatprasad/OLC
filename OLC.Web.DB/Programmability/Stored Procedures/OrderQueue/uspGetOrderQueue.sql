CREATE PROCEDURE [dbo].[uspGetOrderQueue]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        PaymentOrderId,
        OrderReference,
        QueueStatus,
        Priority,
        AssignedExecutiveId,
        AssignedOn,
        ProcessingStartedOn,
        ProcessingCompletedOn,
        RetryCount,
        MaxRetries,
        FailureReason,
        Metadata,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    FROM [dbo].[OrderQueue]
    WHERE QueueStatus = 'Pending'
END;