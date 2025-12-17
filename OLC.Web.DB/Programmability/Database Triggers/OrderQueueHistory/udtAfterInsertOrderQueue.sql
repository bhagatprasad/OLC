CREATE TRIGGER [dbo].[udtAfterInsertOrderQueue]
ON [dbo].[OrderQueue]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[OrderQueueHistory]
    (
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
    )
    SELECT
        i.Id                          AS OrderQueueId,
        i.PaymentOrderId,
        i.AssignedExecutiveId         AS ExecutiveId,
        i.OrderReference,
        i.Priority,
        i.AssignedOn,
        i.ProcessingStartedOn,
        i.ProcessingCompletedOn,
        i.QueueStatus,
        i.RetryCount,
        i.FailureReason,
        i.Metadata,
        'Order created in queue'      AS Description,
        'INSERT'                      AS ActionType,
        i.CreatedBy,
        i.CreatedOn,
        i.ModifiedBy,
        i.ModifiedOn,
        i.IsActive
    FROM inserted i;
END;
GO
