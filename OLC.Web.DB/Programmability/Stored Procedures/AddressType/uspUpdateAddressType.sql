CREATE PROCEDURE [dbo].[uspUpdateAddressType]
(
	 @id			bigint
	,@name			NVARCHAR(255)
	,@code			NVARCHAR(255)
	,@createdBy		bigint
)
AS
BEGIN
	UPDATE [dbo].[AddressType]
		SET
		 Name			=@name
		,Code			=@code
		,ModifiedBy		=@createdBy
		,ModifiedOn		=GETDATE()
	WHERE 
		Id				=@id
END