CREATE PROCEDURE [dbo].[uspUpdateRewardConfiguration]
(
    @Id                          BIGINT,
    @RewardName                  NVARCHAR(100),
    @RewardType                  NVARCHAR(20),
    @RewardValue                 DECIMAL(18,6),
    @MinimumTransactionAmount    DECIMAL(18,6) = 0,
    @MaximumReward               DECIMAL(18,6) = NULL,
    @ValidFrom                   DATETIMEOFFSET,
    @ValidTo                     DATETIMEOFFSET = NULL,
    @IsActive                    BIT = 1,
    @ModifiedBy                  BIGINT  
)
AS
BEGIN
    SET NOCOUNT ON;    
    UPDATE [dbo].[RewardConfiguration] 
    SET
        RewardName               = @RewardName,
        RewardType               = @RewardType,
        RewardValue              = @RewardValue,
        MinimumTransactionAmount = @MinimumTransactionAmount,
        MaximumReward            = @MaximumReward,
        ValidFrom                = @ValidFrom,
        ValidTo                  = @ValidTo,
        IsActive                 = @IsActive,
        ModifiedBy               = @ModifiedBy,
        ModifiedOn               = GETUTCDATE()

    WHERE Id = @Id;

    SELECT @Id AS UpdatedId;
END

