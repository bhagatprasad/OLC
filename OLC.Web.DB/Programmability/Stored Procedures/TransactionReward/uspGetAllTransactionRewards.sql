CREATE PROCEDURE [dbo].[GetAllTransactionRewards]
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

    ORDER BY CreatedOn DESC;
END