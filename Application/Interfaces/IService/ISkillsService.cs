using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Skills;
using Adros.Core.Entities.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface ISkillsService
    {
        Task<IReadOnlyList<SkillDto>> GetVariousSkillAsync(int? take , int? skip);
        Task<SkillDto> CreateVariousSkillAsync(SkillDto SkillDto);
        Task<SkillDto?> UpdateVariousSkillAsync( SkillDto VariousSkillUpdateDto);
        Task<bool> DeleteVariousSkillAsync(Guid VariousSkillId);
        Task<SkillDto?> GetVariousSkillByIdAsync(Guid VariousSkillId);
    }
}
