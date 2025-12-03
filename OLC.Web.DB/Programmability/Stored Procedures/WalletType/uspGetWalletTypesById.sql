CREATE PROCEDURE [dbo].[uspGetWalletTypesById]
(
   @walletTypeId   bigint
)

WITH RECOMPILE

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

   WHERE Id=@walletTypeId AND IsActive=1

   END