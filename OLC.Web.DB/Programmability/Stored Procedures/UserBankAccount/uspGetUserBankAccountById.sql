CREATE PROCEDURE [dbo].[uspGetUserBankAccountById]
(
    @Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
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
    FROM [dbo].[UserBankAccount]

    WHERE [Id] = @Id;
END

