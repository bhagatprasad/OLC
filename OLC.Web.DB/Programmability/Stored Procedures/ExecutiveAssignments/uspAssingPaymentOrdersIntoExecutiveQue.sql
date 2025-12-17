CREATE PROCEDURE [dbo].[uspAssignPaymentOrdersIntoExecutiveQue]
(
    @PaymentOrderIds   NVARCHAR(MAX),
    @UserId            BIGINT,
    @ExecutiveId       BIGINT,
    @AssignedBy        BIGINT,
    @AssignedAt        DATETIMEOFFSET = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Default assigned date
    IF @AssignedAt IS NULL
        SET @AssignedAt = SYSDATETIMEOFFSET();

    /* Insert into ExecutiveAssignments */
    INSERT INTO [dbo].[ExecutiveAssignments]
    (
        UserId,
        PaymentOrderId,
        ExecutiveId,
        OrderQueueId,
        AssignmentStatus,
        AssignedAt,
        AssignedBy,
        CreatedBy
    )
    SELECT
        @UserId,
        t.PaymentOrderId,
        @ExecutiveId,
        oq.Id AS OrderQueueId,
        'Assigned',
        @AssignedAt,
        @AssignedBy,
        @AssignedBy
    FROM dbo.udfSplitPaymentOrderIds(@PaymentOrderIds) t
    INNER JOIN dbo.OrderQueue oq  ON oq.PaymentOrderId = t.PaymentOrderId

    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.ExecutiveAssignments ea
        WHERE ea.PaymentOrderId = t.PaymentOrderId
          AND ea.IsActive = 1
    );

    UPDATE oq
    SET
        oq.QueueStatus = 'Assigned',
        oq.AssignedExecutiveId  = @ExecutiveId,
        oq.AssignedOn  = @AssignedAt
    FROM dbo.OrderQueue oq
    INNER JOIN dbo.udfSplitPaymentOrderIds(@PaymentOrderIds) t ON oq.PaymentOrderId = t.PaymentOrderId;       
END;