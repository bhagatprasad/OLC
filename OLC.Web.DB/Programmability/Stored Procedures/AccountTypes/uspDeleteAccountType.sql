CREATE PROCEDURE [dbo].[uspDeleteAccountType]
(
    	@accountTypeId     bigint
)
AS

BEGIN 

	    DELETE FROM [dbo].[AccountType] 
		WHERE 
		Id = @accountTypeId
END

	