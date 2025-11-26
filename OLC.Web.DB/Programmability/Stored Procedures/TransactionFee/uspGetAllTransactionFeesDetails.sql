CREATE PROCEDURE [dbo].[uspGetAllTransactionFeesDetails]
AS
BEGIN
	SELECT 
			[Id],
			[Name],
			[Code],
			[Price],
			[CreatedBy],
			[CreatedOn],
			[ModifiedBy],
			[ModifiedOn],
			[IsActive]
		FROM 
			[dbo].[TransactionFee] 
	END
