CREATE PROCEDURE [dbo].[uspInsertEmaiCategory]
(
	@name nvarchar(200),
	@code nvarchar(10)
)
AS
BEGIN

INSERT INTO [dbo].[EmailCategory]
(
	Name,
	Code,
	CreatedBy,
	CreatedOn,
	ModifiedBy,
	ModifiedOn,
	IsActive
)
VALUES
(
	@name,
	@code,
	-1,
	GETDATE(),
	-1,
	GETDATE(),
	1
)
END
