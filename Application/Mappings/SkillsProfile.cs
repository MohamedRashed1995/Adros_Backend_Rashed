using Adros.Application.DTOs.Skills;
using Adros.Core.Entities.Home;

namespace Adros.Application.Mappings
{
    public class SkillsProfile : BaseProfile
    {
        public SkillsProfile()
        {
<<<<<<< HEAD
            CreateMap<SkillDto, Skill>();
            CreateMap<Skill, SkillShowDto>();
            CreateMap<VariousSkill, SkillDto>();
            CreateMap<Skill, SkillDto>();

=======
            // Mapping from Entity -> DTO
            // Entity -> DTO
            CreateMap<VariousSkill, SkillDto>()
                //.ForMember(dest => dest.viewsCount, opt => opt.MapFrom(src => src.Views.Count))
                .ForMember(dest => dest.createdAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
            //.ForMember(dest => dest.createdBy, opt => opt.MapFrom(src => src.CreatedBy))
            //.ForMember(dest => dest.updatedBy, opt => opt.MapFrom(src => src.UpdatedBy));
            CreateMap<Skill, SkillDto>().ReverseMap();
            // DTO -> Entity
            CreateMap<SkillDto, VariousSkill>()
                .ForMember(dest => dest.Views, opt => opt.Ignore()); // مهم تتجاهل Views
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        }
    }
}
