CREATE PROCEDURE [dbo].[uspGetAllNewsLetter]

AS

BEGIN

SET NOCOUNT ON

SELECT 

    [Id]
   ,[Email]
   ,[SubscribedOn]
   ,[UnsubscribedOn]
   ,[CreatedBy]
   ,[CreatedOn]
   ,[ModifiedBy]
   ,[ModifiedOn]
   ,[IsActive]

   FROM [dbo].[NewsLetter]

   ORDER BY

      CreatedOn  DESC

   END