CREATE PROCEDURE [dbo].[uspInsertOrderQueue]
(
    @PaymentOrderId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @queueStatus VARCHAR(20) = 'Pending';
        DECLARE @currentUser BIGINT = -1;
        DECLARE @currentDateTime DATETIMEOFFSET = GETDATE();
        DECLARE @metadata NVARCHAR(MAX);
        
        -- Get metadata using scalar function
        SELECT @metadata = dbo.udfGetPaymentOrderMetaDataJsonScalar(@PaymentOrderId);
        
        -- Check if metadata was found (payment order exists)
        IF @metadata IS NULL
        BEGIN
            RAISERROR('Payment Order with ID %d not found.', 16, 1, @PaymentOrderId);
            RETURN;
        END
        
        -- Check for duplicates (optional but recommended)
        IF EXISTS (SELECT 1 FROM [dbo].[OrderQueue] 
                   WHERE PaymentOrderId = @PaymentOrderId AND IsActive = 1)
        BEGIN
            RAISERROR('Payment Order %d is already in the queue.', 16, 1, @PaymentOrderId);
            RETURN;
        END
        
        -- Insert into queue
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
            @PaymentOrderId,
            @queueStatus,
            1,
            @metadata,
            @currentDateTime,
            @currentUser,
            @currentDateTime,
            @currentUser,
            1
        );
    END TRY
    BEGIN CATCH
        -- Log error and re-throw
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO