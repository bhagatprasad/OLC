CREATE PROCEDURE [dbo].[uspDeleteAddressType]
(
		@id			bigint
)
AS
BEGIN
		DELETE FROM [dbo].[AddressType]
	WHERE
			Id		=@id
END