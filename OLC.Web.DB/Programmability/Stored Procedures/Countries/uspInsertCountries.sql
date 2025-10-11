CREATE PROCEDURE [dbo].[uspInsertCountries]
(
         
		 @name varchar(50) null,
         @code varchar(50) null,
         @createdOn datetimeoffset null,
         @createdBy bigint null,
         @modifiedOn datetimeoffset null,
		 @modifiedBy bigint null,
		 @isActive bit null
 )

 WITH RECOMPILE

 AS 

 BEGIN

     INSERT INTO [dbo].[Country] (
                                        
                                        Name,
                                        Code,
                                        CreatedOn,
                                        CreatedBy,
                                        ModifiedOn,
                                        ModifiedBy,
                                        IsActive
                                      )  VALUES
                                      (
                                      
                                      @name,
                                      @code,
                                      @createdOn,
                                      @createdBy,
                                      @modifiedOn,
                                      @modifiedBy,
                                      @isActive)


END
