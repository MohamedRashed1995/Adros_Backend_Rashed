using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Teacher;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities;
using Adros.Core.Entities.Users;
using Adros.Core.Specifications;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using System.Drawing.Printing;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

namespace Adros.Application.Services.UsersServices
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TeacherService> logger,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<PaginatedResult<TeacherEntityDto>> GetAllTeachersAsync(int page, int pageSize, bool? isActive, string? sort)
        {
            var spec = new TeacherSpecifications(isActive, sort, page, pageSize);
            var totalTeachers = await _unitOfWork.Repository<Teacher>().GetCountWithSpecAsync(spec);
            var teachers = await _unitOfWork.Repository<Teacher>().ListAsync(spec);

            var teacherDtos = _mapper.Map<IReadOnlyList<TeacherEntityDto>>(teachers);

            var totalPages = (int)Math.Ceiling(totalTeachers / (double)pageSize);

            return new PaginatedResult<TeacherEntityDto>
            {
                Data = teacherDtos,
                Pagination = new PaginationMetadata
                {
<<<<<<< HEAD
                    PageIndex = page,        // بدل Page
                    PageSize = pageSize,
                    Count = totalTeachers,   // بدل TotalCount
                    TotalPages = (int)Math.Ceiling(totalTeachers / (double)pageSize)
                }
            };

        }
        public async Task<string> ChangePasswordAsync(
    Guid teacherId,
    TeacherChangePasswordDto dto)
        {
            var teacher = await _userManager.FindByIdAsync(teacherId.ToString());

            if (teacher == null)
                throw new Exception("Teacher not found");

            var result = await _userManager.ChangePasswordAsync(
                teacher,
                dto.OldPassword,
                dto.NewPassword
            );

            if (!result.Succeeded)
                throw new Exception(string.Join(", ",
                    result.Errors.Select(e => e.Description)));

            return "Password changed successfully";
        }


=======
                    PageIndex = page,
                    PageSize = pageSize,
                    Count = totalTeachers,
                    TotalPages = totalPages
                }
            };
        }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        public async Task<Teacher?> GetTeacherByIdAsync(Guid teacherId)
        {
            var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
            return await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
        }

<<<<<<< HEAD


        public async Task<(IReadOnlyList<TeacherEntityDto> Teachers, int TeacherCount)> GetTeachersByStageAsync(Guid stageId)
        {
            var teachers = await _unitOfWork.Repository<Teacher>().GetAllAsync();

            var filtered = teachers
                .Where(t => t.StageId == stageId && t.IsActive)
                .ToList();

            var teacherDtos = _mapper.Map<IReadOnlyList<TeacherEntityDto>>(filtered);

            var teacherCount = teacherDtos.Count;

            return (teacherDtos, teacherCount);
        }



=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<Teacher> CreateTeacherAsync(TeacherCreateDto dto)
        {
            // التحقق من وجود المستخدم مسبقاً
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new ApplicationException("المستخدم موجود مسبقاً");
<<<<<<< HEAD
                
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
<<<<<<< HEAD
                Photo = dto.Photo != null ? "temp" : null, // سيتم تحديثها بعد الرفع
                LastName = dto.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
=======
                LastName = dto.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            };

            var createUserResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                throw new ApplicationException($"فشل في إنشاء المستخدم: {errors}");
            }

            // رفع الصورة لو موجودة
            if (dto.Photo != null)
            {
                try
                {
<<<<<<< HEAD
                    user.Photo = await FileManager.UploadFileAsync(dto.Photo, "Images/teachers");
=======
                    user.Photo = await FileManager.UploadFileAsync(dto.Photo, "teachers");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    await _userManager.UpdateAsync(user);
                }
                catch
                {
                    // متابعة بدون صورة إذا فشل الرفع
                }
            }

            await _userManager.AddToRoleAsync(user, "Teacher");

            var teacher = new Teacher
            {
                ApplicationUserId = user.Id,
                Email = dto.Email,
<<<<<<< HEAD
                ProfilePictureUrl = user.Photo,
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                About = dto.About ?? "معلم في منصة أدرس",
                CreatedAt = DateTime.UtcNow,
<<<<<<< HEAD
                UpdatedAt = DateTime.UtcNow,
                StageId = dto.StageId,
                phoneNumber = dto.PhoneNumber,
=======
                UpdatedAt = DateTime.UtcNow
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            };

            await _unitOfWork.Repository<Teacher>().AddAsync(teacher);
            await _unitOfWork.CompleteAsync();

            return teacher;
        }

        public async Task<Teacher?> UpdateTeacherAsync(Guid teacherId, TeacherUpdateDto dto)
        {
            var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
            var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
            if (teacher == null) return null;

            var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
            if (user == null) return null;

            if (!string.IsNullOrEmpty(dto.Email))
            {
                user.Email = dto.Email;
                user.UserName = dto.Email;
            }

            if (!string.IsNullOrEmpty(dto.About))
                teacher.About = dto.About;

            if (dto.Photo != null)
                user.Photo = await FileManager.UploadFileAsync(dto.Photo, "teachers");
<<<<<<< HEAD
                teacher.ProfilePictureUrl = user.Photo;
            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                teacher.FirstName = dto.FirstName;
            }
            if (!string.IsNullOrEmpty(dto.LastName))
            {
                teacher.LastName = dto.LastName;
            }
            if (!string.IsNullOrEmpty(dto.StageId.ToString()))
            {
                teacher.StageId = dto.StageId;
            }
            teacher.IsActive = dto.IsActive;
            teacher.phoneNumber = dto.PhoneNumber;
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new ApplicationException("فشل في تحديث المستخدم.");

            _unitOfWork.Repository<Teacher>().Update(teacher);
            await _unitOfWork.CompleteAsync();

            return teacher;
        }

<<<<<<< HEAD
        //public async Task<bool> DeleteTeacherAsync(Guid teacherId)
        //{
        //    var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
        //    var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
        //    if (teacher == null) return false;

        //    var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
        //    if (user == null) return false;

        //    var result = await _userManager.DeleteAsync(user);
        //    if (!result.Succeeded)
        //        throw new ApplicationException("فشل في حذف المستخدم.");

        //    _unitOfWork.Repository<Teacher>().Delete(teacher);
        //    await _unitOfWork.CompleteAsync();

        //    return true;
        //}

        public async Task<bool> DeleteTeacherAsync(Guid teacherId)
        {
            try
            {
                var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
                var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
                if (teacher == null) return false;

                var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
                if (user == null) return false;

                _unitOfWork.Repository<Teacher>().Delete(teacher);
                await _unitOfWork.CompleteAsync();

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher");
                throw; // عشان تشوف السبب الحقيقي
            }
        }


=======
        public async Task<bool> DeleteTeacherAsync(Guid teacherId)
        {
            var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
            var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
            if (teacher == null) return false;

            var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new ApplicationException("فشل في حذف المستخدم.");

            _unitOfWork.Repository<Teacher>().Delete(teacher);
            await _unitOfWork.CompleteAsync();

            return true;
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<bool> ChangeTeacherPasswordAsync(Guid teacherId, string newPassword)
        {
            var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
            var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
            if (teacher == null) return false;

            var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
            if (user == null) return false;

<<<<<<< HEAD
            

=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
                throw new ApplicationException("فشل في تغيير كلمة المرور.");

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateTeacherStatusAsync(Guid teacherId, bool isActive)
        {
            var spec = new TeacherSpecifications(null, null, 1, 1, teacherId);
            var teacher = await _unitOfWork.Repository<Teacher>().GetEntityWithSpec(spec);
            if (teacher == null) return false;

            var user = await _userManager.FindByIdAsync(teacher.ApplicationUserId.ToString());
            if (user == null) return false;

            user.IsActive = isActive;
            user.LockoutEnd = isActive ? null : DateTimeOffset.MaxValue;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new ApplicationException("فشل في تحديث حالة المستخدم.");

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IReadOnlyList<ClientTeacherDto>> GetClientTeachersAsync()
        {
            var spec = new TeacherSpecifications();
            var teachers = await _unitOfWork.Repository<Teacher>().ListAsync(spec);
            return _mapper.Map<IReadOnlyList<ClientTeacherDto>>(teachers);
        }
    }
}
