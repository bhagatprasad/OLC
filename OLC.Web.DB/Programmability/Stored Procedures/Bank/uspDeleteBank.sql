CREATE PROCEDURE [dbo].[uspDeleteBank]
(
          @id               BIGINT  
)

AS 

 BEGIN
    
    DELETE  FROM [dbo].[Bank] WHERE Id=@id
 
END
       
