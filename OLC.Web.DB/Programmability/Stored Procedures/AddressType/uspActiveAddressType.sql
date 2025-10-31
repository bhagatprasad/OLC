CREATE PROCEDURE [dbo].[uspActivateAddressType]
(
	@addressTypeId     bigint,
	@modifiedBy      bigint
)
AS
 BEGIN 
	UPDATE   [dbo].[AddressType]  SET  
	                                    
										IsActive   = 1,
										ModifiedBy = @modifiedBy,
										ModifiedOn = GETDATE()

								   WHERE        Id = @addressTypeId
 END