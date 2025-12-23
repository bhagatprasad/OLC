CREATE PROCEDURE [dbo].[uspDeleteEmailRuleType]
(
    @id BIGINT
)
AS
BEGIN
    UPDATE [dbo].[EmailRuleType]
    SET    IsActive = 0
    WHERE  Id = @id;
END
