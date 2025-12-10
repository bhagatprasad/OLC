CREATE PROCEDURE [dbo].[uspGetBlockChainById]
(
    @blockChainId     BIGINT
)

AS

BEGIN

  SET NOCOUNT ON;

  SELECT 
      
      Id,
      Name,
      Code,
      CreatedBy,
      CreatedOn,
      ModifiedBy,
      ModifiedOn,
      IsActive

      FROM [dbo].[BlockChain]

      WHERE Id=@blockChainId ;
END
