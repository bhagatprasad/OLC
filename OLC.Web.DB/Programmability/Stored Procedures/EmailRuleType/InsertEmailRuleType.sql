CREATE PROCEDURE  [dbo].[uspInsertEmailRuleType]
(
    @ruleCode     NVARCHAR(50),
    @ruleName     NVARCHAR(100),
    @description  NVARCHAR(255),
    @createdBy    BIGINT
)
AS
BEGIN
    INSERT INTO [dbo].[EmailRuleType]
    (
        RuleCode,
        RuleName,
        Description,
        CreatedBy
    )
    VALUES
    (
        @ruleCode,
        @ruleName,
        @description,
        @createdBy
    );
END
