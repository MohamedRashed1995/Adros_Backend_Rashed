    namespace Adros.Application.DTOs.Auth
    {
        public class LoginResponse
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string SubscriptionStatus { get; set; }
            public string Email { get; set; } = default!;
            public Guid? LevelId { get; set; }
            public string Phone { get; set; } = default!;
            public string Token { get; set; } = default!;
            public string? ValidTo { get; set; }
            public string? Role { get; set; }
        }
    }
