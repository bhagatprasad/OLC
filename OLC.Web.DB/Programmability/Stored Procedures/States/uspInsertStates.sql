CREATE PROCEDURE [dbo].[uspInsertStates]
(
         @countryId bigint null,
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

     INSERT INTO [dbo].[State] (
                                        CountryId,
                                        Name,
                                        Code,
                                        CreatedOn,
                                        CreatedBy,
                                        ModifiedOn,
                                        ModifiedBy,
                                        IsActive
                                      )  VALUES
                                      (
                                      @countryId,
                                      @name,
                                      @code,
                                      @createdOn,
                                      @createdBy,
                                      @modifiedOn,
                                      @modifiedBy,
                                      @isActive)


END