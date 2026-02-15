using Adros.Application.DTOs.Level;
using Adros.Core.Entities.Course;

namespace Adros.Application.Mappings
{
    public class LevelProfile : BaseProfile
    {
        public LevelProfile() 
        {
            // Map from Level to LevelEntityDto (Admin-facing DTO)
            CreateMap<Level, LevelEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            // Map from Level to ClientLevelDto (Client-facing DTO)
            CreateMap<Level, ClientLevelDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

            // Map from LevelCreateDto to Level (Entity for creation)
            CreateMap<LevelCreateDto, Level>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(src => src.StageId));

            // Map from LevelUpdateDto to Level (Entity for updates)
            CreateMap<LevelUpdateDto, Level>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null));

        }
    }
}
