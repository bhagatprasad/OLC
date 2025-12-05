CREATE PROCEDURE [dbo].[uspDeleteBlockChain]
(
  @id    BIGINT
)

AS

BEGIN

  SET NOCOUNT ON;

  UPDATE  [dbo].[BlockChain]

  SET

    IsActive=0,
    ModifiedOn=SYSDATETIMEOFFSET()
    where Id=@id

    END;