﻿CREATE PROCEDURE [dbo].[uspGetByUserBellingAddress]
	(
	  @UserBellingAddressId  bigint
	)
	WITH RECOMPILE

	AS 

	BEGIN

	SELECT
	    Id
		,UserId
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
		,IsActive

	FROM [dbo].[UserBillingAddress]

    WHERE Id=@UserBellingAddressId

    END
