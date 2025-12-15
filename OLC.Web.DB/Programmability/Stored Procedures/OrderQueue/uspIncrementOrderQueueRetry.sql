CREATE PROCEDURE [dbo].[uspIncrementOrderQueueRetry]
(
    @OrderQueueId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[OrderQueue]
    SET
        RetryCount = RetryCount + 1,
        ModifiedOn = GETUTCDATE()
    WHERE Id = @OrderQueueId;
END
GO
