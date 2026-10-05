using Adros.Application.DTOs.Stage;
using Adros.Core.Entities.Home;
using Adros.Shared.Helpers;

namespace Adros.Application.Mappings
{
    public class StageProfile:BaseProfile
    {
        public StageProfile()
        {
            // Map from Stage to StageEntityDto (Admin-facing DTO)
            CreateMap<Stage, StageEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom<ImageUrlResolver<Stage>>())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));

            // Map from Stage to ClientStageDto (Client-facing DTO)
            CreateMap<Stage, ClientStageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
                 //.ForMember(dest => dest.ImagePath, opt => opt.MapFrom<ImageUrlResolver<Stage>>());

            // Map from StageCreateDto to Stage (Entity for creation)
            CreateMap<StageCreateDto, Stage>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.ImageName, opt => opt.Ignore()) // Image handling is external
                .ForMember(dest => dest.Levels, opt => opt.Ignore()); 

            // Map from StageUpdateDto to Stage (Entity for updates)
            CreateMap<StageUpdateDto, Stage>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
                .ForMember(dest => dest.Order, opt => opt.Condition(src => src.Order.HasValue))
                .ForMember(dest => dest.ImageName, opt => opt.Ignore()) // Image handling is external
                .ForMember(dest => dest.Levels, opt => opt.Ignore()); // Not mapped for updates
        }
    }
}
