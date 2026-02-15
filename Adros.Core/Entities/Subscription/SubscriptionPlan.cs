using Adros.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Entities.Subscription
{
    public class SubscriptionPlan : BaseEntity
    {
        //[Key]
        //public Guid SubscriptionPlanId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationInDays { get; set; }

        [Required]
        public string PlanType { get; set; } // "monthly", "quarterly", "semi_annual", "annual"

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        // Features
        //public bool HasVideoLessons { get; set; }
        //public bool HasLiveSessions { get; set; }
        //public bool HasQuizzes { get; set; }
        //public bool HasCertificates { get; set; }
        //public int MaxCourses { get; set; }
    }
}
