CREATE PROCEDURE [dbo].[uspDeleteTransactionTypes]
    @id BIGINT
AS
BEGIN
    UPDATE dbo.[TransactionType]
    SET IsActive = 0
    WHERE Id = @id
END

