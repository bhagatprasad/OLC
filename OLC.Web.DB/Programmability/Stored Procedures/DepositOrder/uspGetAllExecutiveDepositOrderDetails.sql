CREATE PROCEDURE [dbo].[uspGetAllExecutiveDepositOrderDetails]	 
AS
  BEGIN
     SELECT
	    usr.Id as UserId,
		do.Id as DepositOrderId,
		po.Id as PaymentOrderId,		
		po.PaymentReasonId,
		do.OrderReference as 'DepositeReferance',
		po.OrderReference,
		do.ActualDepositeAmount as 'TotalAmount',
		do.DepositeAmount as 'DepositedAmount',
		do.PendingDepositeAmount as 'PendingAmount',
		usr.Email as 'UserEmail',
		usr.Phone as 'UserPhone',
		ucc.EncryptedCardNumber as 'CreditCardNumber',
		uba.AccountHolderName as 'DepositedTo',
		uba.AccountNumber as 'BankAccount',
		uba.IFSCCode,
		uba.BankName,
		uba.BranchName
	FROM[DepositOrder] do 
		left join [PaymentOrder] po ON do.PaymentOrderId = po.Id
		left join [User] usr on po.UserId = usr.Id
		left join [UserCreditCard] ucc on po.CreditCardId= ucc.Id
		left join [UserBankAccount] uba on po.BankAccountId=uba.Id
		END;
