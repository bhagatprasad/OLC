CREATE PROCEDURE [dbo].[uspGetTransactionRewardsByUserId]
(
    @UserId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        PaymentOrderId,
        UserId,
        RewardConfigurationId,
        TransactionAmount,
        RewardAmount,
        RewardRate,
        RewardStatus,
        CreditedToWalletId,
        CreditedOn,
        ExpiryDate,
        IsActive,
        CreatedBy,
        CreatedOn,
        ModifiedOn,
        ModifiedBy
    FROM [dbo].[TransactionReward]

    WHERE UserId = @UserId

    ORDER BY CreatedOn DESC;
END