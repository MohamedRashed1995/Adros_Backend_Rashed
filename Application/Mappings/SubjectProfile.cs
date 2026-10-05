
using Adros.Core.Entities.Course;

namespace Adros.Application.Mappings
{
    public class SubjectProfile:BaseProfile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<SubjectInputDto, Subject>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}
