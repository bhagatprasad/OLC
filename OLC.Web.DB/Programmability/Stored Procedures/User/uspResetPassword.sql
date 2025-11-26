CREATE PROCEDURE [dbo].[uspResetPassword]
(
	@userId			    bigint,
	@passwordHash       nvarchar(max),
	@passwordSalt       nvarchar(max)
)
AS
 BEGIN

 UPDATE  [dbo].[User]				Set 
         PasswordHash				=@passwordHash
		,PasswordSalt				=@passwordSalt
		,LastPasswordChangedOn      =GETDATE()
		,ModifiedOn                 =GETDATE()
		,ModifiedBy                 =@userId
		 WHERE
		 Id							= @userId
END
