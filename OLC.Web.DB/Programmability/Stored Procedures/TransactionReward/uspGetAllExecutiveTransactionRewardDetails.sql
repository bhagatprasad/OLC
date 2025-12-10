CREATE PROCEDURE [dbo].[uspGetAllExecutiveTransactionRewardDetails]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        usr.[Id] AS UserId,
		 uw.[WalletId],
         po.[OrderReference] AS PaymentOrderReferenceId,
		 uw.[TotalEarned],
         uw.[TotalSpent],
		 uw.[CurrentBalance],
         po.[TotalAmountToChargeCustomer] AS ChargeableAmount,
         po.[TotalAmountToDepositToCustomer] AS DepositableAmount,
		 tr.[RewardAmount], 
        uba.[AccountHolderName],
        ucc.[EncryptedCardNumber] AS CardNumber,
        uba.[AccountNumber],		
		 tr.[CreatedBy],
	     tr.[CreatedOn],
		 tr.[IsActive]
    FROM [dbo].[TransactionReward] tr
        LEFT JOIN [dbo].[PaymentOrder] po        ON tr.PaymentOrderId = po.Id
        LEFT JOIN [dbo].[User] usr               ON  po.UserId = usr.Id     
        LEFT JOIN [dbo].[UserBankAccount] uba    ON  po.BankAccountId=uba.Id
        LEFT JOIN [dbo].[UserCreditCard] ucc     ON  po.CreditCardId= ucc.Id
		LEFT JOIN [dbo].[UserWallet] uw          ON  tr.CreditedToWalletId= uw.Id
END
