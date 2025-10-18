CREATE PROCEDURE [dbo].[uspDeleteTransactionTypes]
	(@id bigint)

	AS 

	BEGIN

	update dbo.[TransactionType]
	       set IsActive=0
		   where Id=@id

	       EXEC   dbo.[uspDeleteTransactionTypes]
   END