﻿using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IPaymentOrderService
    {
        Task<PaymentOrder> InsertPaymentOrderAsync(PaymentOrder paymentOrder);
        Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId);
        Task<List<PaymentOrder>> GetPaymentOrdersAsync();
        Task<PaymentOrder> ProcessPaymentStatusAsync(ProcessPaymentStatus processPaymentStatus);
        Task<List<UserPaymentOrder>> GetUserPaymentOrderListAsync(long userId);
    }
}
