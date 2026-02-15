namespace Adros.Application.DTOs.Banner
{
    public class ClientBannerDto
    {
        public Guid Id { get; set; }
        //public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public int? Order { get; set; }
    }
}
