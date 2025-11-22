CREATE PROCEDURE [dbo].[uspSaveUserWallet]
(
  @userId bigint
)
AS 
BEGIN

INSERT INTO [dbo].[UserWallet] 
            ([UserId], 
            [WalletId],
            [WalletType], 
            [CurrentBalance], 
            [TotalEarned],
            [TotalSpent], 
            [Currency],
            [IsActive], 
            [CreatedBy],
            [ModifiedBy])
VALUES
            (@userId,
             'UW-' + CAST(NEWID() AS NVARCHAR(36)),
            'Rewards',
             0.000000,
             0.000000,
             0.000000,
            'INR',
             1,
            -1,
            -1)
END
