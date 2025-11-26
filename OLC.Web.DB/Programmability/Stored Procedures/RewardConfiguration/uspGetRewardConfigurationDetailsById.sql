CREATE PROCEDURE [dbo].[uspGetRewardConfigurationDetailsById]  
(  
    @Id BIGINT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    SELECT   
        Id,  
        RewardName,  
        RewardType,  
        RewardValue,  
        MinimumTransactionAmount,  
        MaximumReward,  
        IsActive,  
        ValidFrom,  
        ValidTo,  
        CreatedBy,  
        CreatedOn,  
        ModifiedBy,  
        ModifiedOn  
    FROM [dbo].[RewardConfiguration]   
    WHERE Id = @Id;  
END  
