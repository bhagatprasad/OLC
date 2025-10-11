CREATE PROCEDURE [dbo].[uspUpdateRoles]
(
		@id				bigint,
		@name			varchar(50),
		@code			varchar(50),
		@modifiedBy		bigint,
		@isActive		bit
)

AS
	BEGIN
update dbo.[role] set 
           name      = @name,
		   code      = @code,
		   ModifiedBy = @modifiedBy,
		   ModifiedOn = GETDATE(),
		   IsActive = @isActive
	Where Id=@id

	EXEC  dbo.[uspGetRoles]
END