namespace Adros.Application.DTOs.Banner
{
    public class BannerEntityDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = default!;
        public int? Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
