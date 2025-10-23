CREATE PROCEDURE [dbo].[uspDeleteCountry]
(
    @countryId      BIGINT
)
WITH RECOMPILE
AS
BEGIN

    DELETE FROM [dbo].[Country]
        WHERE  
            Id       =@countryId;
END