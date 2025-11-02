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
DECLARE @AccountHolderName NVARCHAR(150);
DECLARE @Counter INT;
DECLARE @AccountNumber NVARCHAR(MAX);
DECLARE @LastFour CHAR(4);
DECLARE @BankName NVARCHAR(100);
DECLARE @BranchName NVARCHAR(100);
DECLARE @AccountType NVARCHAR(50);
DECLARE @RoutingNumber NVARCHAR(MAX);
DECLARE @IFSCCode NVARCHAR(20);
DECLARE @SWIFTCode NVARCHAR(20);
DECLARE @IsPrimary BIT;
DECLARE @Banks TABLE (Id INT IDENTITY(1,1), BankName NVARCHAR(100), Branch NVARCHAR(100));
DECLARE @AccountTypes TABLE (Id INT IDENTITY(1,1), TypeName NVARCHAR(50));
DECLARE @BankIndex INT;
DECLARE @TypeIndex INT;

-- Populate lists for cycling
INSERT INTO @Banks (BankName, Branch) VALUES 
('State Bank Of India', 'STATE BANK OF CHANDRUPATLA'),
('HDFC Bank', 'HDFC MAIN BRANCH'),
('ICICI Bank', 'ICICI CENTRAL BRANCH'),
('Axis Bank', 'AXIS CITY BRANCH'),
('Punjab National Bank', 'PNB REGIONAL OFFICE');

INSERT INTO @AccountTypes (TypeName) VALUES ('Savings'), ('Current'), ('NRI Account');

-- Cursor to fetch users
DECLARE UserCursor CURSOR FOR
SELECT Id, ISNULL(FirstName, 'Unknown'), ISNULL(LastName, 'Unknown')
FROM [dbo].[User]
WHERE IsActive = 1;  -- Only active users

OPEN UserCursor;
FETCH NEXT FROM UserCursor INTO @UserId, @FirstName, @LastName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @AccountHolderName = @FirstName + ' ' + @LastName;
    SET @Counter = 1;
    SET @BankIndex = 1;  -- Reset cycles per user
    SET @TypeIndex = 1;

    WHILE @Counter <= 3  -- Create 3 accounts per user (adjust as needed)
    BEGIN
        -- Generate dynamic account number (fake, sequential)
        SET @AccountNumber = '12345678' + CAST(@Counter + 100 AS VARCHAR(2)) + '90';  -- e.g., 123456781090
        
        -- Last four digits
        SET @LastFour = RIGHT(@AccountNumber, 4);
        
        -- Cycle through banks and branches
        SELECT @BankName = BankName, @BranchName = Branch FROM @Banks WHERE Id = @BankIndex;
        SET @BankIndex = CASE WHEN @BankIndex < 5 THEN @BankIndex + 1 ELSE 1 END;
        
        -- Cycle through account types
        SELECT @AccountType = TypeName FROM @AccountTypes WHERE Id = @TypeIndex;
        SET @TypeIndex = CASE WHEN @TypeIndex < 3 THEN @TypeIndex + 1 ELSE 1 END;
        
        -- Fake routing/IFSC/SWIFT (based on Indian context)
        SET @RoutingNumber = 'CHAND' + RIGHT('000' + CAST(@Counter AS VARCHAR(4)), 4);  -- e.g., CHAND0001
        SET @IFSCCode = LEFT(@BankName, 4) + RIGHT('000' + CAST(@Counter AS VARCHAR(3)), 3);  -- e.g., STAT001
        SET @SWIFTCode = LEFT(@BankName, 4) + 'IN' + RIGHT('00' + CAST(@Counter AS VARCHAR(2)), 2);  -- e.g., STATIN01
        
        -- Set primary (first account per user)
        SET @IsPrimary = CASE WHEN @Counter = 1 THEN 1 ELSE 0 END;
        
        -- Insert the account
        INSERT INTO [dbo].[UserBankAccount] (
            UserId, AccountHolderName, BankName, BranchName, AccountNumber, LastFourDigits,
            AccountType, RoutingNumber, IFSCCode, SWIFTCode, Currency, IsPrimary, IsActive,
            VerifiedOn, VerificationStatus, VerificationAttempts, LastVerificationAttempt,
            CreatedBy, CreatedOn, ModifiedBy, ModifiedOn
        )
        VALUES (
            @UserId, @AccountHolderName, @BankName, @BranchName, @AccountNumber, @LastFour,
            @AccountType, @RoutingNumber, @IFSCCode, @SWIFTCode, 'INR', @IsPrimary, 1,
            NULL, 'Pending', 0, NULL,
            @UserId, SYSDATETIMEOFFSET(), @UserId, SYSDATETIMEOFFSET()
        );
        
        SET @Counter = @Counter + 1;
    END;
    
    FETCH NEXT FROM UserCursor INTO @UserId, @FirstName, @LastName;
END;

CLOSE UserCursor;
DEALLOCATE UserCursor;