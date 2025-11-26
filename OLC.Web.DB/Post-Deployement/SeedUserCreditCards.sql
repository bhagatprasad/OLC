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
DECLARE @FirstName VARCHAR(250);
DECLARE @LastName VARCHAR(250);
DECLARE @CardHolderName NVARCHAR(MAX);
DECLARE @Counter INT;
DECLARE @CardNumber NVARCHAR(MAX);
DECLARE @MaskedCard NVARCHAR(MAX);
DECLARE @LastFour NVARCHAR(MAX);
DECLARE @ExpiryMonth VARCHAR(25);
DECLARE @ExpiryYear VARCHAR(25);
DECLARE @CVV VARCHAR(25);
DECLARE @CardType VARCHAR(25);
DECLARE @Bank VARCHAR(100);
DECLARE @IsDefault BIT;
DECLARE @Banks TABLE (Id INT IDENTITY(1,1), BankName VARCHAR(100));
DECLARE @BankIndex INT;

-- Populate a list of banks for cycling
INSERT INTO @Banks (BankName) VALUES ('SBI'), ('HDFC'), ('ICICI'), ('Axis Bank'), ('Punjab National Bank'), ('Bank of Baroda'), ('Canara Bank'), ('Union Bank'), ('IDBI Bank'), ('Indian Bank');

-- Cursor to fetch users
DECLARE UserCursor CURSOR FOR
SELECT Id, ISNULL(FirstName, 'Unknown'), ISNULL(LastName, 'Unknown')
FROM [dbo].[User]
WHERE IsActive = 1;  -- Only active users

OPEN UserCursor;
FETCH NEXT FROM UserCursor INTO @UserId, @FirstName, @LastName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @CardHolderName = @FirstName + ' ' + @LastName;
    SET @Counter = 1;
    SET @BankIndex = 1;  -- Reset bank cycle per user

    WHILE @Counter <= 10
    BEGIN
        -- Generate dynamic card number (fake, sequential for uniqueness)
        SET @CardNumber = '4' + CAST(@Counter + 100 AS VARCHAR(3)) + '-1111-1111-' + RIGHT('000' + CAST(@Counter + 1000 AS VARCHAR(4)), 4);
        
        -- Masked card (XXXX-XXXX-XXXX-XXXX)
        SET @MaskedCard = 'XXXX-XXXX-XXXX-' + RIGHT(@CardNumber, 4);
        
        -- Last four digits
        SET @LastFour = RIGHT(@CardNumber, 4);
        
        -- Random expiry (month 01-12, year 2027-2041)
        SET @ExpiryMonth = RIGHT('0' + CAST((@Counter % 12) + 1 AS VARCHAR(2)), 2);
        SET @ExpiryYear = CAST(2026 + (@Counter % 15) + 1 AS VARCHAR(4));
        
        -- Fake CVV
        SET @CVV = RIGHT('00' + CAST(@Counter * 37 % 1000 AS VARCHAR(3)), 3);
        
        -- Alternate card type
        SET @CardType = CASE WHEN @Counter % 2 = 1 THEN 'Credit Card' ELSE 'Debit Card' END;
        
        -- Cycle through banks
        SELECT @Bank = BankName FROM @Banks WHERE Id = @BankIndex;
        SET @BankIndex = CASE WHEN @BankIndex < 10 THEN @BankIndex + 1 ELSE 1 END;
        
        -- Set default (first card per user)
        SET @IsDefault = CASE WHEN @Counter = 1 THEN 1 ELSE 0 END;
        
        -- Insert the card
        INSERT INTO [dbo].[UserCreditCard] (
            UserId, CardHolderName, EncryptedCardNumber, MaskedCardNumber, LastFourDigits,
            ExpiryMonth, ExpiryYear, EncryptedCVV, CardType, IssuingBank, IsDefault,
            CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive
        )
        VALUES (
            @UserId, @CardHolderName, @CardNumber, @MaskedCard, @LastFour,
            @ExpiryMonth, @ExpiryYear, @CVV, @CardType, @Bank, @IsDefault,
            @UserId, SYSDATETIMEOFFSET(), @UserId, SYSDATETIMEOFFSET(), 1
        );
        
        SET @Counter = @Counter + 1;
    END;
    
    FETCH NEXT FROM UserCursor INTO @UserId, @FirstName, @LastName;
END;

CLOSE UserCursor;
DEALLOCATE UserCursor;