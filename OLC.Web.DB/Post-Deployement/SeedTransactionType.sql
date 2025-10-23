/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [dbo].[TransactionType] AS target
USING (
    VALUES 
        ('Deposit', 'DEPOSIT'),
        ('Withdrawal', 'WITHDRAWAL'),
        ('Payout', 'PAYOUT'),
        ('Payment', 'PAYMENT'),
        ('Refund', 'REFUND'),
        ('Chargeback', 'CHARGEBACK'),
        ('Authorization', 'AUTHORIZATION'),
        ('Capture', 'CAPTURE'),
        ('Void', 'VOID'),
        ('Settlement', 'SETTLEMENT'),
        ('Reversal', 'REVERSAL'),
        ('Transfer', 'TRANSFER'),
        ('Funds Transfer', 'FUNDS_TRANSFER'),
        ('Bank Transfer', 'BANK_TRANSFER'),
        ('Wallet Transfer', 'WALLET_TRANSFER'),
        ('Top Up', 'TOP_UP'),
        ('Cash In', 'CASH_IN'),
        ('Cash Out', 'CASH_OUT'),
        ('Bill Payment', 'BILL_PAYMENT'),
        ('Utility Payment', 'UTILITY_PAYMENT'),
        ('Mobile Recharge', 'MOBILE_RECHARGE'),
        ('Donation', 'DONATION'),
        ('Subscription', 'SUBSCRIPTION'),
        ('Recurring Payment', 'RECURRING_PAYMENT'),
        ('One-Time Payment', 'ONE_TIME_PAYMENT'),
        ('Merchant Payment', 'MERCHANT_PAYMENT'),
        ('E-commerce Payment', 'ECOMMERCE_PAYMENT'),
        ('POS Payment', 'POS_PAYMENT'),
        ('Online Payment', 'ONLINE_PAYMENT'),
        ('Offline Payment', 'OFFLINE_PAYMENT'),
        ('International Payment', 'INTERNATIONAL_PAYMENT'),
        ('Domestic Payment', 'DOMESTIC_PAYMENT'),
        ('Cross Border', 'CROSS_BORDER'),
        ('Fee', 'FEE'),
        ('Commission', 'COMMISSION'),
        ('Tax', 'TAX'),
        ('Penalty', 'PENALTY'),
        ('Interest', 'INTEREST'),
        ('Dividend', 'DIVIDEND'),
        ('Salary', 'SALARY'),
        ('Loan Disbursement', 'LOAN_DISBURSEMENT'),
        ('Loan Repayment', 'LOAN_REPAYMENT'),
        ('Investment', 'INVESTMENT'),
        ('Redemption', 'REDEMPTION'),
        ('Escrow', 'ESCROW'),
        ('Release', 'RELEASE'),
        ('Adjustment', 'ADJUSTMENT'),
        ('Correction', 'CORRECTION'),
        ('Reconciliation', 'RECONCILIATION')
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1)
WHEN MATCHED THEN
    UPDATE SET 
        target.[Code] = source.[Code],
        target.[ModifiedBy] = NULL,
        target.[ModifiedOn] = SYSDATETIMEOFFSET(),
        target.[IsActive] = 1;