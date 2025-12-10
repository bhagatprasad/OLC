CREATE PROCEDURE [dbo].[uspGetExecutiveByUserId]
(
    @UserId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
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
    FROM [dbo].[Executives]
    WHERE UserId = @UserId AND IsActive = 1;
END
