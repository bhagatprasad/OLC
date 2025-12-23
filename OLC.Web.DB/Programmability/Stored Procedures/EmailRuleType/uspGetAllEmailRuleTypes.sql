CREATE PROCEDURE [dbo].[uspGetAllEmailRuleTypes]
AS
BEGIN
    SELECT
            Id,
            RuleCode,
            RuleName,
		    Description,		
		    CreatedBy,
		    CreatedOn,	
		    ModifiedBy,
		    ModifiedOn,		
		    IsActive

    FROM    [dbo].[EmailRuleType]
    WHERE   IsActive = 1
    ORDER BY  RuleName;
END
