CREATE PROCEDURE [dbo].[uspDeleteQueueProcessingHistory]
(
    @HistoryId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[QueueProcessingHistory]
    WHERE Id = @HistoryId;
END
GO
