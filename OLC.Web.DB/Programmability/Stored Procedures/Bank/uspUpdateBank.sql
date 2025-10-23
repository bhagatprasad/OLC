CREATE PROCEDURE [dbo].[uspUpdateBank]
(
    @id               bigint        
   ,@name             nvarchar(255)
   ,@code             nvarchar(255)
   ,@isActive         bit
   ,@modifiedBy       bigint
)
AS 
 BEGIN
  UPDATE    [dbo].[Bank]
        SET
             Name       =       @name,
             Code       =       @code,
             IsActive   =       @isActive,
             ModifiedOn =       GETDATE(),
             ModifiedBy =       @modifiedBy
       WHERE 
             ID         =       @id
END