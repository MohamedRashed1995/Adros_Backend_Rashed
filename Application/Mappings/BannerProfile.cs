using Adros.Application.DTOs.Banner;
using Adros.Core.Entities.Home;
using Adros.Shared.Helpers;

namespace Adros.Application.Mappings
{
    public class BannerProfile : BaseProfile
    {
        public BannerProfile()
        {
            CreateMap<Banner, ClientBannerDto>()
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom<ImageUrlResolver<Banner>>())
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));

            CreateMap<BannerCreateDto, Banner>()
                .ForMember(dest => dest.ImageName, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));
            CreateMap<BannerUpdateDto, Banner>()
                .ForMember(dest => dest.ImageName, opt => opt.Ignore());

            CreateMap<Banner, BannerEntityDto>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver<Banner>>());
        }
    }
}
