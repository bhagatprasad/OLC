CREATE PROCEDURE [dbo].[uspDeleteOrderQueue]
(
    @OrderQueueId BIGINT
   
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[OrderQueue]
    SET
        QueueStatus = 'Failed',
        ModifiedOn = GETUTCDATE()
    WHERE Id = @OrderQueueId;
END
GO
