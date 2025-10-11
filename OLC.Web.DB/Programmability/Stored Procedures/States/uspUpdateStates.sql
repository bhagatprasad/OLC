CREATE PROCEDURE [dbo].[uspUpdateStates]
(
         @stateId bigint,
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

    UPDATE [dbo].[State]
    SET
        CountryId = @countryId,
        Name = @name,
        Code = @code,
        ModifiedOn = @modifiedOn,
        ModifiedBy = @modifiedBy,
        IsActive = @isActive
    WHERE Id = @stateId
END