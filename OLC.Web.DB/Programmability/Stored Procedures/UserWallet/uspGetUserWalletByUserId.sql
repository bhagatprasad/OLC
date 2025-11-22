CREATE PROCEDURE [dbo].[uspGetWalletsByUserId]
(
    @UserId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
        [UserId],
        [WalletId],
        [WalletType],
        [CurrentBalance],
        [TotalEarned],
        [TotalSpent],
        [Currency],
        [IsActive],
        [CreatedBy],
        [CreatedOn],
        [ModifiedOn],
        [ModifiedBy]
    FROM 
        [dbo].[UserWallet]
    WHERE 
        [UserId] = @UserId
    ORDER BY 
        [Id] DESC;
END
