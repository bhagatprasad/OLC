CREATE PROCEDURE [dbo].[uspGetUserAccounts]
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
	ORDER BY ModifiedOn DESC
END