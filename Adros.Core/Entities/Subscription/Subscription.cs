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

<<<<<<< HEAD
namespace Adros.Core.Entities.Subscription  
=======
namespace Adros.Core.Entities.Subscription
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
{
    public class Subscription : BaseEntity
    {
        [Required]
<<<<<<< HEAD
        public string Name { get; set; } // Monthly, Annual, أو أي اسم
        [Required]
        public string Price { get; set; } // ممكن نخليه string عشان يظهر بالـ UI
        [Required]
        public string Duration { get; set; } // Monthly, Annually

        public List<string> Benefits { get; set; } = new(); // List of benefits كـ string مفصولة بفواصل
    }

=======
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
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
}
