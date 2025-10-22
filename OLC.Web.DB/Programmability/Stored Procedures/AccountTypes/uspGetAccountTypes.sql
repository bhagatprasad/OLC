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
		 ,[IsActive]
	  FROM [dbo].[AccountType] 
END