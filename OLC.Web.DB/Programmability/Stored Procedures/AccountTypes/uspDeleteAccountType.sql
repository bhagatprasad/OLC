CREATE PROCEDURE [dbo].[uspDeleteAccountType]
(
    	@id     bigint
)
AS
BEGIN 
	    DELETE FROM [dbo].[AccountType] WHERE Id = @id
END

	