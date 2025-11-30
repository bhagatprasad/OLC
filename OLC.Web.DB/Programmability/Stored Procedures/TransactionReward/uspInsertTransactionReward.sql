CREATE  PROCEDURE [dbo].[uspInsertTransactionReward]
(
    @PaymentOrderId BIGINT,
    @UserId BIGINT,
    @TransactionAmount DECIMAL(18,6)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RewardConfigurationId BIGINT = 6;
    DECLARE @RewardRate DECIMAL(18,6) = 0.02;  -- 2%

    -- Calculations
    DECLARE @RewardAmount DECIMAL(18,6) = @TransactionAmount * @RewardRate;
    DECLARE @RewardStatus NVARCHAR(20) = 'credited';
    DECLARE @CreditedToWalletId BIGINT;
    DECLARE @CreditedOn DATETIMEOFFSET = GETUTCDATE();
    DECLARE @ExpiryDate DATETIMEOFFSET = DATEADD(MONTH, 12, @CreditedOn);
    DECLARE @IsActive BIT = 1;
    DECLARE @CreatedBy BIGINT = @UserId;
    DECLARE @CreatedOn DATETIMEOFFSET = @CreditedOn;
    DECLARE @ModifiedOn DATETIMEOFFSET = @CreditedOn;
    DECLARE @ModifiedBy BIGINT = @UserId;
    DECLARE @ReferanceNumber varchar(max);
    
    SET @ReferanceNumber = NEWID();

    SELECT @CreditedToWalletId = [Id]
    FROM [dbo].[UserWallet]
    WHERE [UserId] = @UserId;

    IF @CreditedToWalletId IS NULL
    BEGIN
        RAISERROR('No wallet found for the specified UserId.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[TransactionReward]
    (
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
    )
    VALUES
    (
        @PaymentOrderId,
        @UserId,
        @RewardConfigurationId,
        @TransactionAmount,
        @RewardAmount,
        @RewardRate,
        @RewardStatus,
        @CreditedToWalletId,
        @CreditedOn,
        @ExpiryDate,
        @IsActive,
        @CreatedBy,
        @CreatedOn,
        @ModifiedOn,
        @ModifiedBy
    );

    exec [dbo].[uspUpdateUserWalletBalance] @CreditedToWalletId,@RewardAmount,@UserId,'Credit','Rawards balance credited', @ReferanceNumber
END