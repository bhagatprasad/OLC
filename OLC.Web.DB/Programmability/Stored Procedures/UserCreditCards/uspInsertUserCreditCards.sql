CREATE PROCEDURE [dbo].[uspInsertUserCreditCards]

(
@uSerId							int,
@cardholderName			     	NVARCHAR(100),
@encryptedCardNumber		    NVARCHAR(MAX),
@maskedCardNumber			    VARCHAR(19),
@lastFourDigits					CHAR(4),
@expiryMonth					TINYINT ,
@expiryYear						SMALLINT ,
@encryptedCVV					NVARCHAR (MAX),
@cardType						NVARCHAR (50),
@issuingBank					NVARCHAR(100),
@billingAddress					NVARCHAR(250),
@createdBy                      BIGINT
)
AS
BEGIN
      INSERT INTO  [dbo].[UserCreditCard] 
          	 
           	(UserId
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
			,ModifiedOn)
	 VALUES
			(@uSerId
			,@cardholderName
			,CONVERT(VARBINARY(MAX) ,@encryptedCardNumber)
			,@maskedCardNumber
			,@lastFourDigits
			,@expiryMonth
			,@expiryYear
			,Convert(VARBINARY(MAX), @encryptedCVV)
			,@cardType
			,@issuingBank
			,@billingAddress
			,0
			,1
			,@createdBy
			,GETDATE()
			,@createdBy
			,GETDATE())
END
			 


			
