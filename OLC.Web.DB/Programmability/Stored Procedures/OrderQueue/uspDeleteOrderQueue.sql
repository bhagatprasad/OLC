CREATE PROCEDURE [dbo].[uspDeleteOrderQueue]
(
    @OrderQueueId BIGINT,
    @Reason NVARCHAR(255),
    @ModifiedBy BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[OrderQueue]
    SET
        QueueStatus = 'Failed',
        FailureReason = @Reason,
        ModifiedOn = GETUTCDATE()
    WHERE Id = @OrderQueueId;
END
GO
