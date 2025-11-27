CREATE PROCEDURE [dbo].[uspGetAllRewardConfigurations]  
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
    FROM   
        [dbo].[RewardConfiguration]  
    ORDER BY   
        CreatedOn DESC;   
END  