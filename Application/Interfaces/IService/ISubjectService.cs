
using Adros.Core.Entities.Course;
using Adros.Shared.Helpers;

namespace Adros.Application.Interfaces.IService
{
    public interface ISubjectService
    {
        Task<Pagination<SubjectDto>> GetSubjectsAsync(SubjectSpecParams subjectSpecParams);
        Task<SubjectDto> GetSubjectByIdAsync(Guid id);
        Task<SubjectDto> CreateSubjectAsync(SubjectInputDto dto);
        Task UpdateSubjectAsync(Guid id, SubjectInputDto dto);
        Task DeleteSubjectAsync(Guid id);
        Task<IReadOnlyList<SubjectDto>> GetSubjectsByLevelIdAsync(Guid levelId);
    }
}
