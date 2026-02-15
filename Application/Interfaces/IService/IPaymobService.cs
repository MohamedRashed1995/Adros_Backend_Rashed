using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface IPaymobService
    {
        Task<(string paymentUrl, string paymobOrderId)> CreatePaymentAsync(decimal amount, string merchantOrderId);
    }

}
