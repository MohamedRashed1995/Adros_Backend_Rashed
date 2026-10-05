using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Calender;
using Adros.Application.DTOs.Stage;
using Adros.Application.DTOs.Level;

namespace Adros.Application.DTOs.Home
{
    public class HomeResponseDto
    {
        public IReadOnlyList<ClientBannerDto> Banners { get; set; } = [];
        public IReadOnlyList<StageEntityDto> Stages { get; set; } = [];
        public IReadOnlyList<ClientCalenderDto> Calenders { get; set; } = [];
        //public IReadOnlyList<SkillDto> Skills { get; set; } = [];
    }
}
