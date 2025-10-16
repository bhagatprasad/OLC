CREATE PROCEDURE [dbo].[uspGetUserCreditCards]
	(
	@userId        bigint
	)
AS
  BEGIN
	  SELECT 
          	 Id
           	,UserId
			,CardHolderName
			,EncryptedCardNumber
			,MaskedCardNumber
			,LastFourDigits
			,ExpiryMonth
			,ExpiryYear
			,EncryptedCVV
			,CardType
			,IssuingBank
			,BillingAddress
			,IsDefault
			,IsActive
			,CreatedBy
			,CreatedOn
			,ModifiedBy
			,ModifiedOn


	  FROM 
	[dbo].[UserCreditCard]

	WHERE UserId =@userId
END
