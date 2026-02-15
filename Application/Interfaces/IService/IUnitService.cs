//using Adros.Application.DTOs.Topic;
using Adros.Application.DTOs.Topic;

namespace Adros.Application.Interfaces.IService
{
    public interface IUnitService
    {
<<<<<<< HEAD
        Task<IReadOnlyList<UnitEntityDto>> GetAllUnitsAsync();
        Task<UnitEntityDto?> GetUnitByIdAsync(Guid topicId);
        Task<UnitEntityDto> CreateUnitAsync(UnitCreateDto dto);
        Task<UnitEntityDto?> UpdateUnitAsync(Guid topicId, UnitUpdateDto dto);
        Task<bool> DeleteUnitAsync(Guid topicId);
        Task<IReadOnlyList<UnitEntityDto>> GetUnitsBySubjectIdAsync(Guid subjectId);
=======
        Task<IReadOnlyList<UnitEntityDto>> GetAllTopicsAsync();
        Task<UnitEntityDto?> GetTopicByIdAsync(Guid topicId);
        Task<UnitEntityDto> CreateTopicAsync(UnitCreateDto dto);
        Task<UnitEntityDto?> UpdateTopicAsync(Guid topicId, UnitUpdateDto dto);
        Task<bool> DeleteTopicAsync(Guid topicId);
        Task<IReadOnlyList<UnitEntityDto>> GetTopicsBySubjectIdAsync(Guid subjectId);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

    }
}
