CREATE PROCEDURE [dbo].[uspGetAccountTypes]
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
END