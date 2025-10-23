CREATE PROCEDURE [dbo].[uspDeleteBank]
(
  @bankId      bigint
)

AS 

 BEGIN
    
    DELETE  FROM [dbo].[Bank] WHERE Id = @bankId
 
END
       
