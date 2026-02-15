using Adros.Application.DTOs.Level;
using Adros.Application.DTOs.Stage;

namespace Adros.Application.Interfaces.IService
{
    public interface ILevelService
    {
        Task<IReadOnlyList<LevelEntityDto>> GetLevelsAsync();
        Task<IReadOnlyList<ClientLevelDto>> GetLevelsForClientByStageIdAsync(Guid stageId);
        Task<IReadOnlyList<LevelEntityDto>> GetLevelsByStageIdForAdminAsync(Guid stageId);
        Task<LevelEntityDto> CreateLevelAsync(LevelCreateDto levelCreateDto);
        Task<LevelEntityDto?> UpdateLevelAsync(Guid levelId, LevelUpdateDto levelUpdateDto);
        Task<bool> DeleteLevelAsync(Guid levelId);
        Task<LevelEntityDto?> GetLevelByIdAsync(Guid levelId);
    }
}
