CREATE PROCEDURE [dbo].[uspRegisterUser]
(
	@firstName			varchar(250),
	@lastName			varchar(250),
	@email              varchar(250),
	@phone				varchar(50),
	@passwordHash       nvarchar(max),
	@passwordSalt       nvarchar(max),
	@roleId             bigint
)
  AS
  
  BEGIN
     
	INSERT INTO [dbo].[User]
	(
			 [FirstName]
			,[LastName]
			,[Email]
			,[Phone]
			,[PasswordHash]
			,[PasswordSalt]
			,[RoleId]
			,[IsBlocked]
			,[CreatedBy]
			,[CreatedOn]
			,[ModifiedBy]
			,[ModifiedOn]
			,[IsActive] 
	)
	VALUES
	(
			 @firstName
			,@lastName
			,@email
			,@phone
			,@passwordHash
			,@passwordSalt
			,@roleId
			,0
			,-1
			,GETDATE()
			,-1
			,GETDATE()
			,1
	)
END