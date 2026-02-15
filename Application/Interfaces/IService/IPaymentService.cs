using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adros.Application.DTOs.Payment;

namespace Adros.Application.Interfaces.IService
{
    public interface IPaymentService
    {
        Task<PaymobInitResponse> InitiateSubscriptionPaymentAsync(Guid subscriptionId);
        Task HandlePaymobWebhookAsync(PaymobWebhookDto dto);
        Task<bool> DeleteSubscriptionsByStudentAsync(Guid studentId);
    }

}
