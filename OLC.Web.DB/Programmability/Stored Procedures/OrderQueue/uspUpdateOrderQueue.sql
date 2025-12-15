CREATE PROCEDURE [dbo].[uspUpdateOrderQueue]
(
    @OrderQueueId          BIGINT,
    @QueueStatus           NVARCHAR(20),
    @AssignedExecutiveId   BIGINT = NULL,
    @Priority              INT = NULL,
    @FailureReason         NVARCHAR(MAX) = NULL,
    @Metadata              NVARCHAR(MAX) = NULL,
    @ModifiedBy            BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[OrderQueue]
    SET
        QueueStatus = @QueueStatus,
        AssignedExecutiveId = @AssignedExecutiveId,
        Priority = ISNULL(@Priority, Priority),
        FailureReason = @FailureReason,
        Metadata = ISNULL(@Metadata, Metadata),
        ModifiedOn = GETUTCDATE(),

        AssignedOn = CASE 
                        WHEN @QueueStatus = 'Assigned' 
                        THEN GETUTCDATE() 
                        ELSE AssignedOn 
                     END,

        ProcessingStartedOn = CASE 
                                WHEN @QueueStatus = 'Processing' 
                                THEN GETUTCDATE() 
                                ELSE ProcessingStartedOn 
                              END,

        ProcessingCompletedOn = CASE 
                                   WHEN @QueueStatus IN ('Completed','Failed') 
                                   THEN GETUTCDATE() 
                                   ELSE ProcessingCompletedOn 
                                END
    WHERE Id = @OrderQueueId;
END
GO
