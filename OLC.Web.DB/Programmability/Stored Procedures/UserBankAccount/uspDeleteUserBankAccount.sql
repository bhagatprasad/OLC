CREATE PROCEDURE [dbo].[uspDeleteUserBankAccount]
	
(
    @Id BIGINT
)
AS
BEGIN
    UPDATE [dbo].[UserBankAccount]   SET IsActive = 0 WHERE [Id] = @Id;
END
