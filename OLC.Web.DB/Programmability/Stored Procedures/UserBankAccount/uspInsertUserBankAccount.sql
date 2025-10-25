CREATE PROCEDURE [dbo].[uspInsertUserBankAccount]
(
    @UserId BIGINT,
    @AccountHolderName NVARCHAR(150),
    @BankName NVARCHAR(100),
    @BranchName NVARCHAR(100) = NULL,
    @AccountNumber NVARCHAR(MAX),
    @LastFourDigits CHAR(4),
    @AccountType NVARCHAR(50),
    @RoutingNumber NVARCHAR(MAX) = NULL,
    @IFSCCode NVARCHAR(20) = NULL,
    @SWIFTCode NVARCHAR(20) = NULL,
    @Currency NVARCHAR(10) = 'USD',
    @IsPrimary BIT = 0,
    @IsActive BIT = 1,  -- Assuming default to active
    @VerificationStatus NVARCHAR(20) = NULL,
    @VerificationAttempts INT = 0
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[UserBankAccount]
    (
        [UserId],
        [AccountHolderName],
        [BankName],
        [BranchName],
        [AccountNumber],
        [LastFourDigits],
        [AccountType],
        [RoutingNumber],
        [IFSCCode],
        [SWIFTCode],
        [Currency],
        [IsPrimary],
        [IsActive],
        [VerifiedOn],
        [VerificationStatus],
        [VerificationAttempts],
        [LastVerificationAttempt],
        [CreatedBy],
        [CreatedOn],
        [ModifiedBy],
        [ModifiedOn]
    )
    VALUES
    (
        @UserId,
        @AccountHolderName,
        @BankName,
        @BranchName,
        @AccountNumber,
        @LastFourDigits,
        @AccountType,
        @RoutingNumber,
        @IFSCCode,
        @SWIFTCode,
        @Currency,
        @IsPrimary,
        @IsActive,
        NULL,  -- VerifiedOn set to NULL as it's not verified yet
        @VerificationStatus,
        @VerificationAttempts,
        NULL,  -- LastVerificationAttempt set to NULL
        @UserId,
        SYSDATETIMEOFFSET(),
        @UserId,
        SYSDATETIMEOFFSET()
    );
END