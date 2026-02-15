using Adros.Application.DTOs.Video;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.CoursesSevices;
using Adros.Application.Services.UsersServices;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Core.Enums;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Data.Entity;



namespace Adros.Application.Services.CourseServices
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VideoService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStudentService _studentService;
        private readonly IVimeoService _vimeoService;
        public VideoService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<VideoService> logger,
            ICurrentUserService currentUserService,
            IStudentService studentService,
            IVimeoService vimeoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
            _studentService = studentService;
            _vimeoService = vimeoService;
        }



        public async Task<IEnumerable<VideoDto>> GetAllVideosAsync()
        {
            try
            {
                var baseUrl = "http://adros-mrashed.runasp.net";

                // جلب البيانات من الـ database
                var videos = await _unitOfWork.Repository<Video>().GetAllAsync();
                var lessons = await _unitOfWork.Repository<Lesson>().ListAllAsync();
                var teachers = await _unitOfWork.Repository<Teacher>().ListAllAsync();
                var units = await _unitOfWork.Repository<Unit>().ListAllAsync();

                var videoDtos = videos.Select(v =>
                {
                    var lesson = lessons.FirstOrDefault(l => l.Id == v.LessonId);
                    var teacher = lesson != null ? teachers.FirstOrDefault(t => t.Id == lesson.TeacherId) : null;
                    var unit = units.FirstOrDefault(u => u.Id == v.UnitId);

                    // تحديد الـ URL النهائي للفيديو
                    string finalUrl = string.Empty;
                    if (!string.IsNullOrEmpty(v.Url))
                    {
                        finalUrl = (v.BunnyVideoId != null || v.SourceType == VideoSourceType.Upload)
                            ? baseUrl + "/" + v.Url.TrimStart('/')
                            : v.Url;
                    }

                    // تحديد الـ thumbnail النهائي
                    //string? thumbnailUrl = null;
                    //if (!string.IsNullOrEmpty(v.ThumbnailUrl))
                    //{
                    //    if (v.SourceType == VideoSourceType.Upload)
                    //        thumbnailUrl = baseUrl + "/" + v.ThumbnailUrl.TrimStart('/');
                    //    else
                    //        thumbnailUrl = v.ThumbnailUrl;
                    //}

                    return new VideoDto
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Description = v.Description,
                        Url = finalUrl,
                        Order = v.Order,
                        LessonId = v.LessonId,
                        UnitId = v.UnitId,
                        Status = v.Status,
                        CreatedAt = v.CreatedAt,
                        ProcessedAt = v.ProcessedAt,
                        Duration = v.Duration,
                        //ViewsCount = v.Views?.Count ?? 0,
                        WatchLaterCount = v.Watchlater?.Count ?? 0,
                        LessonTitle = lesson?.Title,
                        UnitTitle = unit?.Title,
                        TeacherName = teacher != null ? teacher.FirstName + " " + teacher.LastName : null,
                        TeacherAbout = teacher?.About,
                        
                    };
                }).ToList();

                return videoDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all videos");
                throw new Exception("GetAllVideosAsync failed: " + ex.Message, ex);

            }
        }



        //public async Task<VideoDto> GetVideoByIdAsync(Guid id)
        //{
        //    try
        //    {
        //        var video = await _unitOfWork.Repository<Video>().GetBYIdAsync(id);
        //        if (video == null || video.Deleted)
        //            throw new Exception("Video not found");

        //        var lesson = await _unitOfWork.Repository<Lesson>().GetBYIdAsync(video.LessonId);
        //        Teacher? teacher = null;
        //        if (lesson != null && lesson.TeacherId != Guid.Empty)
        //            teacher = await _unitOfWork.Repository<Teacher>().GetBYIdAsync(lesson.TeacherId);
        //        var unit = await _unitOfWork.Repository<Unit>().GetBYIdAsync(video.UnitId);

        //        var baseUrl = "http://adros-mrashed.runasp.net";

        //        string finalUrl = video.BunnyVideoId != null || video.SourceType == VideoSourceType.Upload
        //            ? $"{baseUrl}/{video.Url.TrimStart('/')}"
        //            : video.Url;


        //        return new VideoDto
        //        {
        //            Id = video.Id,
        //            Title = video.Title,
        //            Url = video.Url,
        //            Order = video.Order,
        //            Description = video.Description,
        //            LessonId = video.LessonId,
        //            UnitId = video.UnitId,
        //            Status = video.Status,
        //            CreatedAt = video.CreatedAt,
        //            ProcessedAt = video.ProcessedAt,
        //            //Duration = video.Duration,
        //            //ViewsCount = video.Views?.Count ?? 0,
        //            WatchLaterCount = video.Watchlater?.Count ?? 0,
        //            LessonTitle = lesson?.Title,
        //            UnitTitle = unit?.Title,
        //            TeacherName = teacher != null ? teacher.FirstName + " " + teacher.LastName : null,
        //            TeacherAbout = teacher?.About,
        //            //ThumbnailUrl = thumbnailUrl
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting video by ID: {Id}", id);
        //        throw;
        //    }
        //}

        public async Task<VideoDto> GetVideoByIdAsync(Guid id)
        {
            // جلب الفيديو مع Watchlater collection
            var video = await _unitOfWork.Repository<Video>()
                .GetAsync(
                    v => v.Id == id && !v.Deleted,
                    q => q.Include(v => v.Watchlater)
                );

            if (video == null)
                throw new Exception("Video not found");

            // جلب الدرس والوحدة
            var lesson = await _unitOfWork.Repository<Lesson>().GetBYIdAsync(video.LessonId);
            var unit = await _unitOfWork.Repository<Unit>().GetBYIdAsync(video.UnitId);

            // جلب المدرس لو موجود
            Teacher? teacher = null;
            if (lesson != null && lesson.TeacherId != Guid.Empty)
            {
                teacher = await _unitOfWork.Repository<Teacher>().GetBYIdAsync(lesson.TeacherId);
            }

            // تكوين final URL للفيديو
            var baseUrl = "http://adros-mrashed.runasp.net";
            var finalUrl = video.SourceType == VideoSourceType.Upload || video.BunnyVideoId != null
                ? $"{baseUrl}/{video.Url.TrimStart('/')}"
                : video.Url;

            // ================== WatchLater logic ==================
            bool isWatchLater = false;
            int watchLaterCount = 0;

            if (_currentUserService.UserId != Guid.Empty)
            {
                var studentId = await _studentService.GetStudentIdByUserIdAsync(_currentUserService.UserId);

                if (studentId != Guid.Empty)
                {
                    // جلب WatchLater مباشرة من DB
                    var watchLaterList = await _unitOfWork.Repository<WatchLater>()
                        .GetAllAsync(
                            filter: q => q.Where(w => w.VideoId == video.Id && !w.Deleted)
                        );

                    watchLaterCount = watchLaterList.Count;
                    isWatchLater = watchLaterList.Any(w => w.StudentId == studentId);
                }
            }

            // ================== إنشاء DTO ==================
            return new VideoDto
            {
                Id = video.Id,
                Title = video.Title,
                Url = finalUrl,
                Description = video.Description,
                Order = video.Order,
                LessonId = video.LessonId,
                UnitId = video.UnitId,
                Status = video.Status,
                CreatedAt = video.CreatedAt,
                ProcessedAt = video.ProcessedAt,
                Duration = video.Duration,
                LessonTitle = lesson?.Title,
                UnitTitle = unit?.Title,
                
                TeacherName = teacher != null
                    ? $"{teacher.FirstName} {teacher.LastName}"
                    : null,
                TeacherAbout = teacher?.About,

                WatchLaterCount = watchLaterCount,
                IsWatchLater = isWatchLater
            };
        }








        //public async Task<IEnumerable<VideoDto>> GetVideosByLessonIdAsync(Guid lessonId)
        //{
        //    try
        //    {
        //        var baseUrl = "http://adros-mrashed.runasp.net";
        //        var videos = await _unitOfWork.Repository<Video>().GetAllAsync();
        //        var lessonVideos = videos.Where(v => v.LessonId == lessonId).ToList();

        //        var lessons = await _unitOfWork.Repository<Lesson>().ListAllAsync();
        //        var teachers = await _unitOfWork.Repository<Teacher>().ListAllAsync();
        //        var units = await _unitOfWork.Repository<Unit>().ListAllAsync();

        //        var videoDtos = lessonVideos.Select(v =>
        //        {
        //            var lesson = lessons.FirstOrDefault(l => l.Id == v.LessonId);
        //            var teacher = lesson != null ? teachers.FirstOrDefault(t => t.Id == lesson.TeacherId) : null;
        //            var unit = units.FirstOrDefault(u => u.Id == v.UnitId);

        //            string finalUrl = v.BunnyVideoId != null || v.SourceType == VideoSourceType.Upload
        //                ? $"{baseUrl}/{v.Url.TrimStart('/')}"
        //                : v.Url;

        //            return new VideoDto
        //            {
        //                Id = v.Id,
        //                Title = v.Title,
        //                Url = v.Url,
        //                Order = v.Order,
        //                LessonId = v.LessonId,
        //                UnitId = v.UnitId,
        //                Status = v.Status,
        //                CreatedAt = v.CreatedAt,
        //                ProcessedAt = v.ProcessedAt,

        //                WatchLaterCount = v.Watchlater?.Count ?? 0,
        //                LessonTitle = lesson?.Title,
        //                UnitTitle = unit?.Title,
        //                TeacherName = teacher != null ? teacher.FirstName + " " + teacher.LastName : null,
        //                TeacherAbout = teacher?.About,

        //            };
        //        }).ToList();

        //        return videoDtos;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting videos by lesson ID: {LessonId}", lessonId);
        //        throw;
        //    }
        //}
        public async Task<IEnumerable<VideoDto>> GetVideosByLessonIdAsync(Guid lessonId)
{
    var baseUrl = "http://adros-mrashed.runasp.net";

    // ================== جلب الفيديوهات ==================
    var videos = await _unitOfWork.Repository<Video>()
        .GetAllAsync(
            filter: q => q.Where(v => v.LessonId == lessonId && !v.Deleted)
        );

    if (!videos.Any())
        return Enumerable.Empty<VideoDto>();

    // ================== IDs ==================
    var videoIds = videos.Select(v => v.Id).ToList();

    // ================== Student ==================
    Guid studentId = Guid.Empty;
    if (_currentUserService.UserId != Guid.Empty)
    {
        studentId = await _studentService.GetStudentIdByUserIdAsync(_currentUserService.UserId);
    }

    // ================== WatchLater (مرة واحدة) ==================
    var watchLaterList = new List<WatchLater>();

    if (studentId != Guid.Empty)
    {
        watchLaterList = (await _unitOfWork.Repository<WatchLater>()
            .GetAllAsync(
                filter: q => q.Where(w =>
                    videoIds.Contains(w.VideoId) &&
                    !w.Deleted
                )
            )).ToList();
    }

    // ================== بيانات مساعدة ==================
    var lessons = await _unitOfWork.Repository<Lesson>().ListAllAsync();
    var teachers = await _unitOfWork.Repository<Teacher>().ListAllAsync();
    var units = await _unitOfWork.Repository<Unit>().ListAllAsync();

    // ================== DTO ==================
    var videoDtos = videos.Select(v =>
    {
        var lesson = lessons.FirstOrDefault(l => l.Id == v.LessonId);
        var teacher = lesson != null
            ? teachers.FirstOrDefault(t => t.Id == lesson.TeacherId)
            : null;

        var unit = units.FirstOrDefault(u => u.Id == v.UnitId);

        var finalUrl = v.SourceType == VideoSourceType.Upload || v.BunnyVideoId != null
            ? $"{baseUrl}/{v.Url.TrimStart('/')}"
            : v.Url;

        var videoWatchLater = watchLaterList.Where(w => w.VideoId == v.Id).ToList();

        return new VideoDto
        {
            Id = v.Id,
            Title = v.Title,
            Description = v.Description,
            Url = finalUrl,
            Order = v.Order,
            LessonId = v.LessonId,
            LessonTitle = lesson?.Title,
            UnitId = v.UnitId,
            UnitTitle = unit?.Title,
            Status = v.Status,
            CreatedAt = v.CreatedAt,
            ProcessedAt = v.ProcessedAt,

            TeacherName = teacher != null
                ? $"{teacher.FirstName} {teacher.LastName}"
                : null,
            TeacherAbout = teacher?.About,
            Duration = v.Duration,
            WatchLaterCount = videoWatchLater.Count,
            IsWatchLater = studentId != Guid.Empty &&
                           videoWatchLater.Any(w => w.StudentId == studentId)
        };
    }).ToList();

    return videoDtos;
}





        public async Task<int> GetVideoViewsCountForCurrentStudentAsync(Guid videoId)
        {
            var studentId = _currentUserService.UserId;
            var StudentId = await _studentService.GetStudentIdByUserIdAsync(studentId);
            return await _unitOfWork.Repository<VideoView>()
                .CountAsync(v => v.VideoId == videoId && v.StudentId == StudentId);
        }




        public async Task<int> GetVideoViewsCountAsync(Guid videoId)
        {
            return await _unitOfWork.Repository<VideoView>()
                .CountAsync(v => v.VideoId == videoId);
        }


        public async Task RecordVideoViewAsync(Guid videoId, Guid studentId, int watchedSeconds)
        {
            if (watchedSeconds <= 0) return;

            var today = DateTime.UtcNow.Date;

            var existingView = await _unitOfWork.Repository<VideoView>()
                .GetFirstOrDefaultAsync(
                    filter: query => query.Where(v =>
                        v.VideoId == videoId &&
                        v.StudentId == studentId &&
                        v.CreatedAt.HasValue &&
                        v.CreatedAt.Value.Date == today
                    )
                );

            if (existingView != null)
            {
                // نحسب الفرق الحقيقي من آخر مرة
                int increment = watchedSeconds - existingView.LastReportedSecond;

                if (increment > 0)
                {
                    var totalSeconds = (int)existingView.Duration.TotalSeconds + increment;

                    existingView.Duration = TimeSpan.FromSeconds(totalSeconds);
                    existingView.LastReportedSecond = watchedSeconds;

                    _unitOfWork.Repository<VideoView>().Update(existingView);
                    await _unitOfWork.CompleteAsync();
                }

                return;
            }

            // أول مرة في اليوم
            var newView = new VideoView
            {
                VideoId = videoId,
                StudentId = studentId,
                Duration = TimeSpan.FromSeconds(watchedSeconds),
                LastReportedSecond = watchedSeconds,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<VideoView>().AddAsync(newView);
            await _unitOfWork.CompleteAsync();
        }










        public async Task<VideoDto> CreateVideoAsync(CreateVideoDto dto, VideoSourceType sourceType)
        {
            int? duration = null;

            try
            {
                // ================== Vimeo ==================
                if (sourceType == VideoSourceType.ExternalLink &&
                    dto.Url.Contains("vimeo.com"))
                {
                    var videoId = ExtractVimeoVideoId(dto.Url);

                    try
                    {
                        // استدعاء Vimeo service مع حماية ضد الأخطاء و 429
                        duration = await _vimeoService.GetDurationAsync(videoId);
                    }
                    catch (HttpRequestException httpEx)
                    {
                        _logger.LogWarning(httpEx, "Vimeo request failed for video {VideoUrl}", dto.Url);
                        duration = 0; // قيمة افتراضية بدل التعطل
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error fetching Vimeo duration for video {VideoUrl}", dto.Url);
                        duration = 0;
                    }
                }

                // ================== إنشاء الفيديو ==================
                var video = new Video
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Description = dto.Description,
                    Duration = duration,
                    Url = dto.Url,
                    Order = dto.Order,
                    LessonId = dto.LessonId,
                    UnitId = dto.UnitId,
                    SourceType = sourceType,
                    Status = VideoStatus.Ready,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUserService.UserId,
                    ProcessedAt = DateTime.UtcNow
                };

                await _unitOfWork.Repository<Video>().AddAsync(video);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<VideoDto>(video);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating video {@Dto}", dto);
                throw; // تقدر تعدل هنا لو حابب ترجع null بدل exception
            }
        }






        public async Task<VideoDto> UpdateVideoAsync(Guid id, UpdateVideoDto dto)
        {
            try
            {
                var video = await _unitOfWork.Repository<Video>().GetBYIdAsync(id);
                if (video == null)
                    throw new Exception($"Video with ID {id} not found");

                _mapper.Map(dto, video);
                video.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Video>().Update(video);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<VideoDto>(video);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating video: {Id}, {@Dto}", id, dto);
                throw;
            }
        }

        public async Task<bool> DeleteVideoAsync(Guid id)
        {
            try
            {
                var video = await _unitOfWork.Repository<Video>().GetBYIdAsync(id);
                if (video == null)
                    return false;

                video.Deleted = true;
                _unitOfWork.Repository<Video>().Delete(video);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting video: {Id}", id);
                throw;
            }
        }
        public async Task<bool> ToggleWatchLaterAsync(Guid videoId, Guid studentId)
        {


            if (studentId == Guid.Empty)
                throw new Exception("Invalid student");


            var existing = await _unitOfWork.Repository<WatchLater>()
                .GetFirstOrDefaultAsync(
                    filter: q => q.Where(w => w.VideoId == videoId && w.StudentId == studentId)
                );

            var videoExists = await _unitOfWork.Repository<Video>()
                .GetByIdAsync(videoId);

            if (videoExists == null)
                throw new Exception("Video not found");


            if (existing != null)
            {
                _unitOfWork.Repository<WatchLater>().Delete(existing);
                await _unitOfWork.CompleteAsync();
                return false; // إلغاء Watch Later
            }

            var watchLater = new WatchLater
            {
                Id = Guid.NewGuid(),
                VideoId = videoId,
                StudentId = studentId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = studentId,
            };

            await _unitOfWork.Repository<WatchLater>().AddAsync(watchLater);
            await _unitOfWork.CompleteAsync();

            return true; // إضافة Watch Later
        }

        public async Task<List<WatchLaterVideoDto>> GetWatchLaterVideosAsync(Guid studentId)
        {
            var watchLaterList = await _unitOfWork.Repository<WatchLater>()
                .GetAllWithIncludeAsync(
                    w => w.StudentId == studentId,
                    w => w.Video
                );

            // فلترة العناصر اللي فيها Video null
            var filtered = watchLaterList
                .Where(w => w.Video != null)
                .Select(w => new WatchLaterVideoDto
                {
                    WatchLaterId = w.Id,
                    IsWatchLater = true,
                    VideoId = w.VideoId,
                    Title = w.Video?.Title ?? "No Title",
                    Description = w.Video?.Description ?? ""
                })
                .ToList();

            return filtered;
        }






        public async Task<bool> ChangeVideoStatusAsync(Guid id, VideoStatus status)
        {
            try
            {
                var video = await _unitOfWork.Repository<Video>().GetBYIdAsync(id);
                if (video == null)
                    return false;

                video.Status = status;
                video.ProcessedAt = status == VideoStatus.Ready ? DateTime.UtcNow : null;

                _unitOfWork.Repository<Video>().Update(video);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing video status: {Id}, Status: {Status}", id, status);
                throw;
            }
        }
        private string ExtractVimeoVideoId(string url)
        {
            var uri = new Uri(url);
            return uri.Segments.Last().Trim('/');
        }

    }
}