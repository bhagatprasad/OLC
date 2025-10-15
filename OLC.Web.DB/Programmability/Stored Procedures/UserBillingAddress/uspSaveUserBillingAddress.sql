CREATE PROCEDURE [dbo].[uspSaveUserBillingAddress]
(
	@userId							bigint,
	@addessLineOne					varchar(max),
	@addessLineTwo					varchar(max),
	@addessLineThree				varchar(max),
	@location                       varchar(max),
	@countryId						bigint,
	@stateId						bigint,
	@cityId							bigint,
	@pinCode						varchar(max),
	@createdBy						bigint
)
AS
 BEGIN
   INSERT INTO [dbo].[UserBillingAddress] 
            (UserId
			,AddessLineOne
			,AddessLineTwo
			,AddessLineThress
			,Location
			,CountryId
			,StateId
			,CityId
			,PinCode
			,CreatedBy
			,CreatedOn
			,ModifiedBy
			,ModifiedOn
			,IsActive)
			Values
			(@userId
			,@addessLineOne
			,@addessLineTwo
			,@addessLineThree
			,@location
			,@countryId
			,@stateId
			,@cityId
			,@pinCode
			,@createdBy
			,GETDATE()
			,@createdBy
			,GETDATE()
			,1)
END
			