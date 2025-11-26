CREATE PROCEDURE [dbo].[uspGetAllUsersKycDocuments]
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
    ORDER BY 
        Id DESC;
END
