using Adros.Application.Interfaces.IService;
using Adros.Core.Entities;
using Adros.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.UsersServices
{
    /// <summary>
    /// Service for shared user operations
    /// </summary>
    public class SharedUserService(
        UserManager<ApplicationUser> userManager,
        ILogger<SharedUserService> logger
    ) : ISharedUserService
    {

        /// <summary>
        /// Initializes a new instance of <see cref="SharedUserService"/>
        /// </summary>
        /// <param name="userManager">Identity framework user manager</param>
        /// <param name="logger">Logging service</param>
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<SharedUserService> _logger = logger;


        /// <summary>
        /// Changes the activation status of a user
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <param name="isActive">The activation flag to set</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found</exception>
        public async Task ChangeUserActivationAsync(Guid userId, bool isActive)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                throw new NotFoundException($"User {userId} not found");
            }

            user.IsActive = isActive;
            _logger.LogInformation("Changing user activation. UserId: {UserId}, Active: {IsActive}", userId, isActive);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // You can handle identity errors here as needed
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update user. Errors: {errors}");
            }
        }


        /// <summary>
        /// Retrieves the associated student ID for a given user
        /// </summary>
        /// <param name="userId">User's unique identifier</param>
        /// <returns>The student ID if available, otherwise null</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found</exception>
        public async Task<Guid?> GetStudentIdByUserIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                throw new NotFoundException($"User {userId} not found");
            }

            // StudentId is a Guid? (nullable). Return it directly.
            return user.StudentId;
        }


        public async Task<ApplicationUser> GetUserBYId(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                throw new NotFoundException($"User {userId} not found");
            }
            return user;
        }

        
        public async Task<string?> GetUserNameById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                throw new NotFoundException($"User {userId} not found");
            }
            return user.UserName;
        }
    }

}
