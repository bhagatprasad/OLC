CREATE PROCEDURE [dbo].[uspDeleteEmailCategory]
(
@id  bigint
)
AS
BEGIN

DELETE [dbo].[EmailCategory] 
WHERE Id	=@id
END