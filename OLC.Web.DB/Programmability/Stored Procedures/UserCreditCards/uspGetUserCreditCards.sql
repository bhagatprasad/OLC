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
			,IsDefault
			,CreatedBy
			,CreatedOn
			,ModifiedBy
			,ModifiedOn
			,IsActive
	  FROM 	[dbo].[UserCreditCard]

	  WHERE UserId =@userId
END
