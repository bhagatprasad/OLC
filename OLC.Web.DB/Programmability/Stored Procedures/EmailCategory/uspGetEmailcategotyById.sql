CREATE PROCEDURE [dbo].[uspGetEmailcategotyById]
(
			@id		bigint
)
AS
BEGIN
	SELECT 
			[Id],
			[Name],
			[code],
			[CreatedBy],
			[CreatedOn],
			[ModifiedBy],
			[ModifiedOn],
			[IsActive]
FROM [dbo].[EmailCategory]
WHERE Id	=@id
END