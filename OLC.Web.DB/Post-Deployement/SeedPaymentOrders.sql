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
-- Declare variables
DECLARE @UserId BIGINT;
DECLARE @CreditCardId BIGINT;
DECLARE @BankAccountId BIGINT;
DECLARE @BillingAddressId BIGINT;
DECLARE @Amount DECIMAL(18,6);
DECLARE @PlatformFeeAmount DECIMAL(18,6);
DECLARE @TotalAmountToCharge DECIMAL(18,6);
DECLARE @TotalAmountToDeposit DECIMAL(18,6);
DECLARE @TotalPlatformFee DECIMAL(18,6);
DECLARE @OrderReference NVARCHAR(50);
DECLARE @StripePaymentIntent NVARCHAR(255);
DECLARE @StripePaymentCharge NVARCHAR(255);
DECLARE @StripeDepositIntent NVARCHAR(255);
DECLARE @StripeDepositCharge NVARCHAR(255);
DECLARE @RandomSuffix NVARCHAR(10);

-- Cursor for users
DECLARE UserCursor CURSOR FOR
SELECT DISTINCT u.Id
FROM [dbo].[User] u
INNER JOIN [dbo].[UserCreditCard] cc ON u.Id = cc.UserId AND cc.IsActive = 1
WHERE u.IsActive = 1;

OPEN UserCursor;
FETCH NEXT FROM UserCursor INTO @UserId;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Get one bank account and billing address for this user (first active ones)
    SELECT TOP 1 @BankAccountId = Id FROM [dbo].[UserBankAccount] WHERE UserId = @UserId AND IsActive = 1;
    SELECT TOP 1 @BillingAddressId = Id FROM [dbo].[UserBillingAddress] WHERE UserId = @UserId AND IsActive = 1;
    
    -- Cursor for credit cards of this user
    DECLARE CreditCardCursor CURSOR FOR
    SELECT Id FROM [dbo].[UserCreditCard] WHERE UserId = @UserId AND IsActive = 1;
    
    OPEN CreditCardCursor;
    FETCH NEXT FROM CreditCardCursor INTO @CreditCardId;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Generate random amount (between 10000 and 100000 INR)
        SET @Amount = CAST((RAND() * 90000) + 10000 AS DECIMAL(18,6));
        
        -- Calculate fees (2% platform fee, as in sample)
        SET @PlatformFeeAmount = @Amount * 0.02;
        SET @TotalAmountToCharge = @Amount;  -- Since FeeCollectionMethod = 'No'
        SET @TotalAmountToDeposit = @Amount - @PlatformFeeAmount;
        SET @TotalPlatformFee = @PlatformFeeAmount;
        
        -- Generate unique OrderReference
        SET @RandomSuffix = RIGHT('000' + CAST(ABS(CHECKSUM(NEWID())) % 10000 AS VARCHAR(4)), 4);
        SET @OrderReference = 'ORD-PY-' + FORMAT(GETDATE(), 'ddMMyyyyHHmmss') + @RandomSuffix;
        
        -- Generate fake Stripe IDs
        SET @StripePaymentIntent = 'pi_' + LEFT(NEWID(), 20);
        SET @StripePaymentCharge = 'cs_test_' + LEFT(NEWID(), 30);
        SET @StripeDepositIntent = NULL;  -- As in sample
        SET @StripeDepositCharge = NULL;  -- As in sample
        
        -- Insert the order
        INSERT INTO [dbo].[PaymentOrder] (
            OrderReference, UserId, PaymentReasonId, Amount, TransactionFeeId, PlatformFeeAmount, FeeCollectionMethod,
            TotalAmountToChargeCustomer, TotalAmountToDepositToCustomer, TotalPlatformFee, Currency,
            CreditCardId, BankAccountId, BillingAddressId, OrderStatusId, PaymentStatusId, DepositStatusId,
            StripePaymentIntentId, StripePaymentChargeId, StripeDepositeIntentId, StripeDepositeChargeId,
            CreatedBy, CreatedOn, ModifiedOn, ModifiedBy, IsActive
        )
        VALUES (
            @OrderReference, @UserId, 41, @Amount, 3, @PlatformFeeAmount, 'No',
            @TotalAmountToCharge, @TotalAmountToDeposit, @TotalPlatformFee, 'INR',
            @CreditCardId, @BankAccountId, @BillingAddressId, 42, 24, 1,
            @StripePaymentIntent, @StripePaymentCharge, @StripeDepositIntent, @StripeDepositCharge,
            @UserId, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), @UserId, 1
        );
        
        FETCH NEXT FROM CreditCardCursor INTO @CreditCardId;
    END;
    
    CLOSE CreditCardCursor;
    DEALLOCATE CreditCardCursor;
    
    FETCH NEXT FROM UserCursor INTO @UserId;
END;

CLOSE UserCursor;
DEALLOCATE UserCursor;

-- Optional: Select to verify (top 10 for brevity)
--SELECT TOP 10 * FROM [dbo].[PaymentOrder] ORDER BY Id DESC;