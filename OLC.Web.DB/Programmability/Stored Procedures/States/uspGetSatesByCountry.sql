CREATE PROCEDURE [dbo].[uspGetSatesByCountry]
(
	@countryId			bigint
)
AS
BEGIN
	SELECT
	   [Id]
	  ,[CountryId]
	  ,[Name]
	  ,[Code]
	  ,[CreatedBy]
	  ,[CreatedOn]
	  ,[ModifiedBy]
	  ,[ModifiedOn]
	  ,[IsActive]
  FROM [dbo].[State]
	 WHERE 
		CountryId		=@countryId
END
