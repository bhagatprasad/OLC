CREATE PROCEDURE [dbo].[uspGetStates]
	WITH RECOMPILE

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

END