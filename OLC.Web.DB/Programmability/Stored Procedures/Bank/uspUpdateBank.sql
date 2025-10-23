CREATE PROCEDURE [dbo].[uspUpdateBank]
(
           @id               BIGINT        
          ,@name             NVARCHAR(255)
          ,@code             NVARCHAR(255)
          ,@isActive         BIT
)
AS 
 BEGIN
  UPDATE [dbo].[Bank]
          SET
             Name = @name,
             Code = @code,
             
             ModifiedOn = GETDATE(),
             IsActive = @isActive
       WHERE ID = @id
END