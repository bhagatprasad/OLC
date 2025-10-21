CREATE PROCEDURE [dbo].[uspGetCardTypeById]
(
		@id			bigint
)
AS
BEGIN
		SELECT * FROM [dbo].[CardType]
	WHERE 
		Id			=@id
END