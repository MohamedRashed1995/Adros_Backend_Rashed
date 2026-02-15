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
<<<<<<< HEAD
        Task<Guid> GetStudentIdByUserIdAsync(Guid applicationUserId);
        Task<bool> DeleteStudentAsync(Guid studentId);
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}

