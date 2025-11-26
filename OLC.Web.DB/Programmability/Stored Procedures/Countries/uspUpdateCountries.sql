CREATE PROCEDURE [dbo].[uspUpdateCountries]
	(
        
         @id                   bigint,
         @name                 varchar(255),
         @code                 varchar(255),
         @modifiedBy           bigint,
         @isActive             bit
)
WITH RECOMPILE
AS
BEGIN

    UPDATE [dbo].[Country]
    SET
        
        Name             =@name,
        Code             =@code,
        ModifiedBy       =@modifiedBy,
        ModifiedOn       =GETDATE(),
        IsActive         =@isActive
    WHERE Id = @id
END
