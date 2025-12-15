CREATE PROCEDURE [dbo].[uspInsertOrderQueue]
(
    @PaymentOrderId        BIGINT,
    @OrderReference        NVARCHAR(50),
    @Priority              INT = 5,
    @Metadata              NVARCHAR(MAX) = NULL,
    @CreatedBy             BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[OrderQueue]
    (
        PaymentOrderId,
        OrderReference,
        QueueStatus,
        Priority,
        Metadata,
        RetryCount,
        MaxRetries,
        CreatedOn,
        ModifiedOn
    )
    VALUES
    (
        @PaymentOrderId,
        @OrderReference,
        'Pending',
        @Priority,
        @Metadata,
        0,
        3,
        GETUTCDATE(),
        GETUTCDATE()
    );

    SELECT SCOPE_IDENTITY() AS NewOrderQueueId;
END
GO
