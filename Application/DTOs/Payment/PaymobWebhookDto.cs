using System;

namespace Adros.Application.DTOs.Payment
{
    public class PaymobWebhookDto
    {
        // بيانات أساسية
        public string OrderId { get; set; }
        public bool Success { get; set; }
        public string TransactionId { get; set; }
        public int AmountCents { get; set; }
        public string MerchantOrderId { get; set; }
        public string Message { get; set; }
    }
}
