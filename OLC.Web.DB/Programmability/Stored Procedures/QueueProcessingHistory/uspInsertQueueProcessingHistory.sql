CREATE PROCEDURE [dbo].[uspInsertQueueProcessingHistory]
(
    @OrderQueueId     BIGINT,
    @ExecutiveId      BIGINT = NULL,
    @FromStatus       NVARCHAR(20) = NULL,
    @ToStatus         NVARCHAR(20),
    @Action           NVARCHAR(50),
    @Details          NVARCHAR(MAX) = NULL,
    @IPAddress        NVARCHAR(45) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[QueueProcessingHistory]
    (
        OrderQueueId,
        ExecutiveId,
        FromStatus,
        ToStatus,
        Action,
        ActionTimestamp,
        Details,
        IPAddress
    )
    VALUES
    (
        @OrderQueueId,
        @ExecutiveId,
        @FromStatus,
        @ToStatus,
        @Action,
        GETUTCDATE(),
        @Details,
        @IPAddress
    );

    SELECT SCOPE_IDENTITY() AS NewHistoryId;
END
GO
