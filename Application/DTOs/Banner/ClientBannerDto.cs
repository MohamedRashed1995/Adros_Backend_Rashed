using Adros.Shared;

namespace Adros.Application.DTOs.Banner
{
    public class ClientBannerDto : BaseEntity
    {
        //public Guid Id { get; set; }
        public string ImageUrl { get; set; } = default!;
        public int? Order { get; set; }
    }
}
