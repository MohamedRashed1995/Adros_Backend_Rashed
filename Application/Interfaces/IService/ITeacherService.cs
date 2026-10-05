using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Teacher;
using Adros.Core.Entities.Users;

namespace Adros.Application.Interfaces.IService
{
    public interface ITeacherService
    {
        Task<PaginatedResult<TeacherEntityDto>> GetAllTeachersAsync(int page, int pageSize, bool? isActive, string? sort);
        Task<Teacher?> GetTeacherByIdAsync(Guid teacherId);
        Task<Teacher> CreateTeacherAsync(TeacherCreateDto teacherCreateDto);
        Task<Teacher?> UpdateTeacherAsync(Guid teacherId, TeacherUpdateDto teacherUpdateDto);
        Task<bool> DeleteTeacherAsync(Guid teacherId);
        Task<bool> ChangeTeacherPasswordAsync(Guid teacherId, string newPassword);
        Task<bool> UpdateTeacherStatusAsync(Guid teacherId, bool isActive);
        Task<IReadOnlyList<ClientTeacherDto>> GetClientTeachersAsync();
    }
}
