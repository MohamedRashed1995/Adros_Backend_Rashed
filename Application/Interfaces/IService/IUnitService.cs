//using Adros.Application.DTOs.Topic;
using Adros.Application.DTOs.Topic;

namespace Adros.Application.Interfaces.IService
{
    public interface IUnitService
    {
        Task<IReadOnlyList<UnitEntityDto>> GetAllUnitsAsync();
        Task<UnitEntityDto?> GetUnitByIdAsync(Guid topicId);
        Task<UnitEntityDto> CreateUnitAsync(UnitCreateDto dto);
        Task<UnitEntityDto?> UpdateUnitAsync(Guid topicId, UnitUpdateDto dto);
        Task<bool> DeleteUnitAsync(Guid topicId);
        Task<IReadOnlyList<UnitEntityDto>> GetUnitsBySubjectIdAsync(Guid subjectId);

    }
}
