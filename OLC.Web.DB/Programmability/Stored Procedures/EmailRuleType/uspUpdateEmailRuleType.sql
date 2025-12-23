CREATE PROCEDURE [dbo].[uspUpdateEmailRuleType]
(
        @id             BIGINT,
		@ruleCode       NVARCHAR(50),
        @ruleName       NVARCHAR(100),
        @description    NVARCHAR(255),    
        @modifiedBy     BIGINT,
	    @isActive       BIT
)
AS
BEGIN
    UPDATE [dbo].[EmailRuleType]
    SET
	    RuleCode      = @ruleCode,
        RuleName      = @ruleName,
        Description   = @description,        
        ModifiedOn    = GETDATE(),
        ModifiedBy    = @modifiedBy,
		IsActive      = @isActive
    WHERE Id = @Id;
END