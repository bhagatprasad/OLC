CREATE PROCEDURE [dbo].[uspDeleteUserBankAccount]
	
(
    @Id BIGINT
)
AS
BEGIN
   
    DELETE FROM [dbo].[UserBankAccount]
    WHERE [Id] = @Id;
END
