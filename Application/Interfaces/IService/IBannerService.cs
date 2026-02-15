using Adros.Application.DTOs.Banner;

namespace Adros.Application.Interfaces.IService
{
    public interface IBannerService
    {
        Task<IReadOnlyList<ClientBannerDto>> GetClientBannersAsync();
        Task<BannerEntityDto> CreateBannerAsync(BannerCreateDto bannerCreateDto);
        Task<ClientBannerDto?> UpdateBannerAsync(Guid bannerId, BannerUpdateDto bannerUpdateDto);
        Task<bool> DeleteBannerAsync(Guid bannerId);
        Task<BannerEntityDto?> GetBannerByIdAsync(Guid bannerId);
    }
}
