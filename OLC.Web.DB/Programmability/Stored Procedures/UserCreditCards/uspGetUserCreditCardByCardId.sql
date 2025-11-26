CREATE PROCEDURE [dbo].[uspGetUserCreditCardByCardId]
(
	@createdCardId        bigint
)
AS
  BEGIN
	  SELECT TOP 1
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

	  WHERE Id = @createdCardId
END
