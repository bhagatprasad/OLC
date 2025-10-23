CREATE PROCEDURE [dbo].[uspGetCities]
	
WITH RECOMPILE

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
		END
