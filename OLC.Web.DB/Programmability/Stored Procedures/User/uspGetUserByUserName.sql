CREATE PROCEDURE [dbo].[uspGetUserByUserName]
(
	@userName			varchar(250)
)

AS

BEGIN
  
  SELECT 
     Id
	,FirstName
	,LastName
	,Email
	,Phone
	,PasswordHash
	,PasswordSalt
	,RoleId
	,LastPasswordChangedOn
	,IsBlocked
	,IsApproved
	,ApprovedBy
	,ApprovedOn
	,CreatedBy
	,CreatedOn
	,ModifiedBy
	,ModifiedOn
	,IsActive
	,IsExternalUser
	FROM [dbo].[User]

	WHERE UPPER(EMAIL) = UPPER(@userName) or Phone = @userName

END