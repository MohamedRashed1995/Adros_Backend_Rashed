using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Teacher;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Users;
using Adros.Core.Entities;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Adros.Core.Specifications;

namespace Adros.Application.Services.UsersServices
{
    public class TeacherService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<TeacherService> logger,
        UserManager<ApplicationUser> userManager) : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<TeacherService> _logger = logger;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<PaginatedResult<TeacherEntityDto>> GetAllTeachersAsync(int page, int pageSize, bool? isActive, string? sort)
        {
            try
            {
                _logger.LogInformation("Fetching all teachers with page {Page}, pageSize {PageSize}, isActive {IsActive}, sort {Sort}", page, pageSize, isActive, sort);

                var spec = new TeacherSpecifications(isActive, sort, page, pageSize);
                var totalTeachers = await _unitOfWork.Repository<Teacher>().GetCountWithSpecAsync(spec);
                var teachers = await _unitOfWork.Repository<Teacher>().ListAsync(spec);

                var teacherDtos = _mapper.Map<IReadOnlyList<TeacherEntityDto>>(teachers);

                var totalPages = (int)Math.Ceiling(totalTeachers / (double)pageSize);

                var paginatedResult = new PaginatedResult<TeacherEntityDto>
                {
                    Data = teacherDtos,
                    Pagination = new PaginationMetadata
                    {
                        PageIndex = page,
                        PageSize = pageSize,
                        Count = totalTeachers,
                        TotalPages = totalPages
                    }
                };

                return paginatedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all teachers.");
                throw;
            }
        }

        public async Task<Teacher?> GetTeacherByIdAsync(Guid teacherId)
        {
            try
            {
                _logger.LogInformation("Fetching teacher with ID: {TeacherId}", teacherId);

                var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
                var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID: {TeacherId} not found.", teacherId);
                    return null;
                }

                //var teacherDto = _mapper.Map<TeacherEntityDto>(teacher);
                return teacher;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching teacher with ID: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<Teacher> CreateTeacherAsync(TeacherCreateDto teacherCreateDto)
        {
            try
            {
                _logger.LogInformation("🚀 بدء إنشاء معلم جديد: {Email}", teacherCreateDto.Email);

                // 1. التحقق من عدم وجود المستخدم مسبقاً
                var existingUser = await _userManager.FindByEmailAsync(teacherCreateDto.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("❌ المستخدم موجود مسبقاً: {Email}", teacherCreateDto.Email);
                    throw new ApplicationException("المستخدم موجود مسبقاً");
                }

                // 2. إنشاء ApplicationUser أولاً
                var user = new ApplicationUser
                {
                    UserName = teacherCreateDto.Email,
                    
                    FirstName = teacherCreateDto.FirstName,
                    LastName = teacherCreateDto.LastName,
                    Email = teacherCreateDto.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };

                _logger.LogInformation("👤 إنشاء ApplicationUser...");

                // 3. حفظ الـ User أولاً في Identity
                var createUserResult = await _userManager.CreateAsync(user, teacherCreateDto.Password);
                if (!createUserResult.Succeeded)
                {
                    var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                    _logger.LogError("❌ فشل في إنشاء المستخدم: {Errors}", errors);
                    throw new ApplicationException($"فشل في إنشاء المستخدم: {errors}");
                }

                _logger.LogInformation("✅ تم إنشاء ApplicationUser - ID: {UserId}", user.Id);

                // 4. إضافة الصورة (إذا وجدت)
                if (teacherCreateDto.Photo != null)
                {
                    _logger.LogInformation("📸 رفع صورة المعلم...");
                    try
                    {
                        user.Photo = await FileManager.UploadFileAsync(teacherCreateDto.Photo, "teachers");
                        await _userManager.UpdateAsync(user);
                        _logger.LogInformation("✅ تم رفع الصورة: {Photo}", user.Photo);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "⚠️ لم أتمكن من رفع الصورة، المتابعة بدون صورة");
                    }
                }

                // 5. إضافة دور المعلم
                _logger.LogInformation("🎭 إضافة دور Teacher...");
                var roleResult = await _userManager.AddToRoleAsync(user, "Teacher");
                if (!roleResult.Succeeded)
                {
                    _logger.LogWarning("⚠️ لم أتمكن من إضافة الدور، لكن المستخدم تم إنشاؤه");
                }

                // 6. الآن فقط، إنشاء Teacher entity
                var teacher = new Teacher
                {
                    ApplicationUserId = user.Id, // ⬅️ الآن الـ UserId موجود
                    TeacherID = user.Id,
                    Email = teacherCreateDto.Email,
                    FirstName = teacherCreateDto.FirstName,
                    LastName = teacherCreateDto.LastName,
                    About = teacherCreateDto.About ?? "معلم في منصة أدرس",
                    
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _logger.LogInformation("👨‍🏫 إنشاء Teacher entity...");

                // 7. حفظ Teacher في قاعدة البيانات
                await _unitOfWork.Repository<Teacher>().AddAsync(teacher);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("✅ تم إنشاء Teacher - ID: {TeacherId}", teacher.Id);

                // 8. تحديث الـ ApplicationUser بالـ TeacherId
                user.TeacherId = teacher.Id;
                await _userManager.UpdateAsync(user);

                _logger.LogInformation("🎉 تم إنشاء المعلم بنجاح!");
                _logger.LogInformation("   📧 Email: {Email}", teacher.Email);
                _logger.LogInformation("   🆔 Teacher ID: {TeacherId}", teacher.Id);
                _logger.LogInformation("   🆔 User ID: {UserId}", user.Id);

                return teacher;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "💥 فشل في إنشاء المعلم");

                // محاولة التراجع إذا فشلنا
                try
                {
                    await _unitOfWork.RollbackAsync();
                }
                catch { }

                throw new Exception($"فشل في إنشاء المعلم: {ex.Message}", ex);
            }
        }

        public async Task<Teacher?> UpdateTeacherAsync(Guid teacherId, TeacherUpdateDto teacherUpdateDto)
        {
            try
            {
                _logger.LogInformation("Updating teacher with ID: {TeacherId}", teacherId);

                var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
                var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID: {TeacherId} not found.", teacherId);
                    return null;
                }

                var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
                if (user == null)
                {
                    _logger.LogWarning("Associated user for teacher ID: {TeacherId} not found.", teacherId);
                    return null;
                }

                // Update user properties
                if (!string.IsNullOrEmpty(teacherUpdateDto.Email))
                {
                    _logger.LogInformation("Updating email for teacher with ID: {TeacherId}", teacherId);
                    user.Email = teacherUpdateDto.Email;
                    user.UserName = teacherUpdateDto.Email;
                }

                if (!string.IsNullOrEmpty(teacherUpdateDto.About))
                {
                    _logger.LogInformation("Updating about for teacher with ID: {TeacherId}", teacherId);
                    teacher.About = teacherUpdateDto.About;
                }

                if (teacherUpdateDto.Photo != null)
                {
                    _logger.LogInformation("Uploading new photo for teacher with ID: {TeacherId}", teacherId);
                    user.Photo = await FileManager.UploadFileAsync(teacherUpdateDto.Photo, "teachers");
                }

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    _logger.LogWarning("Failed to update teacher with ID: {TeacherId}. Errors: {Errors}", teacherId, string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                    throw new ApplicationException("Failed to update teacher.");
                }

                _unitOfWork.Repository<Teacher>().Update(teacher);
                await _unitOfWork.CompleteAsync();

                //var teacherDto = _mapper.Map<TeacherEntityDto>(teacher);
                //teacherDto.Email = user.Email;
                //teacherDto.Photo = user.Photo;

                //teacherDto.LessonCount = teacher.Lessons.Count;

                _logger.LogInformation("Successfully updated teacher with ID: {TeacherId}", teacherId);

                return teacher;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating teacher with ID: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<bool> DeleteTeacherAsync(Guid teacherId)
        {
            try
            {
                _logger.LogInformation("Deleting teacher with ID: {TeacherId}", teacherId);

                var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
                var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID: {TeacherId} not found.", teacherId);
                    return false;
                }

                var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
                if (user == null)
                {
                    _logger.LogWarning("Associated user for teacher ID: {TeacherId} not found.", teacherId);
                    return false;
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to delete teacher with ID: {TeacherId}. Errors: {Errors}", teacherId, string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new ApplicationException("Failed to delete teacher.");
                }

                _unitOfWork.Repository<Teacher>().Delete(teacher);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully deleted teacher with ID: {TeacherId}", teacherId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting teacher with ID: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<bool> ChangeTeacherPasswordAsync(Guid teacherId, string newPassword)
        {
            try
            {
                _logger.LogInformation("Changing password for teacher with ID: {TeacherId}", teacherId);

                var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
                var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID: {TeacherId} not found.", teacherId);
                    return false;
                }

                var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
                if (user == null)
                {
                    _logger.LogWarning("Associated user for teacher ID: {TeacherId} not found.", teacherId);
                    return false;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to change password for teacher with ID: {TeacherId}. Errors: {Errors}", teacherId, string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new ApplicationException("Failed to change password.");
                }

                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully changed password for teacher with ID: {TeacherId}", teacherId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing password for teacher with ID: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<bool> UpdateTeacherStatusAsync(Guid teacherId, bool isActive)
        {
            try
            {
                _logger.LogInformation("Updating activation status for teacher with ID: {TeacherId} to {IsActive}", teacherId, isActive);

                var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
                var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID: {TeacherId} not found.", teacherId);
                    return false;
                }

                var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
                if (user == null)
                {
                    _logger.LogWarning("Associated user for teacher ID: {TeacherId} not found.", teacherId);
                    return false;
                }

                user.IsActive = isActive;
                if (!isActive)
                {
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                }
                else
                {
                    user.LockoutEnd = null;
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to update activation status for teacher with ID: {TeacherId}. Errors: {Errors}", teacherId, string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new ApplicationException("Failed to update activation status.");
                }

                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully updated activation status for teacher with ID: {TeacherId} to {IsActive}", teacherId, isActive);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating activation status for teacher with ID: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<IReadOnlyList<ClientTeacherDto>> GetClientTeachersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching client-facing teachers.");

                var spec = new TeacherSpecifications();
                var teachers = await _unitOfWork.Repository<Teacher>().ListAsync(spec);

                var clientTeacherDtos = _mapper.Map<IReadOnlyList<ClientTeacherDto>>(teachers);

                _logger.LogInformation("Successfully fetched {Count} client-facing teachers.", clientTeacherDtos.Count);

                return clientTeacherDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching client-facing teachers.");
                throw;
            }
        }
    }
}
