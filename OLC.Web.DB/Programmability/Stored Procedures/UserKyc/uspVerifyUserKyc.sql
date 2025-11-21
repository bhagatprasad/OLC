CREATE PROCEDURE [dbo].[uspVerifyUserKyc]
(
	   @userKycId         BIGINT,
	   @userKycDocumentId BIGINT,
	   @status            VARCHAR(MAX),
	   @modifiedBy        BIGINT,
	   @rejectedReason    VARCHAR(MAX)
)
AS
BEGIN
--Update to UserKyc
	Update [UserKyc]
	set  
		KycStatus	 = @status ,
		VerifiedBy	 = @modifiedby ,
		VerifiedOn   =Getdate() 
	where 
		Id = @userKycId;

	--Update to the UserKycDocument

	update [UserKycDocument] 
	set 
		VerificationStatus= @status ,
		RejectionReason = @rejectedReason, 
		ModifiedBy= @modifiedby ,
		ModifiedOn=Getdate() 
	where 
		Id=@userKycDocumentId;
	END