CREATE PROCEDURE [dbo].[uspProcessUserKyc]
(
    @id             BIGINT, 
    @kycStatus      VARCHAR(50) ,
    @kycLevel       VARCHAR(50) ,
    @verifiedBy     BIGINT ,
    @rejectionReason nvarchar(500),
    @modifiedBy     BIGINT, 
    @isActive       bigint  
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE UserKyc
    SET
        KycStatus             = @kycStatus,
        KycLevel              = @kycLevel,
        VerifiedOn            = GETDATE(),
        VerifiedBy            = @verifiedBy,
        RejectionReason       = @rejectionReason,
        ModifiedBy            = @modifiedBy,
        ModifiedOn            = GETDATE(),
        IsActive              =@isActive
    WHERE Id = @id;
END