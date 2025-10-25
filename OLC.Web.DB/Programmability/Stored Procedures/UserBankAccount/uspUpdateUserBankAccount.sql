CREATE PROCEDURE [dbo].[uspUpdateUserBankAccount]
(
    @Id BIGINT,
    @UserId BIGINT,
    @AccountHolderName NVARCHAR(150),
    @BankName NVARCHAR(100),
    @BranchName NVARCHAR(100) = NULL,
    @AccountNumber NVARCHAR(MAX) = NULL,
    @LastFourDigits CHAR(4),
    @AccountType NVARCHAR(50),
    @RoutingNumber NVARCHAR(MAX) = NULL,
    @IFSCCode NVARCHAR(20) = NULL,
    @SWIFTCode NVARCHAR(20) = NULL,
    @Currency NVARCHAR(10) = 'USD',
    @IsPrimary BIT = NULL,
    @IsActive BIT = 1,
    @VerifiedOn DATETIMEOFFSET = NULL,
    @VerificationStatus NVARCHAR(20) = NULL,
    @VerificationAttempts INT = NULL,
    @LastVerificationAttempt DATETIMEOFFSET = NULL,
    @ModifiedBy BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[UserBankAccount]
    SET 
        [UserId] = @UserId,
        [AccountHolderName] = @AccountHolderName,
        [BankName] = @BankName,
        [BranchName] = @BranchName,
        [AccountNumber] = @AccountNumber,
        [LastFourDigits] = @LastFourDigits,
        [AccountType] = @AccountType,
        [RoutingNumber] = @RoutingNumber,
        [IFSCCode] = @IFSCCode,
        [SWIFTCode] = @SWIFTCode,
        [Currency] = @Currency,
        [IsPrimary] = @IsPrimary,
        [IsActive] = @IsActive,
        [VerifiedOn] = @VerifiedOn,
        [VerificationStatus] = @VerificationStatus,
        [VerificationAttempts] = @VerificationAttempts,
        [LastVerificationAttempt] = @LastVerificationAttempt,
        [ModifiedBy] = @ModifiedBy,
        [ModifiedOn] = SYSDATETIMEOFFSET()
    WHERE [Id] = @Id;
END