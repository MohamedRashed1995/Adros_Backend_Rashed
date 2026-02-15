using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Payment
{
    public class PaymobInitResponse
    {
        public string PaymentUrl { get; set; }
        public string? MerchantOrderId { get; set; }
    }

}
