CREATE PROCEDURE [dbo].[uspInsertServiceRequest]
(
	@orderId				bigint,
	@userId					bigint,
	@subject				nvarchar(max)	=NULL,
	@message				nvarchar(max)	=NULL,
	@category				nvarchar(max)	=NULL,
	@requestReference		nvarchar(max)	=NULL,
	@classification			nvarchar(max)	=NULL,
	@priority				nvarchar(max)	=NULL,
	@statusId				bigint			=NULL,
	@assignTo				bigint			=NULL,
	@assignBy				bigint			=NULL
)
AS
BEGIN 
	INSERT INTO [dbo].[ServiceRequest]
(
	OrderId,
	UserId,
	Subject,
	Message,
	Category,
	RequestReference,
	Classification,
	Priority,
	StatusId,
	AssignTo,
	AssignBy,
	AssignedOn,
	CreatedBy,
	CreatedOn,
	ModifiedBy,
	ModifiedOn,
	IsActive
)
	VALUES
(
			@orderId,				
			@userId,					
			@subject,				
			@message,				
			@category,				
			@requestReference,	
			@classification,			
			@priority,				
			@statusId,			
			@assignTo,				
			@assignBy,
			GETDATE(),
			@userId,
			GETDATE(),
			@userId,
			GETDATE(),
			1
)
END


