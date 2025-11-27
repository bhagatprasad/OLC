CREATE PROCEDURE [dbo].[uspGetUserWalletLogByUserId]
(
    @UserId BIGINT
)
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
    WHERE 
        UserId = @UserId
    ORDER BY 
        Id DESC;
END
