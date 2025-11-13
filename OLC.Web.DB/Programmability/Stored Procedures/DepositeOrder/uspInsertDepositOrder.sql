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
        @PendingAmount DECIMAL(18,6);  
          
    INSERT INTO [dbo].[DepositeOrder]  
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
      
 SELECT   
        @TotalDepositAmount = SUM(DepositeAmount),   
        @PendingAmount = MIN(PendingDepositeAmount)  
    FROM [dbo].[DepositeOrder]  
    WHERE PaymentOrderId = @PaymentOrderId;  
  
    SELECT   
        @TotalAmountToDepositToCustomer = TotalAmountToDepositToCustomer,  
        @PaymentStatusId = PaymentStatusId  
    FROM [dbo].[PaymentOrder]  
    WHERE Id = @PaymentOrderId;  
  
    IF (@TotalDepositAmount = @TotalAmountToDepositToCustomer AND @PendingAmount = 0)  
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
        @DepositDescription 
END
