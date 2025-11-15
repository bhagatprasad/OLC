CREATE PROCEDURE [dbo].[uspInsertUserKyc]
(
    @userId            BIGINT,
    @kycStatus         VARCHAR(50)      = NULL,
    @kycLevel          VARCHAR(50)      = NULL,
    @submittedOn       DATETIMEOFFSET   = NULL,
    @verifiedOn        DATETIMEOFFSET   = NULL,
    @verifiedBy        BIGINT           = NULL,
    @rejectionReason   NVARCHAR(500)    = NULL
    
)
AS
BEGIN

    INSERT INTO [dbo].[UserKyc]
    (
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
    )
    VALUES
    (
        @userId,
        @kycStatus,
        @kycLevel,
        @submittedOn,
        @verifiedOn,
        @VerifiedBy,
        @rejectionReason,
        @userId,
        GETDATE(),
        @userId,
        GETDATE(),
        0
    );
END