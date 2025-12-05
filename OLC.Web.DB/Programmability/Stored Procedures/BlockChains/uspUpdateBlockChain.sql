CREATE PROCEDURE [dbo].[uspUpdateBlockChain]
(
   @id            BIGINT,
   @name          VARCHAR(MAX),
   @code          VARCHAR(MAX),
   @modifiedBy    BIGINT,
   @isActive      BIT
)

AS

BEGIN

    SET NOCOUNT ON;

    UPDATE  [dbo].[BlockChain]   

    SET
      
      Name = @name,
      Code = @code,
      ModifiedBy = @modifiedBy,
      ModifiedOn = SYSDATETIMEOFFSET(),
      IsActive = @isActive

      WHERE Id = @id

      EXEC [dbo].[uspUpdateBlockChain]

      END;

