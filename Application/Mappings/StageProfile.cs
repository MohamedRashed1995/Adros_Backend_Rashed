using Adros.Application.DTOs.Stage;
using Adros.Core.Entities.Home;
using Adros.Shared.Helpers;
using AutoMapper;

namespace Adros.Application.Mappings
{
    public class StageProfile:BaseProfile
    {
        public StageProfile()
        {
            // Map from Stage to StageEntityDto (Admin-facing DTO)
           
            // Map from Stage to ClientStageDto (Client-facing DTO)
            CreateMap<Stage, ClientStageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
            //.ForMember(dest => dest.ImagePath, opt => opt.MapFrom<ImageUrlResolver<Stage>>());

            CreateMap<Stage, StageEntityDto>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.ImageName)
                        ? string.Empty
                        : $"/Images/Stages/{src.ImageName}"))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy));

            CreateMap<StageCreateDto, Stage>()
                .ForMember(dest => dest.ImageName, opt => opt.Ignore())
                .ForMember(dest => dest.Levels, opt => opt.Ignore());

            CreateMap<StageUpdateDto, Stage>()
                .ForMember(dest => dest.ImageName, opt => opt.Ignore())
                .ForMember(dest => dest.Levels, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type.HasValue));
        }
    }
}
