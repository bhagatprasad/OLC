CREATE PROCEDURE [dbo].[uspDeleteCardType]
(
		@id			bigint
)
AS
BEGIN
		DELETE FROM [dbo].[CardType]
	WHERE 
		Id			=@id
END
