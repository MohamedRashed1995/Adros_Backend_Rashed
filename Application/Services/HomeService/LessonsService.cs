using Adros.Application.DTOs.Lesson;
using Adros.Application.DTOs.Topic;
using Adros.Application.Interfaces.IService;
<<<<<<< HEAD
using Adros.Application.Services.UsersServices;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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
<<<<<<< HEAD
using static System.Net.WebRequestMethods;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

namespace Adros.Application.Services.HomeService
{
    public class LessonsService : ILessonsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonsService> _logger;
        private readonly ICurrentUserService _currentUserService;
<<<<<<< HEAD
        private readonly IStudentService _studentService;
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public LessonsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<LessonsService> logger,
<<<<<<< HEAD
            ICurrentUserService currentUserService,
            IStudentService studentService)
=======
            ICurrentUserService currentUserService)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
<<<<<<< HEAD
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





=======
        }

        public async Task<IReadOnlyList<LessonDto>> GetAllLessonsAsync()
        {
            var lessons = await _unitOfWork.Repository<Lesson>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<LessonDto>>(lessons);
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<LessonDto> CreateLessonAsync(LessonCreateDto dto)
        {
            // التحقق من Teacher
            var teacherExists = await _unitOfWork.Repository<Teacher>().GetByIdAsync(dto.TeacherId);
            if (teacherExists == null) throw new Exception($"Teacher {dto.TeacherId} not found.");

<<<<<<< HEAD
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
=======
            // التحقق من Subject
            var TopicExists = await _unitOfWork.Repository<Unit>().GetByIdAsync(dto.UnitId);
            if (TopicExists == null) throw new Exception($"Topic {dto.UnitId} not found.");

            // رفع الملف إذا موجود
            string? uploadedFileName = null;
            if (dto.Lessonfile != null)
            {
                uploadedFileName = await FileManager.UploadFileAsync(dto.Lessonfile, "Lessons");
            }

            // التحقق من Current User
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty) throw new Exception("Current user not found.");

            // إنشاء الـ Lesson
            var lesson = _mapper.Map<Lesson>(dto);
            lesson.LessonfileName = uploadedFileName;
            lesson.CreatedBy = userId;
            lesson.UpdatedBy = userId;

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<LessonDto>(lesson);
        }

<<<<<<< HEAD


=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<IReadOnlyList<LessonDto>> GetLessonsBySubjectIdAsync(Guid subjectId)
        {
            var spec = new LessonSpecifications(subjectId);
            var lessons = await _unitOfWork.Repository<Lesson>().ListAsync(spec);
            return _mapper.Map<IReadOnlyList<LessonDto>>(lessons);
        }

        public async Task<LessonDto?> GetLessonByIdAsync(Guid lessonId)
        {
<<<<<<< HEAD
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






=======
            var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);
            if (lesson == null) return null;
            return _mapper.Map<LessonDto>(lesson);
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<LessonDto?> UpdateLessonAsync(Guid lessonId, LessonUpdateDto dto)
        {
            var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);
            if (lesson == null) return null;

<<<<<<< HEAD
            
=======
            if (dto.Lessonfile != null)
            {
                var fileName = await FileManager.UploadFileAsync(dto.Lessonfile, "Lessons");
                lesson.LessonfileName = fileName;
            }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

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
<<<<<<< HEAD
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

=======
        public async Task<List<LessonDto>> GetLessonsByTopicIdAsync(Guid topicId)
        {
            var spec = new LessonByUnitSpecifications(topicId);
            var lessons = await _unitOfWork.Repository<Lesson>().ListAsync(spec);

            return lessons.Select(l => new LessonDto
            {
                Id = l.Id,
                LessonfileName = l.LessonfileName,
                Order = l.Order,
                Title = l.Title,
                Description = l.Description,
                TeacherId = l.TeacherId,
                TeacherName = $"{l.Teacher.FirstName} {l.Teacher.LastName}",
                UnitId = l.UnitId,
                ExamId = l.ExamId,
                //ExamTitle = l.Exam?.Title,
                Attachments = l.Attachments.Select(a => a.Url).ToList(),
                Videos = l.Videos.Select(v => v.Url).ToList()

            }).ToList();
        }
        
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a


    }
}
