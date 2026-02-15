using Adros.Application.Interfaces.IService;
using Adros.Core.Entities;
using Adros.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.UsersServices
{
    public class SharedUserService : ISharedUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SharedUserService> _logger;

        public SharedUserService(UserManager<ApplicationUser> userManager, ILogger<SharedUserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

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
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update user. Errors: {errors}");
            }
        }

        public async Task<Guid?> GetStudentIdByUserIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                throw new NotFoundException($"User {userId} not found");
            }

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
