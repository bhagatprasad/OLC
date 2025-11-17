
CREATE PROCEDURE [dbo].[uspGetAllServiceRequestReplies]
AS 
  BEGIN
      SELECT
					
			 TicketId		
			,ReplierId	
			,Message		
			,Status	    
			,IsInternal	
			,CreatedBy
			,CreatedOn	
			,ModifiedBy	
			,ModifiedOn	
			,IsActive	
	FROM [dbo].[ServiceRequestReplies]
	ORDER BY  CreatedOn ASC;  
END;