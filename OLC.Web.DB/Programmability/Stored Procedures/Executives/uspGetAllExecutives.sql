CREATE  PROCEDURE [dbo].[uspGetAllExecutives]
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
    ORDER BY Id DESC;
END
