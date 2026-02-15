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
<<<<<<< HEAD
        Task<IReadOnlyList<SkillShowDto>> GetAllAsync(int? take, int? skip);
        Task<SkillShowDto?> GetByIdAsync(Guid id);
        Task<SkillShowDto> CreateAsync(SkillDto dto);
        Task<SkillDto?> UpdateAsync(Guid ID ,SkillDto dto);
=======
        Task<IReadOnlyList<SkillDto>> GetAllAsync(int? take, int? skip);
        Task<SkillDto?> GetByIdAsync(Guid id);
        Task<SkillDto> CreateAsync(SkillDto dto);
        Task<SkillDto?> UpdateAsync(SkillDto dto);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        Task<bool> DeleteAsync(Guid id);
    }
}
