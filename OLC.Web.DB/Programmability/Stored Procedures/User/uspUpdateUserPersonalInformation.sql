
CREATE PROCEDURE [dbo].[uspUpdateUserPersonalInformation]
(
    @Id        BIGINT,
    @FirstName VARCHAR(250),
    @LastName  VARCHAR(250),
    @Email     VARCHAR(250),
    @Phone     VARCHAR(14)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS 
    (
        SELECT 1 
        FROM [dbo].[User] 
        WHERE Email = @Email AND Id <> @Id
    )
    BEGIN
        SELECT 0 AS Status;  
        RETURN;
    END

   
    IF EXISTS 
    (
        SELECT 1 
        FROM [dbo].[User] 
        WHERE Phone = @Phone AND Id <> @Id
    )
    BEGIN
        SELECT 0 AS Status;  
        RETURN;
    END

    
    UPDATE [dbo].[User]
    SET 
        FirstName = @FirstName,
        LastName  = @LastName,
        Email     = @Email,
        Phone     = @Phone,
        ModifiedOn = SYSDATETIMEOFFSET()
    WHERE Id = @Id;

    SELECT 1 AS Status;   
END
GO