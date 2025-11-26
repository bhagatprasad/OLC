CREATE PROCEDURE [dbo].[uspSaveRewardConfiguration]  
(  
    
    @RewardName                 NVARCHAR(100),  
    @RewardType                 NVARCHAR(20),  
    @RewardValue                DECIMAL(18,6),  
    @MinimumTransactionAmount   DECIMAL(18,6),  
    @MaximumReward              DECIMAL(18,6) = NULL,  
    @IsActive                   BIT = 1,  
    @ValidFrom                  DATETIMEOFFSET = NULL,  
    @ValidTo                    DATETIMEOFFSET = NULL,  
    @CreatedBy                  BIGINT = NULL,  
    @ModifiedBy                 BIGINT = NULL  
)  
AS  
  BEGIN  
      SET NOCOUNT ON;  
           
        IF (@ValidFrom IS NULL)  
            SET @ValidFrom = SYSDATETIMEOFFSET();  
  
 INSERT INTO [dbo].[RewardConfiguration]  
(       
                               
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
        ModifiedOn,  
        ModifiedBy  
)  
    VALUES  
(       
        @RewardName,  
        @RewardType,  
        @RewardValue,  
        @MinimumTransactionAmount,  
        @MaximumReward,  
        @IsActive,  
        @ValidFrom,  
        @ValidTo,  
        @CreatedBy,  
        SYSDATETIMEOFFSET(),  
        SYSDATETIMEOFFSET(),  
        @ModifiedBy  
);  
  
    SELECT SCOPE_IDENTITY() AS SavedId;  
END  