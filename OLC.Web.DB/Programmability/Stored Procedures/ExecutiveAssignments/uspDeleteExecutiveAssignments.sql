CREATE PROCEDURE [dbo].[uspDeleteExecutiveAssignments]
(
    @executiveId         BIGINT    
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ExecutiveAssignments]
    SET
        IsActive   = 0,     
        ModifiedOn = SYSDATETIMEOFFSET()
    WHERE Id = @executiveId
      AND IsActive = 1;
END
GO

