//using Adros.Application.DTOs.Course;
using Adros.Application.DTOs.Topic;
using Adros.Core.Entities.Course;
using AutoMapper;

namespace Adros.Application.Mappings
{
    public class UnitProfile : BaseProfile
    {
        public UnitProfile()
        {
            CreateMap<Unit, UnitEntityDto>();

            CreateMap<UnitCreateDto, Unit>()
                //.ForMember(dest => dest.Prerequisites, opt => opt.Ignore())
                //.ForMember(dest => dest.Postrequisites, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
                .ForMember(dest => dest.Assessments, opt => opt.Ignore());
                //.ForMember(dest => dest.Videos, opt => opt.Ignore());

            CreateMap<UnitUpdateDto, Unit>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UnitCreateDto, Unit>();
            CreateMap<Unit, UnitEntityDto>();
            //CreateMap<Unit, UnitEntityDto>()
            //    .ForMember(dest => dest.Lessons,
            //        opt => opt.MapFrom(src => src.Lessons));

        }
    }
}
