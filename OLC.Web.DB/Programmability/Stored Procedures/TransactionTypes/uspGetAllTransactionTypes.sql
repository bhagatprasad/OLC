CREATE PROCEDURE [dbo].[uspGetAllTransactionTypes]
	WITH RECOMPILE

    AS BEGIN

	SELECT

	    [Id]
	   ,[Name]
	   ,[Code]
	   ,[CreatedBy]
	   ,[CreatedOn]
	   ,[ModifiedBy]
	   ,[ModifiedOn]
	   ,[IsActive]

	   FROM [dbo].[TransactionType]

  END
