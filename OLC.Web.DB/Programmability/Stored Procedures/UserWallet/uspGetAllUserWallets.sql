CREATE PROCEDURE [dbo].[uspGetAllWallets]
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
    ORDER BY 
        [Id] DESC;
END
