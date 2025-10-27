CREATE PROCEDURE [dbo].[uspDeleteUserBankAccount]
	
(
    @Id INT
)
AS
BEGIN
   
    DELETE FROM [dbo].[UserBankAccount]
    WHERE [Id] = @Id;
END
