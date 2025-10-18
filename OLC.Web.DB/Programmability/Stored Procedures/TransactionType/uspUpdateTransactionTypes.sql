CREATE PROCEDURE [dbo].[uspUpdateTransactionTypes]
(    
    @id        bigint,
    @name      varchar(50),
    @code      varchar(50),
    @createdBy bigint,
    @modifiedBy  bigint,
    @isActive   bit
)

AS

BEGIN

update dbo.[TransactionType] set 
                            
                            name=@name,
                            code=@code,
                            ModifiedBy=@modifiedBy,
                            ModifiedOn=GETDATE(),
                            IsActive=@isActive

                            Where Id=@id

                            EXEC dbo.[uspUpdateTransactionTypes]

END

                            
