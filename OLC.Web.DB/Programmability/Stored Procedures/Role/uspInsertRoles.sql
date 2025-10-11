CREATE PROCEDURE [dbo].[uspInsertRoles]
(
	@name			varchar(50),
	@code			varchar(50),
	@createdBy      bigint
)
AS

BEGIN

IF EXISTS (SELECT 1 FROM [dbo].[role] WHERE UPPER(Name) = UPPER(@name))
BEGIN
  update dbo.[role] set 
           name      = @name,
		   code      = @code,
		   ModifiedBy = @createdBy,
		   ModifiedOn = GETDATE()
	Where UPPER(NAME) = @name
END
ELSE
BEGIN
    INSERT INTO [dbo].[role]
                     (Name,
					  Code,
					  CreatedBy,
					  CreatedOn,
					  ModifiedBy,
					  ModifiedOn,
					  IsActive)
					  values
					  (@name,
					  @code,
					  @createdBy,
					  GETDATE(),
					  @createdBy,
					  GETDATE(),
					  1)
END
     EXEC  [dbo].[uspGetRoles]
	END
