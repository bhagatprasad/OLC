CREATE PROCEDURE [dbo].[uspDeleteTransationTypes]
	(@id bigint)

	AS 

	BEGIN

	update dbo.[TransactionType]
	       set IsActive=0
		   where Id=@id
   END
