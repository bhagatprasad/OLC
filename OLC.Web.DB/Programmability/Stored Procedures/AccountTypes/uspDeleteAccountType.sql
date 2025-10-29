CREATE PROCEDURE [dbo].[uspDeleteAccountType]
(
    	@accountTypeId     bigint
)
AS

BEGIN 

	    UPDATE [dbo].[AccountType] set IsActive = 0
		WHERE 
		Id = @accountTypeId
END

	