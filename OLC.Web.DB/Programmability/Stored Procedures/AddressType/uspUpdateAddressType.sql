CREATE PROCEDURE [dbo].[uspUpdateAddressType]
(
	 @id				bigint
	,@name				NVARCHAR(255)
	,@code				NVARCHAR(255)
	,@ModifiedBy		bigint
	,@isActive			bit
)
AS
BEGIN
	UPDATE [dbo].[AddressType]
		SET
		 Name			=@name
		,Code			=@code
		,ModifiedBy		=@ModifiedBy
		,ModifiedOn		=GETDATE()
		,IsActive		=@isActive
	WHERE 
		Id				=@id
END