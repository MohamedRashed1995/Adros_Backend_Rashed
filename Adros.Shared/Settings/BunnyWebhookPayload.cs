namespace Adros.Shared.Settings
{
    internal class BunnyWebhookPayload
    {
        public Guid VideoGuid { get; set; }
        public bool Success { get; set; }
        public decimal Duration { get; set; }
        public string Status { get; set; } = default!;
        public string? ErrorMessage { get; set; }
    }
}
