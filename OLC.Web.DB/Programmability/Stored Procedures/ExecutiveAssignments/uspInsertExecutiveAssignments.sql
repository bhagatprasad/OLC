CREATE PROCEDURE [dbo].[uspInsertExecutiveAssignments]
(
    @ExecutiveId      BIGINT,
    @OrderQueueId     BIGINT,
    @Notes            NVARCHAR(MAX) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[ExecutiveAssignments]
    (
        ExecutiveId,
        OrderQueueId,
        AssignmentStatus,
        AssignedAt,
        Notes
    )
    VALUES
    (
        @ExecutiveId,
        @OrderQueueId,
        'Active',          
        GETDATE(),    
        @Notes
    );

    
    SELECT SCOPE_IDENTITY() AS ExecutiveAssignmentId;
END;
