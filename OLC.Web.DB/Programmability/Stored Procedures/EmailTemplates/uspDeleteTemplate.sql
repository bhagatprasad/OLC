CREATE PROCEDURE [dbo].[uspDeleteTemplate]
(
    @id    bigint
)
AS

BEGIN

SET NOCOUNT ON;

update  [dbo].[EmailTemplate]

set   

      IsActive=0,
      ModifiedOn=GETDATE()
      where  Id=@id;

  END