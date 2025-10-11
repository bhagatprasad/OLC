CREATE PROCEDURE [dbo].[uspDeleteCities]
	(
	
    @cityId BIGINT
)

WITH RECOMPILE

AS

BEGIN

    DELETE FROM [dbo].[City] WHERE Id =@cityId ;

END