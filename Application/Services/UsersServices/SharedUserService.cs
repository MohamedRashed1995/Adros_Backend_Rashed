using Adros.Application.Interfaces.IService;
using Adros.Core.Entities;
<<<<<<< HEAD
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

=======
using Adros.Core.Entities.Users;
using Adros.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Adros.Shared.Interfaces;

namespace Adros.Application.Services.UsersServices
{
    /// <summary>
    /// Service for shared user operations
    /// </summary>
    public class SharedUserService(
        UserManager<ApplicationUser> userManager,
        ILogger<SharedUserService> logger,
        IUnitOfWork unitOfWork
    ) : ISharedUserService
    {

        /// <summary>
        /// Initializes a new instance of <see cref="SharedUserService"/>
        /// </summary>
        /// <param name="userManager">Identity framework user manager</param>
        /// <param name="logger">Logging service</param>
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<SharedUserService> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// Changes the activation status of a user
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <param name="isActive">The activation flag to set</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found</exception>
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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
<<<<<<< HEAD
=======
                // You can handle identity errors here as needed
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update user. Errors: {errors}");
            }
        }

<<<<<<< HEAD
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

=======

        /// <summary>
        /// Retrieves the associated student ID for a given user
        /// </summary>
        /// <param name="userId">User's unique identifier</param>
        /// <returns>The student ID if available, otherwise null</returns>
        /// <exception cref="NotFoundException">Thrown if the user is not found</exception>
        public async Task<Guid?> GetStudentIdByUserIdAsync(Guid userId)
        {
            var student = await _unitOfWork.Repository<Student>()
                .GetEntityWithSpec(new StudentByUserIdSpecification(userId));

            return student?.Id;
        }



>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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

<<<<<<< HEAD
=======
        
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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
<<<<<<< HEAD
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
}
