CREATE PROCEDURE [dbo].[uspGetAccountTypeById]
(
       @accountTypeId     bigint
)
AS
BEGIN
       SELECT 
		   [Id]
		  ,[Name]
		  ,[Code]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedOn]
		  ,[ModifiedOn]
		  ,[ModifiedBy]
		  ,[IsActive]
	  FROM [dbo].[AccountType] 
     WHERE  
			Id  = @accountTypeId

END