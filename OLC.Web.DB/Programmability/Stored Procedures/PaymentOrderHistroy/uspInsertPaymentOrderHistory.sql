CREATE PROCEDURE [dbo].[uspInsertPaymentOrderHistory]	 
(        
    @paymentOrderId       bigint    
    ,@orderStatusId       bigint    
    ,@description         nvarchar(max)    
    ,@createdBy           bigint    
        
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
                   ,@orderStatusId    
                   ,@description    
                   ,@createdBy    
                   ,GETDATE()    
                   ,GETDATE()    
                   ,@createdBy    
                   ,1    
)    
END 