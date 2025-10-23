CREATE PROCEDURE [dbo].[uspGetBankById]
(
  @bankId      bigint 
)
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
     WHERE 
          Id = @bankId
    END