CREATE PROCEDURE [dbo].[uspGetCityById]
(
	    @cityId     bigint
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
				,[modifiedOn]
				,[IsActive]
		  FROM
		         [dbo].[City]
		  WHERE 
		         Id  =  @cityid
		  END
