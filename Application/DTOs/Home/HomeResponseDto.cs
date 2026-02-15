using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Calender;
using Adros.Application.DTOs.Stage;
using Adros.Application.DTOs.Level;
using Adros.Core.Entities.Home;
using Adros.Core.Entities.Course;

namespace Adros.Application.DTOs.Home
{
    public class HomeResponseDto
    {
        public IReadOnlyList<ClientBannerDto> Banners { get; set; } = [];
        //public IReadOnlyList<StageEntityDto> Stages { get; set; } = [];
        //public string? level { get; set; }
        public LevelEntityDto? levels { get; set; }    
        //public IReadOnlyList<ClientCalenderDto> Calenders { get; set; } = [];
        public IReadOnlyList<ClientCalenderDto> calenders { get; set; }
        public bool IsSubscribed { get; set; }
        //public IReadOnlyList<SkillDto> Skills { get; set; } = [];
    }
}
