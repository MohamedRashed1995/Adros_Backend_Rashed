namespace Adros.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SubscriptionStatus { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string? ValidTo { get; set; }
        public string? Role { get; set; }
    }
}
