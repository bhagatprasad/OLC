CREATE PROCEDURE [dbo].[uspInsertRoles]
	(@name varchar(50),
	@code varchar(50),
	@createdBy bigint,
	@createdOn datetimeoffset,
	@modifiedBy bigint,
	@modifiedOn datetimeoffset,
	@isActive bit)
AS

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
					  @createdOn,
					  @modifiedBy,
					  @modifiedOn,
					  @isActive)

     EXEC  [dbo].[uspGetRoles]
	END
