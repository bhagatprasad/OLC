CREATE PROCEDURE [dbo].[uspUpdateEmailRuleType]
(
    @id INT,
    @ruleName NVARCHAR(100),
    @description NVARCHAR(255),
    @isActive BIT,
    @modifiedBy NVARCHAR(50)
)
AS
BEGIN
    UPDATE [dbo].[EmailRuleType]
    SET
        RuleName = @ruleName,
        Description = @description,
        IsActive = @isActive,
        ModifiedOn = GETDATE(),
        ModifiedBy = @modifiedBy
    WHERE Id = @Id;
END
