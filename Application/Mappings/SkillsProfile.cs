using Adros.Application.DTOs.Skills;
using Adros.Core.Entities.Home;

namespace Adros.Application.Mappings
{
    public class SkillsProfile : BaseProfile
    {
        public SkillsProfile()
        {
            CreateMap<SkillDto, Skill>();
            CreateMap<Skill, SkillShowDto>();
            CreateMap<VariousSkill, SkillDto>();
            CreateMap<Skill, SkillDto>();


        }
    }
}
