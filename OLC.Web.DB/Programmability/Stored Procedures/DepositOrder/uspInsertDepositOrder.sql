CREATE PROCEDURE [dbo].[uspInsertDepositOrder]  
(  
   @PaymentOrderId BIGINT,  
   @OrderReference NVARCHAR(100),  
   @DepositAmount DECIMAL(18,6),  
   @ActualDepositAmount DECIMAL(18,6),  
   @PendingDepositAmount DECIMAL(18,6), 
   @StripeDepositIntentId NVARCHAR(100),  
   @StripeDepositChargeId NVARCHAR(100),  
   @IsPartialPayment BIT,  
   @CreatedBy BIGINT  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    DECLARE   
        @OrderStatusId      BIGINT,  
        @PaymentStatusId    BIGINT,  
        @DepositStatusId    BIGINT,  
        @DepositDescription NVARCHAR(MAX),  
        @TotalDepositAmount DECIMAL(18,6),  
        @TotalAmountToDepositToCustomer DECIMAL(18,6),  
        @ComputedPending DECIMAL(18,6),
        @InsertedId BIGINT;  
  

    INSERT INTO [dbo].[DepositOrder]  
    (  
        PaymentOrderId,  
        OrderReference,  
        DepositeAmount,  
        ActualDepositeAmount,  
        PendingDepositeAmount,  
        StripeDepositeIntentId,  
        StripeDepositeChargeId,  
        IsPartialPayment,  
        CreatedBy,  
        CreatedOn,  
        ModifiedBy,  
        ModifiedOn,  
        IsActive  
    )  
    VALUES  
    (  
        @PaymentOrderId,  
        @OrderReference,  
        @DepositAmount,  
        @ActualDepositAmount,  
        @PendingDepositAmount,  
        @StripeDepositIntentId,  
        @StripeDepositChargeId,  
        @IsPartialPayment,  
        @CreatedBy,  
        GETDATE(),  
        @CreatedBy,  
        GETDATE(),  
        1  
    );  

    SET @InsertedId = SCOPE_IDENTITY();

    SELECT   
        @TotalDepositAmount = ISNULL(SUM(DepositeAmount),0)
    FROM [dbo].[DepositOrder]  
    WHERE PaymentOrderId = @PaymentOrderId;  

    SELECT   
        @TotalAmountToDepositToCustomer = ISNULL(TotalAmountToDepositToCustomer,0),  
        @PaymentStatusId = PaymentStatusId  
    FROM [dbo].[PaymentOrder]  
    WHERE Id = @PaymentOrderId;  

    SET @ComputedPending = @TotalAmountToDepositToCustomer - @TotalDepositAmount;

    IF @ComputedPending < 0
        SET @ComputedPending = 0;

    UPDATE [dbo].[DepositOrder]
    SET PendingDepositeAmount = @ComputedPending,
        ModifiedOn = GETDATE(),
        ModifiedBy = @CreatedBy
    WHERE Id = @InsertedId;

    IF (ABS(@TotalDepositAmount - @TotalAmountToDepositToCustomer) < 0.0001 AND @ComputedPending = 0)  
    BEGIN  
        SET @OrderStatusId = 24;   
        SET @DepositStatusId = 24;  
        SET @DepositDescription = 'Fully Amount Deposited';  
    END  
    ELSE  
    BEGIN  
        SET @OrderStatusId = 37;  
        SET @DepositStatusId = 37;  
        SET @DepositDescription = 'Partially Amount Deposited';  
    END  

    EXEC [dbo].[uspProcessPaymentOrder]     
        @PaymentOrderId,  
        @OrderStatusId,   
        @PaymentStatusId,   
        @DepositStatusId,   
        @CreatedBy,        
        @DepositDescription;  

END;
