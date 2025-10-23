CREATE PROCEDURE [dbo].[uspGetBanks]

WITH RECOMPILE

AS 

 BEGIN

   SELECT 
          [Id],
          [Name],
          [Code],
          [CreatedBy],
          [CreatedOn],
          [ModifiedBy],
          [ModifiedOn],
          [IsActive]
     FROM 
          [dbo].[Bank] 
     ORDER BY [NAME] ASC
END
