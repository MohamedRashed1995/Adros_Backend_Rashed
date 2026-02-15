using Adros.Application.DTOs.Lesson;
using Adros.Application.DTOs.Topic;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.UsersServices;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Core.Specifications;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Adros.Application.Services.HomeService
{
    public class LessonsService : ILessonsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonsService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStudentService _studentService;
        public LessonsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<LessonsService> logger,
            ICurrentUserService currentUserService,
            IStudentService studentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
            _studentService = studentService;

        }

        public async Task<List<LessonDto>> GetAllLessonsAsync()
        {
            try
            {
                var spec = new LessonSpecifications(includeDetails: true);
                var lessons = await _unitOfWork.Repository<Lesson>().ListAsync(spec);

                // ================== Student ==================
                Guid studentId = Guid.Empty;
                if (_currentUserService.UserId != Guid.Empty)
                {
                    studentId = await _studentService
                        .GetStudentIdByUserIdAsync(_currentUserService.UserId);
                }

                // ================== All Video IDs ==================
                var videoIds = lessons
                    .SelectMany(l => l.Videos ?? new List<Video>())
                    .Select(v => v.Id)
                    .ToList();

                // ================== WatchLater ==================
                var watchLaterList = new List<WatchLater>();

                if (studentId != Guid.Empty && videoIds.Any())
                {
                    watchLaterList = (await _unitOfWork.Repository<WatchLater>()
                        .GetAllAsync(q =>
                            q.Where(w =>
                                videoIds.Contains(w.VideoId) &&
                                w.StudentId == studentId &&
                                !w.Deleted
                            )
                        )).ToList();
                }
                var isWatchLater = studentId != Guid.Empty &&
                                           watchLaterList.Any(w => w.VideoId == w.Id);
                var result = lessons.Select(lesson => new LessonDto
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Order = lesson.Order,
                    TeacherId = lesson.TeacherId,
                    TeacherName = lesson.Teacher != null
                        ? lesson.Teacher.FirstName + " " + lesson.Teacher.LastName
                        : "Unknown",
                    AboutTeacher = lesson.Teacher.About,
                    UnitId = lesson.UnitId,
                    ExamId = lesson.ExamId,

                    Videos = lesson.Videos?.Select(v => new LessonVideoDto
                    {
                        Id = v.Id,
                        Url = v.Url,
                        VimeoId = v.Url.Contains("vimeo.com") ? ExtractVimeoId(v.Url) : null,
                        VideoName = v.Title,
                        VideoTeacher = lesson.Teacher != null
                            ? lesson.Teacher.FirstName + " " + lesson.Teacher.LastName
                            : "Unknown",
                        Description = v.Description,
                        Duration = v.Duration,
                        // ✅ الإضافة الوحيدة
                        isWatchlater = isWatchLater

                    }).ToList() ?? new List<LessonVideoDto>(),

                    Attachments = lesson.Attachments?.Select(v => new DTOs.Attachment.AttachmentDto
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Url = "https://adros-mrashed.runasp.net/" + v.Url
                    }).ToList() ?? new List<DTOs.Attachment.AttachmentDto>(),

                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء جلب كل الدروس");
                throw;
            }
        }





        public async Task<LessonDto> CreateLessonAsync(LessonCreateDto dto)
        {
            // التحقق من Teacher
            var teacherExists = await _unitOfWork.Repository<Teacher>().GetByIdAsync(dto.TeacherId);
            if (teacherExists == null) throw new Exception($"Teacher {dto.TeacherId} not found.");

            // التحقق من Unit
            var unitExists = await _unitOfWork.Repository<Unit>().GetByIdAsync(dto.UnitId);
            if (unitExists == null) throw new Exception($"Unit {dto.UnitId} not found.");

            // إنشاء الـ Lesson
            var lesson = _mapper.Map<Lesson>(dto);
            lesson.UpdatedAt = null;
            lesson.CreatedAt = DateTime.Now;

            // لو CurrentUser موجود استخدمه، لو لأ خلي Guid جديد
            lesson.CreatedBy = _currentUserService.UserId != Guid.Empty ? _currentUserService.UserId : Guid.NewGuid();
            lesson.UpdatedBy = lesson.CreatedBy;
            //lesson.TeacherId = _currentUserService.UserId;
            await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<LessonDto>(lesson);
        }



        public async Task<IReadOnlyList<LessonDto>> GetLessonsBySubjectIdAsync(Guid subjectId)
        {
            var spec = new LessonSpecifications(subjectId);
            var lessons = await _unitOfWork.Repository<Lesson>().ListAsync(spec);
            return _mapper.Map<IReadOnlyList<LessonDto>>(lessons);
        }

        public async Task<LessonDto?> GetLessonByIdAsync(Guid lessonId)
        {
            var spec = new LessonSpecifications(lessonId, true);
            var lessons = await _unitOfWork.Repository<Lesson>().ListAsync(spec);
            var lesson = lessons.FirstOrDefault();

            if (lesson == null)
                return null;

            var baseUrl = "http://adros-mrashed.runasp.net";

            // ================== Student ==================
            Guid studentId = Guid.Empty;
            if (_currentUserService.UserId != Guid.Empty)
            {
                studentId = await _studentService
                    .GetStudentIdByUserIdAsync(_currentUserService.UserId);
            }

            // ================== Video IDs ==================
            var videoIds = lesson.Videos?
                .Where(v => !v.Deleted)
                .Select(v => v.Id)
                .ToList() ?? new List<Guid>();

            // ================== WatchLater ==================
            var watchLaterList = new List<WatchLater>();

            if (studentId != Guid.Empty && videoIds.Any())
            {
                watchLaterList = (await _unitOfWork.Repository<WatchLater>()
                    .GetAllAsync(q =>
                        q.Where(w =>
                            videoIds.Contains(w.VideoId) &&
                            w.StudentId == studentId &&
                            !w.Deleted
                        )
                    )).ToList();
            }

            // ================== DTO ==================
            var lessonDto = new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Order = lesson.Order,

                TeacherName = lesson.Teacher != null
                    ? $"{lesson.Teacher.FirstName} {lesson.Teacher.LastName}"
                    : "Unknown",

                AboutTeacher = lesson.Teacher?.About ?? "Unknown",

                Videos = lesson.Videos?
                    .Where(v => !v.Deleted)
                    .Select(v =>
                    {
                        var isWatchLater = studentId != Guid.Empty &&
                                           watchLaterList.Any(w => w.VideoId == v.Id);

                        return new LessonVideoDto
                        {
                            Id = v.Id,
                            VideoName = v.Title,
                            VideoTeacher = lesson.Teacher != null
                                ? $"{lesson.Teacher.FirstName} {lesson.Teacher.LastName}"
                                : "Unknown",
                            Description = v.Description,
                            Duration = v.Duration,
                            Url = v.SourceType == VideoSourceType.Upload
                                ? $"{baseUrl}/{v.Url.TrimStart('/')}"
                                : v.Url,
                            VimeoId = v.Url.Contains("vimeo.com")? ExtractVimeoId(v.Url) : null,
                            isWatchlater = isWatchLater
                        };
                    }).ToList() ?? new List<LessonVideoDto>(),

                Attachments = lesson.Attachments?
                    .Select(a => new DTOs.Attachment.AttachmentDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Url = $"{baseUrl}/{a.Url.TrimStart('/')}"
                    }).ToList()
                    ?? new List<DTOs.Attachment.AttachmentDto>()
            };

            return lessonDto;
        }






        public async Task<LessonDto?> UpdateLessonAsync(Guid lessonId, LessonUpdateDto dto)
        {
            var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);
            if (lesson == null) return null;

            

            _mapper.Map(dto, lesson);
            lesson.UpdatedBy = _currentUserService.UserId;

            _unitOfWork.Repository<Lesson>().Update(lesson);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<bool> DeleteLessonAsync(Guid lessonId)
        {
            var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);
            if (lesson == null) return false;

            _unitOfWork.Repository<Lesson>().Delete(lesson);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<UnitWithLessonsDto> GetLessonsByUnitIdAsync(Guid unitId)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetByIdAsync(unitId);
            if (unit == null)
                throw new Exception("Unit not found");

            var spec = new LessonByUnitSpecifications(unitId);
            var lessons = await _unitOfWork.Repository<Lesson>().ListAsync(spec);

            return new UnitWithLessonsDto
            {
                UnitId = unit.Id,
                UnitTitle = unit.Title,
                UnitDescription = unit.Description,

                Lessons = lessons.Select(l => new LessonEntityDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    Order = l.Order
                }).ToList()
            };
        }

        private static string? ExtractVimeoId(string? url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            // https://vimeo.com/1157170696?share=copy
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);

            return segments.LastOrDefault();
        }



    }
}
