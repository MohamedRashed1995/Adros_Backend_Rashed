using Adros.Application.DTOs.Level;
using Adros.Application.DTOs.Stage;

namespace Adros.Application.Interfaces.IService
{
    public interface IStageService
    {
        Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync();
        Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto);
        Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto);
        Task<bool> DeleteStageAsync(Guid stageId);
        Task<StageEntityDto?> GetStageByIdAsync(Guid stageId);
    }
}
