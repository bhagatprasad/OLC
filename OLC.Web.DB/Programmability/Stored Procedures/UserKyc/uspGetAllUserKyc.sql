CREATE PROCEDURE [dbo].[uspGetAllUserKyc]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        UserId,
        KycStatus,
        KycLevel,
        SubmittedOn,
        VerifiedOn,
        VerifiedBy,
        RejectionReason,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    FROM [dbo].[UserKyc]
    ORDER BY Id DESC;
END