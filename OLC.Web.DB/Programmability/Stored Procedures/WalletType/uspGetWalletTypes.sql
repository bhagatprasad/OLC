CREATE PROCEDURE [dbo].[uspGetWalletTypes]

AS

BEGIN

  SET NOCOUNT ON;  

SELECT 
    
     [Id]
    ,[Name]
    ,[Code]
    ,[CreatedBy]
    ,[CreatedOn]
    ,[ModifiedBy]
    ,[ModifiedOn]
    ,[IsActive]

  FROM [dbo].[WalletType]
    
  WHERE IsActive=1

  ORDER BY CreatedOn DESC;
END
