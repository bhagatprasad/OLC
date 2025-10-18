CREATE PROCEDURE [dbo].[uspGetAccountTypeById]
(
       @id       bigint
)
AS
BEGIN
       SELECT * FROM [dbo].[AccountType] 
  WHERE  
       Id          =@id

END