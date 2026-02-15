using Adros.Core.Entities.Users;
using Adros.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Entities.Subscription
{
    public class StudentSubscription : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public string? PaymobOrderId { get; set; }
        public bool IsPaid { get; set; } = false;
        public string? PaymentTransactionId { get; set; } // من PayMob بعد الدفع
    }

}
