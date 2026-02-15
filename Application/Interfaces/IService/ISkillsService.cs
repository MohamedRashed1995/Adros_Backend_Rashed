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
        Task<IReadOnlyList<SkillShowDto>> GetAllAsync(int? take, int? skip);
        Task<SkillShowDto?> GetByIdAsync(Guid id);
        Task<SkillShowDto> CreateAsync(SkillDto dto);
        Task<SkillDto?> UpdateAsync(Guid ID ,SkillDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
