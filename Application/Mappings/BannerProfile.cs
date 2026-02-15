using Adros.Application.DTOs.Banner;
using Adros.Core.Entities.Home;
using Adros.Application.Mappings.ImageResolver;
using Adros.Shared.Helpers;

namespace Adros.Application.Mappings
{
    public class BannerProfile : BaseProfile
    {
        public BannerProfile()
        {
            // =========================
            // Banner -> ClientBannerDto
            // =========================
            CreateMap<Banner, ClientBannerDto>()
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom<BannerImageUrlResolver<ClientBannerDto>>())
                .ForMember(dest => dest.Order, opt =>
                    opt.MapFrom(src => src.Order));

            // =========================
            // BannerCreateDto -> Banner
            // =========================
            CreateMap<BannerCreateDto, Banner>()
                .ForMember(dest => dest.ImageName, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt =>
                    opt.MapFrom(src => src.Order));

            // =========================
            // BannerUpdateDto -> Banner
            // =========================
            CreateMap<BannerUpdateDto, Banner>()
                .ForMember(dest => dest.ImageName, opt => opt.Ignore());

            // =========================
            // Banner -> BannerEntityDto
            // =========================
            CreateMap<Banner, BannerEntityDto>()
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom<BannerImageUrlResolver<BannerEntityDto>>());
        }
    }
}
