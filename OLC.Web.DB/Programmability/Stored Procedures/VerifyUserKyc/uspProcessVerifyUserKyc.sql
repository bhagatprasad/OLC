
CREATE PROCEDURE [dbo].[GetVerifyUserKycProcess]
(
	   @userKycId         BIGINT,
	   @userKycDocumentId BIGINT,
	   @status            VARCHAR(MAX),
	   @modifiedBy        BIGINT,
	   @rejectedReason    VARCHAR(MAX)
)
AS
BEGIN
Update [UserKyc] set  KycStatus = @status , VerifiedBy = @modifiedby ,VerifiedOn=Getdate() where Id = @userKycId
 
update [userKycDocument] set VerificationStatus= @status ,RejectionReason = @rejectedReason, ModifiedBy= @modifiedby , ModifiedOn=Getdate() 
where Id=@userKycDocumentId
 
END