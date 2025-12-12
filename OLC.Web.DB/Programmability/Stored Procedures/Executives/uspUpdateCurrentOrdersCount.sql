CREATE PROCEDURE [dbo].[uspUpdateCurrentOrdersCount]
(
	  @userId bigint,
	  @currentOrderCount int
 )
 as 
 begin  
   UPDATE [dbo].[Executives] 
   set 
		 CurrentOrderCount= @currentOrderCount 
		 WHERE UserId= @userId
END