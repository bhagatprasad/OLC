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
DECLARE @AddressLineOne VARCHAR(MAX);
DECLARE @AddressLineTwo VARCHAR(MAX);
DECLARE @AddressLineThree VARCHAR(MAX);
DECLARE @Location VARCHAR(MAX);
DECLARE @CountryId BIGINT;
DECLARE @StateId BIGINT;
DECLARE @CityId BIGINT;
DECLARE @PinCode VARCHAR(MAX);
DECLARE @Locations TABLE (Id INT IDENTITY(1,1), Line1 VARCHAR(MAX), Line2 VARCHAR(MAX), Line3 VARCHAR(MAX), Loc VARCHAR(MAX), Pin VARCHAR(MAX));
DECLARE @LocIndex INT;

-- Populate a list of fake address variations (Indian-style)
INSERT INTO @Locations (Line1, Line2, Line3, Loc, Pin) VALUES 
('Hyderabad', 'Simhapuri Colony', 'Kukatpally', 'Etur Nagaram', '500043'),
('Mumbai', 'Bandra West', 'Hill Road', 'Mahalaxmi', '400050'),
('Delhi', 'Connaught Place', 'Rajiv Chowk', 'Central Delhi', '110001'),
('Bangalore', 'Koramangala', '1st Block', 'HSR Layout', '560034'),
('Chennai', 'T. Nagar', 'Pondy Bazaar', 'Adyar', '600020');

-- Cursor to fetch users
DECLARE UserCursor CURSOR FOR
SELECT Id, ISNULL(FirstName, 'Unknown'), ISNULL(LastName, 'Unknown')
FROM [dbo].[User]
WHERE IsActive = 1;  -- Only active users

OPEN UserCursor;
FETCH NEXT FROM UserCursor INTO @UserId, @FirstName, @LastName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Cycle through address variations (reset per user or vary)
    SET @LocIndex = (@UserId % 5) + 1;  -- Cycle based on UserId for variety
    
    SELECT @AddressLineOne = Line1, @AddressLineTwo = Line2, @AddressLineThree = Line3, @Location = Loc, @PinCode = Pin
    FROM @Locations WHERE Id = @LocIndex;
    
    -- Set IDs (based on sample: 1=India, 11=Telangana, 2=Hyderabad; vary slightly)
    SET @CountryId = 1;  -- India
    SET @StateId = CASE WHEN @LocIndex = 1 THEN 11 ELSE 12 + (@LocIndex % 3) END;  -- Telangana or others
    SET @CityId = CASE WHEN @LocIndex = 1 THEN 2 ELSE 3 + (@LocIndex % 3) END;  -- Hyderabad or others
    
    -- Insert the address
    INSERT INTO [dbo].[UserBillingAddress] (
        UserId, AddessLineOne, AddessLineTwo, AddessLineThress, Location, CountryId, StateId, CityId, PinCode,
        CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive
    )
    VALUES (
        @UserId, @AddressLineOne, @AddressLineTwo, @AddressLineThree, @Location, @CountryId, @StateId, @CityId, @PinCode,
        @UserId, SYSDATETIMEOFFSET(), @UserId, SYSDATETIMEOFFSET(), 1
    );
    
    FETCH NEXT FROM UserCursor INTO @UserId, @FirstName, @LastName;
END;

CLOSE UserCursor;
DEALLOCATE UserCursor;