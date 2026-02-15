using Adros.Core.Entities;

namespace Adros.Application.Interfaces.IService
{
    /// <summary>
    /// Provides methods for user-related operations
    /// </summary>
    public interface ISharedUserService
    {
        Task ChangeUserActivationAsync(Guid userId, bool isActive);

        Task<Guid?> GetStudentIdByUserIdAsync(Guid userId);

        Task<ApplicationUser> GetUserBYId(Guid userId);
        Task<string?> GetUserNameById(Guid userId);
    }

}
