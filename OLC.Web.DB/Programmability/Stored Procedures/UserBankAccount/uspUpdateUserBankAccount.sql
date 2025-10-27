CREATE PROCEDURE [dbo].[uspUpdateUserBankAccount]
(
    @Id                            INT,
    @UserId                        BIGINT,
    @AccountHolderName             NVARCHAR(150),
    @BankName                      NVARCHAR(100),
    @BranchName                    NVARCHAR(100)      = NULL,
    @AccountNumber                 VARBINARY(MAX),
    @LastFourDigits                CHAR(4),
    @AccountType                   NVARCHAR(50),
    @RoutingNumber                 VARBINARY(MAX)     = NULL,
    @IFSCCode                      NVARCHAR(20)       = NULL,
    @SWIFTCode                     NVARCHAR(20)       = NULL,
    @Currency                      NVARCHAR(10)       = 'USD',
    @IsPrimary                     BIT                = NULL,
    @IsActive                      BIT = 1,
    @VerifiedOn                    DATETIME           = NULL,
    @VerificationStatus            NVARCHAR(20)       = NULL,
    @VerificationAttempts          INT                = NULL,
    @LastVerificationAttempt       DATETIME           = NULL,
    @ModifiedBy                    BIGINT             = NULL
)
AS
BEGIN
   
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
        [IsPrimary] = ISNULL(@IsPrimary, [IsPrimary]),
        [IsActive] = ISNULL(@IsActive, [IsActive]),
        [VerifiedOn] = @VerifiedOn,
        [VerificationStatus] = @VerificationStatus,
        [VerificationAttempts] = @VerificationAttempts,
        [LastVerificationAttempt] = @LastVerificationAttempt,
        [ModifiedBy] = @ModifiedBy,
        [ModifiedOn] = GETDATE()
    WHERE [Id] = @Id;
END

