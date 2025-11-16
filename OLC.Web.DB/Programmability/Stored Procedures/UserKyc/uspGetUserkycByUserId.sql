CREATE PROCEDURE [dbo].[uspGetUserKycByUserId]
(
    @userId BIGINT
)
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
    WHERE UserId = @userId;
END