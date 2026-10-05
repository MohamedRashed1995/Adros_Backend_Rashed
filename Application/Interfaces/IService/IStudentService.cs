using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Student;
using Adros.Core.Entities.Users;
using Adros.Core.Specifications.QueryParams;

namespace Adros.Application.Interfaces.IService
{
    public interface IStudentService
    {
        Task<IReadOnlyList<StudentListDto>> GetAllStudentsAsync();
        Task<StudentEntityDto> GetStudentByIdAsync(Guid studentId);
        Task ChangeStudentActivationAsync(Guid studentId, bool isActive);
        Task<StudentProfileDto> GetStudentProfileAsync(Guid userId);
    }
}

