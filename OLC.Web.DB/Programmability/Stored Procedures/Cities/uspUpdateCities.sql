CREATE PROCEDURE [dbo].[uspUpdateCities]
	(
         @cityId bigint,
         @countryId bigint,
         @stateId bigint,
         @name varchar(50),
         @code varchar(50),
         @modifiedOn datetimeoffset,
         @modifiedBy bigint,
         @isActive bit
)


WITH RECOMPILE

AS

BEGIN

    UPDATE [dbo].[City]
    SET
       
        CountryId = @countryId,
        StateId=@stateId,
        Name = @name,
        Code = @code,
        ModifiedOn = @modifiedOn,
        ModifiedBy = @modifiedBy,
        IsActive = @isActive
    WHERE Id = @cityId
END