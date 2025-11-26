CREATE PROCEDURE [dbo].[uspUpdateCardType]
(
			 @id				bigint
			,@name				NVARCHAR(255)
			,@code				NVARCHAR(255)
			,@modifiedBy		bigint
			,@isActive			bit
)
AS
BEGIN

	 UPDATE [dbo].[CardType]
	 SET
		 Name				=@name
		,Code				=@code
		,ModifiedBy			=@modifiedBy
		,ModifiedOn			=GETDATE()
		,IsActive			=@isActive
	 WHERE
		 Id					=@id

END


