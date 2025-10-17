CREATE PROCEDURE [dbo].[uspRegisterUser]
(
    @firstName        VARCHAR(250),
    @lastName         VARCHAR(250),
    @email            VARCHAR(250),
    @phone            VARCHAR(50),
    @passwordHash     NVARCHAR(MAX),
    @passwordSalt     NVARCHAR(MAX),
    @roleId           BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if email already exists
        IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE [Email] = @email AND [IsActive] = 1)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1; -- Email already exists
        END

        -- Check if phone already exists
        IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE [Phone] = @phone AND [IsActive] = 1)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -2; -- Phone already exists
        END

        -- Check if both email and phone exist
        IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE [Email] = @email AND [Phone] = @phone AND [IsActive] = 1)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -3; -- Both email and phone already exist
        END

        -- Insert new user
        INSERT INTO [dbo].[User]
        (
            [FirstName],
            [LastName],
            [Email],
            [Phone],
            [PasswordHash],
            [PasswordSalt],
            [RoleId],
            [IsBlocked],
            [CreatedBy],
            [CreatedOn],
            [ModifiedBy],
            [ModifiedOn],
            [IsActive],
            [IsExternalUser]
        )
        VALUES
        (
            @firstName,
            @lastName,
            @email,
            @phone,
            @passwordHash,
            @passwordSalt,
            @roleId,
            0,      -- IsBlocked
            -1,     -- CreatedBy (system user)
            GETDATE(),
            -1,     -- ModifiedBy (system user)
            GETDATE(),
            1,       -- IsActive
            0
        );

        COMMIT TRANSACTION;
        
        -- Return success with new UserId
        SELECT SCOPE_IDENTITY() AS UserId;
        RETURN 1; -- Success

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        -- Return error details
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
        RETURN -99; -- General error
    END CATCH
END