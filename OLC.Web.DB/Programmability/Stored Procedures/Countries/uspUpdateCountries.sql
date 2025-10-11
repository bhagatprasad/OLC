CREATE PROCEDURE [dbo].[uspUpdateCountries]
	(
        
         @countryId bigint,
         @name varchar(50),
         @code varchar(50),
         @modifiedOn datetimeoffset,
         @modifiedBy bigint,
         @isActive bit
)


WITH RECOMPILE

AS

BEGIN

    UPDATE [dbo].[Country]
    SET
        
        Name = @name,
        Code = @code,
        ModifiedOn = @modifiedOn,
        ModifiedBy = @modifiedBy,
        IsActive = @isActive
    WHERE Id = @countryId
END
