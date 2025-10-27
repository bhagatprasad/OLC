CREATE PROCEDURE [dbo].[uspInsertUserBankAccount]
(
    @UserId                  BIGINT,
    @AccountHolderName       NVARCHAR(150),
    @BankName                NVARCHAR(100),
    @BranchName              NVARCHAR(100)    = NULL,
    @AccountNumber           VARBINARY(MAX),
    @LastFourDigits          CHAR(4),
    @AccountType             NVARCHAR(50),
    @RoutingNumber           VARBINARY(MAX)   = NULL,
    @IFSCCode                NVARCHAR(20)     = NULL,
    @SWIFTCode               NVARCHAR(20)     = NULL,
    @Currency                NVARCHAR(10)     = 'USD',
    @IsPrimary               BIT = 0,
    @IsActive                BIT = 1,
    @VerifiedOn              DATETIME         = NULL,
    @VerificationStatus      NVARCHAR(20)     = 'Pending',
    @VerificationAttempts    INT = 0,
    @LastVerificationAttempt DATETIME         = NULL
    
)
AS
BEGIN

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
        @VerifiedOn,
        @VerificationStatus,
        @VerificationAttempts,
        @LastVerificationAttempt,
        @UserId,
        GETDATE(),
        @UserId,
        GETDATE()
    );
END