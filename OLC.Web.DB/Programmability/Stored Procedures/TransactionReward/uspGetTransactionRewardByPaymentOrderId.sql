CREATE PROCEDURE [dbo].[GetTransactionRewardByPaymentOrderId]
(
    @PaymentOrderId BIGINT
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

    WHERE PaymentOrderId = @PaymentOrderId

    ORDER BY CreatedOn DESC;
END
