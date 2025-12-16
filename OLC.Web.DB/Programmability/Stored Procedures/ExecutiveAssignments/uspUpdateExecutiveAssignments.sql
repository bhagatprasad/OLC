CREATE PROCEDURE [dbo].[uspUpdateExecutiveAssignments]
(
    @ExecutiveAssignmentId BIGINT,
    @AssignmentStatus      NVARCHAR(20),
    @Notes                 NVARCHAR(MAX) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

   
    UPDATE [dbo].[ExecutiveAssignments]
    SET
        AssignmentStatus = @AssignmentStatus,

        StartedAt =
            CASE 
                WHEN @AssignmentStatus = 'Active'
                     AND StartedAt IS NULL
                THEN StartedAt  -- no change
                WHEN @AssignmentStatus = 'Completed'
                     AND StartedAt IS NULL
                THEN StartedAt
                WHEN @AssignmentStatus = 'Cancelled'
                     AND StartedAt IS NULL
                THEN StartedAt
                WHEN @AssignmentStatus = 'InProgress'
                     AND StartedAt IS NULL
                THEN GETUTCDATE()
                ELSE StartedAt
            END,

        CompletedAt =
            CASE 
                WHEN @AssignmentStatus IN ('Completed', 'Cancelled')
                THEN GETUTCDATE()
                ELSE CompletedAt
            END,

        Notes = @Notes
    WHERE Id = @ExecutiveAssignmentId;

    -- Return updated record
    SELECT *
    FROM dbo.ExecutiveAssignments
    WHERE Id = @ExecutiveAssignmentId;
END;

