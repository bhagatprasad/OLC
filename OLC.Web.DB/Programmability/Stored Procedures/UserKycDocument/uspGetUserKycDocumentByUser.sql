CREATE PROCEDURE [dbo].[uspGetUserKycDocumentByUser]
(
  @userId bigint
)
AS
BEGIN
    SELECT 
        Id,
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
    FROM 
        [dbo].[UserKycDocument]
    WHERE UserId = @userId
END
