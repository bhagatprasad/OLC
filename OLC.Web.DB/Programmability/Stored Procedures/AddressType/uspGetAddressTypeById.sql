CREATE PROCEDURE [dbo].[uspGetAddressTypeById]
(
		@id			bigint
)
AS
BEGIN
		SELECT * FROM [dbo].[AddressType]
	WHERE 
		Id			=@id
END