CREATE PROCEDURE [dbo].[uspInsertCities]
	(
         @cityId   bigint  null,
	     @countryId bigint,
	     @stateId bigint,
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

     INSERT INTO [dbo].[City] (
                                        CountryId,
                                        StateId,
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
                                      @StateId,
                                      @name,
                                      @code,
                                      @createdOn,
                                      @createdBy,
                                      @modifiedOn,
                                      @modifiedBy,
                                      @isActive)


END