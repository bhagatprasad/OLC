CREATE PROCEDURE [dbo].[uspInsertBlockChain]
(
   @Name         VARCHAR(MAX),
   @Code         VARCHAR(MAX),
   @CreatedBy    BIGINT =NULL,
   @ModifiedBy   BIGINT =NULL,
   @IsActive     BIT
)
AS

BEGIN 

SET NOCOUNT ON;

  INSERT INTO [dbo].[BlockChain]

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

    (   @Name
       ,@Code
       ,@CreatedBy
       ,GETDATE()
       ,@ModifiedBy
       ,GETDATE()
       ,1)

       END