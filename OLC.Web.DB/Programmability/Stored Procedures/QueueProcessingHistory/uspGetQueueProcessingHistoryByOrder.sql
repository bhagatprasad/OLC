CREATE PROCEDURE [dbo].[uspGetQueueProcessingHistoryByOrder]
(
    @OrderQueueId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        OrderQueueId,
        ExecutiveId,
        FromStatus,
        ToStatus,
        Action,
        ActionTimestamp,
        Details,
        IPAddress
    FROM [dbo].[QueueProcessingHistory]
    WHERE OrderQueueId = @OrderQueueId
    ORDER BY ActionTimestamp DESC;
END
GO
