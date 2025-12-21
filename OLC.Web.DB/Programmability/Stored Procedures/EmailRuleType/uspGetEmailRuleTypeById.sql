
CREATE PROCEDURE [dbo].[uspGetEmailRuleTypeById]
(
   @id BIGINT
)
AS
BEGIN
   SET NOCOUNT ON;
    SELECT
        Id,
        RuleCode,
        RuleName,
		Description,
		IsActive,
		CreatedOn,
		CreatedBy ,
		ModifiedOn,
		ModifiedBy
    FROM [dbo].[EmailRuleType]
    WHERE Id = @id
    ORDER BY RuleName;
END