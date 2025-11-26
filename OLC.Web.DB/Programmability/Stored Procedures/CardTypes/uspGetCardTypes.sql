CREATE PROCEDURE [dbo].[uspGetCardTypes]
WITH RECOMPILE

AS

BEGIN

    SELECT
		   [Id]
		  ,[Name]
		  ,[Code]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedBy]
		  ,[ModifiedOn]
		  ,[IsActive]
  FROM [dbo].[CardType]

END