CREATE PROCEDURE [dbo].[uspInsertOrderQueue]
(
    @PaymentOrderId        BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;

    declare @queueStatus varchar(20);

    set @queueStatus = 'Pending';

    declare @currentUser bigint;

    declare @currentDateTime datetimeoffset;

    set @currentUser = -1;

    set @currentDateTime = GETDATE();
   
    declare @metadata varchar(max);

    select  @metadata = Metadata FROM dbo.udfGetPaymentOrderMetaDataJson(@PaymentOrderId);

    INSERT INTO [dbo].[OrderQueue]
    (
        PaymentOrderId,
        QueueStatus,
        Priority,
        Metadata,
        CreatedOn,
        CreatedBy,
        ModifiedOn,
        ModifiedBy,
        IsActive
    )
    VALUES
    (
       @PaymentOrderId
      ,@queueStatus
      ,1
      ,@metadata
      ,@currentDateTime
      ,@currentUser
      ,@currentDateTime
      ,@currentUser
      ,1
    );
END
GO