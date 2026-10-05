using Adros.Application.DTOs.Skills;
using Adros.Core.Entities.Home;

namespace Adros.Application.Mappings
{
    public class SkillsProfile : BaseProfile
    {
        public SkillsProfile()
        {
            CreateMap<VariousSkill, SkillDto>()
               .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.Views.Count)) // Custom mapping
               .ReverseMap(); // Allows mapping back if necessary
        }
        
    }
}
