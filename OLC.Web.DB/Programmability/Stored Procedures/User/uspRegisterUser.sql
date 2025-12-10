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
            RETURN -1;
        END

        -- Check if phone already exists
        IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE [Phone] = @phone AND [IsActive] = 1)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -2;
        END

        -- Check if both email and phone exist
        IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE [Email] = @email AND [Phone] = @phone AND [IsActive] = 1)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -3;
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
            0,
            -1,
            GETDATE(),
            -1,
            GETDATE(),
            1,
            0
        );

        DECLARE @newUserId BIGINT = SCOPE_IDENTITY();

        -- Insert into Executives if role is 3
        IF (@roleId = 3)
        BEGIN
            EXEC dbo.uspInsertExecutive
                @UserId = @newUserId,
                @FirstName = @firstName,
                @LastName = @lastName,
                @Email = @email,
                @MaxConcurrentOrders = 100,
                @IsAvailable = 1,
                @CurrentOrderCount = 0;
        END

        -- Create wallet
        EXEC dbo.uspSaveUserWallet @UserId = @newUserId;

        COMMIT TRANSACTION;

        -- Return success
        SELECT @newUserId AS UserId;
        RETURN 1;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;

        RETURN -99;
    END CATCH
END
