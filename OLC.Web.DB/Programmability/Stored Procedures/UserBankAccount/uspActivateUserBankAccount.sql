CREATE PROCEDURE [dbo].[uspActivateUserBankAccount]
(
	@Id            BIGINT,
	@ModifiedBy    BIGINT
)
AS 
  BEGIN
       UPDATE [dbo].[UserBankAccount] SET

	                                     IsActive     = 1,
	                                     @ModifiedBy  = ModifiedBy,										 
										 CreatedOn    = GETDATE()
										
								    WHERE        Id = @Id
									END