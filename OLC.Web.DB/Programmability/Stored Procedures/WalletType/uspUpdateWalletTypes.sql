CREATE PROCEDURE [dbo].[uspUpdateWalletTypes]
	(
	   @id            bigint,
	   @name          varchar(50),
	   @code          varchar(50),
	   @modifiedBy    bigint,
	   @isActive      bit
	)

	AS

	BEGIN
	SET NOCOUNT ON;
	update [dbo].[WalletType] set
	             Name=@name,
				 Code=@code,
				 ModifiedBy =@modifiedBy,
				 ModifiedOn =SYSDATETIMEOFFSET(),
				 IsActive=@isActive
          Where Id = @id

		  EXEC  [dbo].[uspUpdateWalletTypes]

	END
				 