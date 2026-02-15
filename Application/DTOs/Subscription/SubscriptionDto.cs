using System;

namespace Adros.Application.DTOs.Subscription
{
    // DTO للإنشاء
    public class CreateSubscriptionDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
        public List<string> Benefits { get; set; } = new();
    }

    // DTO للتحديث
    public class UpdateSubscriptionDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
        public List<string> Benefits { get; set; } = new();
    }

    // DTO للعرض
    public class SubscriptionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
        public List<string> Benefits { get; set; } = new();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
