using Adros.Application.DTOs.Calender;
using Adros.Core.Entities.Home;
using Adros.Core.Enums;

namespace Adros.Application.Mappings
{
    public class CalenderProfile : BaseProfile
    {
        public CalenderProfile()
        {
            // Map from Calender to CalenderEntityDto
            CreateMap<Calender, CalenderEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.ToString()));

            // Map from CalenderEntityDto to Calender
            CreateMap<CalenderEntityDto, Calender>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // IDs should generally not be updated
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => Enum.Parse<CalenderColor>(src.Color)));

            // Map from Calender to ClientCalenderDto
            CreateMap<Calender, ClientCalenderDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.ToString()));

            // Map from CalenderCreateDto to Calender
            CreateMap<CalenderCreateDto, Calender>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => Enum.Parse<CalenderColor>(src.Color)));

            // Map from CalenderUpdateDto to Calender
            CreateMap<CalenderUpdateDto, Calender>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => Enum.Parse<CalenderColor>(src.Color)));
        }
    }
}
