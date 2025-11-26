CREATE PROCEDURE [dbo].[uspDeleteRewardConfiguration]  
(  
    @Id BIGINT  
      
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
      
    UPDATE [dbo].[RewardConfiguration]   
    SET   
        IsActive = 0,  
        ModifiedOn = GETUTCDATE()  
    WHERE Id = @Id;  
END