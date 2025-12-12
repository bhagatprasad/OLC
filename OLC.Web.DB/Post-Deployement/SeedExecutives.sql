MERGE INTO [dbo].[Executives] AS Target
USING
(
    VALUES
        (1002, 'Excutive', 'Room 1', 'betalenexective@gmail.com', 100, 1, 0, -1, GETDATE(), -1, GETDATE(), 1),
        (1011, 'lishika', 'bhagat', 'lishika.bhagat@gmail.com', 100, 1, 0, -1, GETDATE(), -1, GETDATE(), 1),
        (1012, 'Sai', 'Charvan', 'Sai@gmail.com', 10, 1, 0, -1, GETDATE(), -1, GETDATE(), 1)
) AS Source
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

ON Target.UserId = Source.UserId

WHEN MATCHED THEN
    UPDATE SET
        Target.FirstName            = Source.FirstName,
        Target.LastName             = Source.LastName,
        Target.Email                = Source.Email,
        Target.MaxConcurrentOrders  = Source.MaxConcurrentOrders,
        Target.IsAvailable          = Source.IsAvailable,
        Target.CurrentOrderCount    = Source.CurrentOrderCount,
        Target.ModifiedBy           = Source.ModifiedBy,
        Target.ModifiedOn           = Source.ModifiedOn,
        Target.IsActive             = Source.IsActive

WHEN NOT MATCHED BY TARGET THEN
    INSERT
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
        Source.UserId,
        Source.FirstName,
        Source.LastName,
        Source.Email,
        Source.MaxConcurrentOrders,
        Source.IsAvailable,
        Source.CurrentOrderCount,
        Source.CreatedBy,
        Source.CreatedOn,
        Source.ModifiedBy,
        Source.ModifiedOn,
        Source.IsActive
    );
