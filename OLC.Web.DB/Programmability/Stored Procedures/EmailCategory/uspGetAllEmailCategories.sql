CREATE PROCEDURE [dbo].[uspGetAllEmailCategories]
AS
BEGIN
(
SELECT 
[Id],
[Name],
[Code],
[CreatedBy],
[CreatedOn],
[ModifiedBy],
[ModifiedOn],
[IsActive]
FROM [dbo].[EmailCategory]
)
END