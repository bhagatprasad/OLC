CREATE PROCEDURE [dbo].[uspUpdateUserBellingAddress]
 (
	 @id					bigint,
	 @userId				bigint,
	 @addessLineOne         varchar(max),
	 @addessLineTwo         varchar(max),
	 @addessLineThress      varchar(max),
	 @location				varchar(max),
	 @countryId				bigint,
	 @stateId				bigint,
	 @cityId				bigint,
	 @pinCode				varchar(max),
	 @modifiedBy            bigint,
	 @modifiedOn            datetimeoffset,
	 @isActive              bit)

 AS

 BEGIN

 update [dbo].[UserBillingAddress] 
		set
		     UserId            = @userId
			,AddessLineOne     = @addessLineOne
			,AddessLineTwo     = @addessLineTwo
			,AddessLineThress  = @addessLineThress
			,Location          = @location
			,CountryId         = @countryId
			,StateId           = @stateId
			,CityId            = @cityId
			,PinCode           = @pinCode
		    ,ModifiedBy		   = @modifiedBy
		    ,ModifiedOn		   = @modifiedOn
		    ,IsActive		   = @isActive
		 Where Id = @id
  END

