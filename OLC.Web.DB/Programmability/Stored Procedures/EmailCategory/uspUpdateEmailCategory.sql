CREATE PROCEDURE [dbo].[uspUpdateEmaiCategory]
(
	@id		bigint,
	@name nvarchar(200),
	@code nvarchar(10)
)
AS
BEGIN

Update [dbo].[EmailCategory]

	set
	name=@name,
	code=@code
	where id=@id
END