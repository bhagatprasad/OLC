CREATE PROCEDURE [dbo].[uspInsertExecutive]
(
    @UserId                BIGINT,
    @FirstName             NVARCHAR(50)     ,
    @LastName              NVARCHAR(50)     ,
    @Email                 NVARCHAR(100)    ,
    @MaxConcurrentOrders   INT              = 100,
    @IsAvailable           BIT              = 1,
    @CurrentOrderCount     INT              = 0   
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Executives]
    (
        UserId,
        FirstName,
        LastName,
        Email,
        MaxConcurrentOrders,
        IsAvailable,
        CurrentOrderCount,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        @UserId,
        @FirstName,
        @LastName,
        @Email,
        @MaxConcurrentOrders,
        @IsAvailable,
        @CurrentOrderCount,
        @userId,
        GETDATE(),
        @userId, 
        GETDATE(),
        1
    );

    SELECT SCOPE_IDENTITY() AS NewExecutiveId;
END
