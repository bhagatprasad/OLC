CREATE PROCEDURE [dbo].[uspGetAllUsersWalletlog]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        WalletId,
        UserId,
        Amount,
        TransactionType,
        Description,
        ReferenceId,
        Currency,
        IsActive,
        CreatedBy,
        CreatedOn,
        ModifiedOn,
        ModifiedBy
    FROM 
        UserWalletLog
    ORDER BY 
        Id DESC;  
END
