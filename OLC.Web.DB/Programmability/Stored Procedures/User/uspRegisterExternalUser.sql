CREATE PROCEDURE [dbo].[uspRegisterExternalUser]
(
    @firstName        VARCHAR(250),
    @lastName         VARCHAR(250),
    @email            VARCHAR(250),
    @roleId           BIGINT,
    @userId           BIGINT OUTPUT,  -- Add output parameter
    @statusCode       INT OUTPUT      -- Add output parameter
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if email already exists
        IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE [Email] = @email AND [IsActive] = 1)
        BEGIN
            SET @statusCode = -1; -- Email already exists
            SET @userId = 0;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insert new user
        INSERT INTO [dbo].[User]
        (
            [FirstName],
            [LastName],
            [Email],
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
            @roleId,
            0,      -- IsBlocked
            -1,     -- CreatedBy (system user)
            GETDATE(),
            -1,     -- ModifiedBy (system user)
            GETDATE(),
            1 , -- IsActive
            1 
        );

        SET @userId = SCOPE_IDENTITY();
        SET @statusCode = 1; -- Success

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        SET @statusCode = -99; -- General error
        SET @userId = 0;
    END CATCH
END