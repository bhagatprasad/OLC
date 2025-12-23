
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
		    CreatedBy,
		    CreatedOn,	
		    ModifiedBy,
		    ModifiedOn,		
		    IsActive

     FROM    [dbo].[EmailRuleType]

     WHERE   Id = @id

     ORDER BY RuleName;
END