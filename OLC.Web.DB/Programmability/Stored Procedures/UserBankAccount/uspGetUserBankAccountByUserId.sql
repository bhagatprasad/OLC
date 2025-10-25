CREATE PROCEDURE [dbo].[uspGetAllUserBankAccountByUserId]
(
   @UserId BIGINT
  
)
AS
BEGIN
   
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
    WHERE [UserId] = @UserId;
END

