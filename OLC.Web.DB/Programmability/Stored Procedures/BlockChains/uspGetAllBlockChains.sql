CREATE PROCEDURE [dbo].[uspGetAllBlockChains]

AS

BEGIN
  
  SET NOCOUNT ON

  SELECT 

      [Id]
     ,[Name]
     ,[Code]
     ,[CreatedBy]
     ,[CreatedOn]
     ,[ModifiedBy]
     ,[ModifiedOn]
     ,[IsActive]

     FROM [dbo].[BlockChain]

     ORDER BY

       CreatedOn DESC

     END