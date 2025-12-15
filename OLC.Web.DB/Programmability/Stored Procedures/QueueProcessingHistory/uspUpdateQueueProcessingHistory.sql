CREATE PROCEDURE [dbo].[uspUpdateQueueProcessingHistory]
(
    @HistoryId  BIGINT,
    @Details    NVARCHAR(MAX) = NULL,
    @IPAddress  NVARCHAR(45) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[QueueProcessingHistory]
    SET
        Details = ISNULL(@Details, Details),
        IPAddress = ISNULL(@IPAddress, IPAddress)
    WHERE Id = @HistoryId;
END
GO
