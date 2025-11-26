CREATE PROCEDURE [dbo].[uspGetAllServiceRequestsWithReplies]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
			SR.TicketId,
			SR.OrderId,
			SR.UserId,
			SR.Subject,
			SR.Message AS RequestMessage,
			SR.Category,
			SR.RequestReference,
			SR.Classification,
			SR.Priority,
			SR.StatusId,
			SR.AssignTo,
			SR.AssignBy,
			SR.AssignedOn,
			SR.CreatedBy AS RequestCreatedBy,
			SR.CreatedOn AS RequestCreatedOn,
			SR.ModifiedBy AS RequestModifiedBy,
			SR.ModifiedOn AS RequestModifiedOn,
			SR.IsActive AS RequestIsActive,
			R.Id AS ReplyId,
			R.ReplierId,
			R.Message AS ReplyMessage,
			R.Status AS ReplyStatus,
			R.IsInternal,
			R.CreatedBy AS ReplyCreatedBy,
			R.CreatedOn AS ReplyCreatedOn,
			R.ModifiedBy AS ReplyModifiedBy,
			R.ModifiedOn AS ReplyModifiedOn,
			R.IsActive AS ReplyIsActive

		FROM [dbo].[ServiceRequest] SR
		LEFT JOIN [dbo].[ServiceRequestReplies] R ON SR.TicketId = R.TicketId
		ORDER BY SR.TicketId, R.Id;
END