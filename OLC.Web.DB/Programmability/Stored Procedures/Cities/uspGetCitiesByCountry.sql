CREATE PROCEDURE [dbo].[uspGetCitiesByCountry]
(
   @countryId     bigint
)
AS
  BEGIN
       SELECT
			 [Id]
			,[CountryId]
			,[StateId]
			,[Name]
			,[Code]
			,[CreatedBy]
			,[CreatedOn]
			,[ModifiedBy]
			,[ModifiedOn]
			,[IsActive]
        FROM
             [dbo].[City]
	    WHERE     
	         CountryId = @countryId
		END
