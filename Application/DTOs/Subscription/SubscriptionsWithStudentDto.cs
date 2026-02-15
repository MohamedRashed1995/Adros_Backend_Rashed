using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Subscription
{
    public class SubscriptionsWithStudentDto
    {
        public string StudentId { get; set; }
        public List<SubscriptionResponseDto> Subscriptions { get; set; }
    }

}
