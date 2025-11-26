CREATE PROCEDURE [dbo].[uspUpdateTransationTypes]
(
    @id         BIGINT,
    @name       VARCHAR(50),
    @code       VARCHAR(50),
    @createdBy  BIGINT,
    @modifiedBy BIGINT,
    @isActive   BIT
)
AS
BEGIN
    UPDATE dbo.[TransactionType]
    SET
        name = @name,
        code = @code,
        ModifiedBy = @modifiedBy,
        ModifiedOn = GETDATE(),
        IsActive = @isActive
    WHERE Id = @id
END

                            

