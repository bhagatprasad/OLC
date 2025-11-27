CREATE PROCEDURE [dbo].[uspUpdateUserWalletBalance]
(
    @WalletId           BIGINT,
    @RewardAmount       DECIMAL(18,6),
    @ModifiedBy         BIGINT,
    @TransactionType    NVARCHAR(20) = 'Credit',
    @Description        NVARCHAR(255) = NULL,
    @ReferenceId        NVARCHAR(20) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ModifiedOn DATETIMEOFFSET = GETUTCDATE();
    DECLARE @UserId BIGINT;

    -- Get UserId from wallet
    SELECT @UserId = UserId 
    FROM [dbo].[UserWallet] 
    WHERE [Id] = @WalletId;

    IF @UserId IS NULL
    BEGIN
        RAISERROR('Wallet not found.', 16, 1);
        RETURN -1;
    END

    -- Update wallet balance
    UPDATE [dbo].[UserWallet]
    SET 
        [CurrentBalance] = [CurrentBalance] + @RewardAmount,
        [TotalEarned] = [TotalEarned] + @RewardAmount,
        [ModifiedOn] = @ModifiedOn,
        [ModifiedBy] = @ModifiedBy
    WHERE [Id] = @WalletId;

    -- Insert wallet log
    EXEC [dbo].[uspInsertUserWalletLog]
        @WalletId = @WalletId,
        @UserId = @UserId,
        @Amount = @RewardAmount,
        @TransactionType = @TransactionType,
        @Description = @Description,
        @ReferenceId = @ReferenceId;
END