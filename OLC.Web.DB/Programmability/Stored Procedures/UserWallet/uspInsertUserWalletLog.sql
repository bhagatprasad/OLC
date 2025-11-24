CREATE PROCEDURE [dbo].[uspInsertUserWalletLog]
(
    @WalletId BIGINT,
    @UserId BIGINT,
    @Amount DECIMAL(18,6),
    @TransactionType NVARCHAR(20),  -- e.g., 'Credit' or 'Debit'
    @Description NVARCHAR(255) = NULL,
    @ReferenceId NVARCHAR(20) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CreatedOn DATETIMEOFFSET = GETUTCDATE();
    DECLARE @CreatedBy BIGINT = @UserId;

    INSERT INTO [dbo].[UserWalletLog]
    (
        WalletId,
        UserId,
        Amount,
        TransactionType,
        Description,
        ReferenceId,
        Currency,
        CreatedOn,
        CreatedBy,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        @WalletId,
        @UserId,
        @Amount,
        @TransactionType,
        @Description,
        @ReferenceId,
        'INR',  -- Default currency
        @CreatedOn,
        @CreatedBy,
        @CreatedBy,
        @CreatedOn,
        1       -- IsActive (set to true/active)
    );

END