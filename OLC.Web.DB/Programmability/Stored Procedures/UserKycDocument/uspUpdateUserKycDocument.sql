CREATE PROCEDURE [dbo].[uspUpdateUserKycDocument]
(
    @UserId              BIGINT,
    @DocumentType        VARCHAR(100),
    @DocumentNumber      VARCHAR(100)  ,
    @DocumentFilePath    NVARCHAR(500) ,
    @DocumentFileData    VARBINARY(MAX),
    @VerificationStatus  VARCHAR(50)   ,
    @VerifiedOn          DATETIMEOFFSET,
    @VerifiedBy          BIGINT        ,
    @RejectionReason     NVARCHAR(500) ,
    @ExpiryDate          DATE,
    @isActive            BIT
)
AS
BEGIN
    UPDATE [dbo].[UserKycDocument]
    SET 
        DocumentNumber      = @DocumentNumber,
        DocumentType        = @DocumentType,
        DocumentFilePath    = @DocumentFilePath,
        DocumentFileData    = @DocumentFileData,
        VerificationStatus  = @VerificationStatus,
        VerifiedOn          = @VerifiedOn,
        VerifiedBy          = @VerifiedBy,
        RejectionReason     = @RejectionReason,
        ExpiryDate          = @ExpiryDate,
        ModifiedOn          = GETDATE(),
        IsActive            =@isActive
    WHERE 
        UserId = @UserId
END