﻿CREATE PROCEDURE [dbo].[uspGetTransactionTypesById]
(
 @id  bigint
 )

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

     FROM [dbo].[TransactionType]

     WHERE Id=@id

     END