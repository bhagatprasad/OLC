CREATE PROCEDURE [dbo].[uspDeletePriority]
	(
    @priorityId BIGINT
)
AS
BEGIN
    UPDATE [dbo].[Priority]
    SET IsActive = 0
       
    WHERE Id = @priorityId;
END
