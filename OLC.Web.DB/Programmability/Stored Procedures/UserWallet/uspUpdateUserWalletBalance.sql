CREATE PROCEDURE [dbo].[uspUpdateUSerWalletBalance]
(
	@userId			nvarchar(20),
	@currentBalance		DECIMAL(18,6),
	@totalEarned		DECIMAL(18,6),
	@totalSpent			DECIMAL(18,6)
)
AS
BEGIN
	UPDATE [dbo].[UserWallet] 
	SET
	CurrentBalance	= @currentBalance,
	TotalEarned		= @totalEarned,
	TotalSpent		= @totalSpent,
	ModifiedON		=GETDATE()
WHERE 
	UserId	=@userId
END
	
		