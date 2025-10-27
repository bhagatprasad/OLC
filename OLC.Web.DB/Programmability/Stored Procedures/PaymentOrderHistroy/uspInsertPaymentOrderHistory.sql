CREATE PROCEDURE [dbo].[uspInsertPaymentOrderHistory]	 
(        
    @paymentOrderId bigint    
    ,@statusId       bigint    
    ,@description    nvarchar(max)    
    ,@createdBy      bigint    
        
)    
AS     
 BEGIN    
     INSERT INTO [dbo].[PaymentOrderHistory]    
(    
                        
                    PaymentOrderId,    
                    StatusId,    
                    Description,    
                    CreatedBy,    
                    CreatedOn,    
                    ModifiedOn,    
                    ModifiedBy,    
                    IsActive    
)    
        VALUES    
(    
                       
                    @paymentOrderId    
                   ,@statusId    
                   ,@description    
                   ,@createdBy    
                   ,GETDATE()    
                   ,GETDATE()    
                   ,@createdBy    
                   ,1    
)    
END 