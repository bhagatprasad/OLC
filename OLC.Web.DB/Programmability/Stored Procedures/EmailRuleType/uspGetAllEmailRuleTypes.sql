CREATE PROCEDURE [dbo].[uspGetAllEmailRuleTypes]
AS
BEGIN
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
    WHERE IsActive = 1
    ORDER BY RuleName;
END
