CREATE PROCEDURE [dbo].[uspInsertCountries]
(
	@name                   varchar(255) null,
    @code                   varchar(255) null,   
    @createdBy              bigint null
	
 )
 WITH RECOMPILE
 AS 
 BEGIN
     INSERT INTO [dbo].[Country] 
 (                                       
         Name,
         Code,
         CreatedBy,
         CreatedOn,
         ModifiedBy,
         ModifiedOn,
         IsActive
 )  
  VALUES
 (                                     
                                      
          @name
         ,@code
         ,@createdBy
         ,GETDATE()
         ,@createdBy
         ,GETDATE()
         ,1
)
END
