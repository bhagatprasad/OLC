CREATE PROCEDURE [dbo].[uspDeleteEmailRuleType]
(
    @id INT
)
AS
BEGIN
    UPDATE [dbo].[EmailRuleType]
    SET IsActive = 0
    WHERE Id = @id;
END
