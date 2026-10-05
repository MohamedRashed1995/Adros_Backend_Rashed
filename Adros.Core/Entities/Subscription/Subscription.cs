using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Entities.Subscription
{
    public class Subscription : BaseEntity
    {
        [Required]
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }

        [Required]
        [ForeignKey("SubscriptionPlan")]
        public Guid SubscriptionPlanId { get; set; }

        [Required]
        [StringLength(100)]
        public string PaymentTransactionId { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } = "online";

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "active";

        public bool IsAutoRenew { get; set; } = false;

        public string Notes { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
        //public Guid UserId { get; set; }
        //public ApplicationUser User { get; set; }
        //public Guid StudentId { get; set; }
        //public Student Student { get; set; }
        //public Guid SubscriptionPlanId { get; set; }
        //public SubscriptionPlan SubscriptionPlan { get; set; }

        //public string PaymentTransactionId { get; set; }

        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

        //public string Status { get; set; } = "active"; // active, expired, cancelled, pending

        //public decimal AmountPaid { get; set; }

        //public string PaymentMethod { get; set; } // "credit_card", "vodafone_cash", "bank_transfer", "cash"

        //public bool IsAutoRenew { get; set; } = false;

        //public string Notes { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime? UpdatedAt { get; set; }
    }
}
