CREATE PROCEDURE [dbo].[uspGetAddressType]
(
		@createdBy		bigint
)
AS
BEGIN
		SELECT * FROM [dbo].[AddressType]
	WHERE
		CreatedBy		=@createdBy
END