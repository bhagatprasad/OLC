CREATE PROCEDURE [dbo].[uspGetBankById]
(
       @id BIGINT 
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
     FROM [dbo].[Bank] WHERE Id=@id
END