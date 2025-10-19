CREATE PROCEDURE [dbo].[uspInsertTransationTypes]
	 (
   @id          bigint       null,
   @name        varchar(50)  null,
   @code        varchar(50)  null,
   @createdby   bigint       null,
   @createdOn   datetimeoffset  null,
   @modifiedBy  bigint          null,
   @modifiedOn  datetimeoffset  null,
   @isActive    bit             null
)
WITH RECOMPILE
 AS 
 BEGIN
 INSERT INTO [dbo].[TransactionType] ( Id,    
                                       Name,
                                       Code,
                                       CreatedBy,
                                       CreatedOn,
                                       ModifiedBy,
                                       ModifiedOn,
                                       IsActive
                                       ) VALUES    
                                       ( @id,
                                        @name,
                                        @code,
                                        @createdby,
                                        @createdOn,
                                        @modifiedBy,
                                        @modifiedOn,
                                        @isActive )

      END

