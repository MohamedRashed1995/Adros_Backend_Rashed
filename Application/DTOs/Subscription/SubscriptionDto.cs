using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs.Subscription
{
    public class CreateSubscriptionDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid SubscriptionPlanId { get; set; }

        [Required]
        public string PaymentTransactionId { get; set; }

        public string PaymentMethod { get; set; } = "online";
        public decimal? AmountPaid { get; set; }
        public bool IsAutoRenew { get; set; } = false;
        public string Notes { get; set; }
    }

    public class SubscriptionResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string PaymentTransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public bool IsAutoRenew { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class UpdateSubscriptionDto
    {
        public string Status { get; set; }
        public bool? IsAutoRenew { get; set; }
        public string Notes { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class SubscriptionStatsDto
    {
        public int TotalSubscriptions { get; set; }
        public int ActiveSubscriptions { get; set; }
        public int ExpiredSubscriptions { get; set; }
        public int PendingSubscriptions { get; set; }
        public int CancelledSubscriptions { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal YearlyRevenue { get; set; }
        public Dictionary<string, int> SubscriptionsByPlan { get; set; } = new();
        public Dictionary<string, int> SubscriptionsByStatus { get; set; } = new();
        public Dictionary<string, int> SubscriptionsByMonth { get; set; } = new();
    }
}