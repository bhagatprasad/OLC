CREATE PROCEDURE [dbo].[uspUpdateTemplate]
(
  @id           bigint,
  @name         varchar(max),
  @code         varchar(max),
  @template     varchar(max),
  @modifiedBy   bigint,
  @isActive     bit
)

AS

BEGIN

SET NOCOUNT ON;

UPDATE [dbo].[EmailTemplate] set
           Name=@name,
           Code=@code,
           Template=@template,
           ModifiedBy =@modifiedBy,
		   ModifiedOn =SYSDATETIMEOFFSET(),
	       IsActive=@isActive

           WHERE Id=@id

  END

