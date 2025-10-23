﻿CREATE PROCEDURE [dbo].[uspGetAddressType]

WITH RECOMPILE

AS

BEGIN

  SELECT
		   [Id]
		  ,[Name]
		  ,[Code]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedBy]
		  ,[ModifiedOn]
		  ,[IsActive]
  FROM [dbo].[AddressType]

END