CREATE PROCEDURE [dbo].[uspDeleteRoles]
	(@id bigint)

AS

BEGIN

update dbo.[Role]
       set IsActive=0 
	   where Id= @id

	    EXEC  dbo.[uspGetRoles]
END