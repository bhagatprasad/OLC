CREATE PROCEDURE [dbo].[uspUploadeUserKycDocument]
(
    @userId                BIGINT,
    @documentType          VARCHAR(100),
    @documentNumber        VARCHAR(100)      = NULL,
    @documentFilePath      NVARCHAR(500)     = NULL,
    @documentFileData      VARBINARY(MAX)    = NULL,
    @verifiedOn            DATETIMEOFFSET    = NULL,
    @verifiedBy            BIGINT            = NULL,
    @rejectionReason       NVARCHAR(500)     = NULL,
    @expiryDate            DATE              = NULL
)
AS
BEGIN
    INSERT INTO [dbo].[UserKycDocument]
    (
        UserId,
        DocumentType,
        DocumentNumber,
        DocumentFilePath,
        DocumentFileData,
        VerificationStatus,
        VerifiedOn,
        VerifiedBy,
        RejectionReason,
        ExpiryDate,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        @userId,
        @documentType,
        @documentNumber,
        @documentFilePath,
        @documentFileData,
        'Pending',
        @verifiedOn,
        @verifiedBy,
        @rejectionReason,
        @expiryDate,
        @userId,
        GETDATE(),
        @userId,
        GETDATE(),
        0
    );
END