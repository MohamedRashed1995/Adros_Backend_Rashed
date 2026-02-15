using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Level;
using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Student;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Home;
using Adros.Core.Entities.Users;
using Adros.Core.Specifications;
using Adros.Core.Specifications.QueryParams;
using Adros.Shared.Constants;
using Adros.Shared.Exceptions;
using Adros.Shared.Interfaces;
using Adros.Shared.Settings;
using AutoMapper;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using System.Globalization;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
namespace Adros.Application.Services.UsersServices
{
    /// <summary>
    /// Provides student-related business logic and data operations
    /// </summary>
    /// <remarks>
    /// This service handles:
    /// - Paginated student data retrieval
    /// - Query parameter validation
    /// - Database interaction through Unit of Work pattern
    /// - Mapping between entities and DTOs
    /// - Error handling and logging
    /// </remarks>
    public class StudentService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<StudentService> logger,
        ISharedUserService sharedUserService) : IStudentService
    {
        private const string ServiceName = nameof(StudentService);
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<StudentService> _logger = logger;
        private readonly ISharedUserService _sharedUserService = sharedUserService;
        

        /// <summary>
        /// Retrieves paginated list of students with filtering and sorting capabilities
        /// </summary>
        /// <param name="parameters">Query parameters including filters, sorting, and pagination</param>
        /// <returns>Paginated result containing student DTOs and pagination metadata</returns>
        /// <exception cref="ArgumentNullException">Thrown when parameters are null</exception>
        /// <exception cref="ArgumentException">Thrown for invalid pagination parameters</exception>
        /// <exception cref="StudentServiceException">Thrown for service-specific errors</exception>
        /// <remarks>
        /// Typical workflow:
        /// 1. Validate input parameters
        /// 2. Calculate pagination values
        /// 3. Create specification for query
        /// 4. Execute parallel count and data queries
        /// 5. Map results to DTOs
        /// 6. Return paginated response
        /// </remarks>
        public async Task<IReadOnlyList<StudentListDto>> GetAllStudentsAsync()
        {
            //using var scope = _logger.BeginScope("{Service}.{Method}", ServiceName, nameof(GetAllStudentsAsync));

            // Validate input parameters and calculate pagination values.
            //ValidateParameters(parameters);
            //var (pageSize, pageIndex) = CalculatePagination(parameters);

            //_logger.LogInformation("Processing request with parameters: {@Parameters}", parameters);

            try
            {
                //var spec = new StudentSpecifications(parameters, pageIndex, pageSize);
                //var (totalCount, students) = await FetchPaginatedDataAsync(spec);

                //return CreatePaginatedResult(students, totalCount, pageIndex, pageSize);
<<<<<<< HEAD
                var spec = new StudentListSpecification();
                var students = await _unitOfWork.Repository<Student>().ListAsync(spec);
                
                return _mapper.Map<IReadOnlyList<StudentListDto>>(students, opts =>
                {
                    
=======
                var students = await _unitOfWork.Repository<Student>().ListAllAsync();
                
                return _mapper.Map<IReadOnlyList<StudentListDto>>(students, opts =>
                {
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    opts.Items["FolderName"] = FoldersNames.Media.StudentsFolder;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Students.");
                throw;
            }
        }


        /// <summary>
        /// Retrieves a student by ID
        /// </summary>
        /// <param name="studentId">ID of the student</param>
        /// <returns>StudentEntityDto with student info</returns>
        /// <exception cref="NotFoundException">If no student is found</exception>
        /// <exception cref="StudentServiceException">If an error occurs in the service</exception>
        public async Task<StudentEntityDto> GetStudentByIdAsync(Guid studentId)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentId);
            if (student == null)
                return null;

            return _mapper.Map<StudentEntityDto>(student, opts =>
            {
                opts.Items["FolderName"] = "students";
            });
        }


        /// <summary>
        /// Changes the activation status of a student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <param name="isActive">The activation flag to set</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="NotFoundException">Thrown if the student is not found</exception>
        /// <exception cref="StudentServiceException">Thrown if activation fails</exception>
        public async Task ChangeStudentActivationAsync(Guid studentId, bool isActive)
        {
            using var scope = _logger.BeginScope("{Service}.{Method}", ServiceName, nameof(ChangeStudentActivationAsync));
            try
            {
                _logger.LogInformation("Changing student activation. StudentId: {StudentId}, Active: {IsActive}", studentId, isActive);

                var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentId);
                if (student == null)
                {
                    throw new NotFoundException($"Student {studentId} not found");
                }

                // Use the shared service to toggle the user record
                await _sharedUserService.ChangeUserActivationAsync(student.ApplicationUserId, isActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing student activation: {Message}", ex.Message);
                throw new StudentServiceException("An error occurred while changing student activation", ex);
            }
        }

<<<<<<< HEAD
        public async Task<Guid> GetStudentIdByUserIdAsync(Guid userId)
        {
            var student = await _unitOfWork.Repository<Student>()
                .GetFirstOrDefaultAsync(q =>
                    q.Where(s => s.ApplicationUserId == userId));

            //if (student == null)
            //    throw new UnauthorizedAccessException("Student not found");

            return student?.Id ?? Guid.Empty;
        }

=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        public async Task<StudentProfileDto> GetStudentProfileAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation($"جلب ملف الطالب للمستخدم: {userId}");

                // هنا جلب بيانات الطالب من الداتابيز
                var spec = new StudentProfileSpecs(userId);
                var student = await _unitOfWork.Repository<Student>().GetEntityWithSpec(spec);
<<<<<<< HEAD
                var lessonCount = await _unitOfWork
                    .Repository<Lesson>()
                    .CountAsync(l => l.Unit.Subject.LevelId == student.LevelId);




=======
                
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                if (student == null)
                {
                    throw new NotFoundException($"Student not found for user ID: {userId}");
                }

                if (student.ApplicationUser == null)
                {
                    throw new NotFoundException($"User not found for ID: {userId}");
                }

<<<<<<< HEAD

                var videoViews = student.VideoViews ?? new List<VideoView>();
                var watchLater = student.WatchLater ?? new List<WatchLater>();
                

                var viewsCount = videoViews
                    .Select(v => v.VideoId)
                    .Distinct()
                    .Count();

                var watchLaterCount = watchLater
                    .Select(w => w.VideoId)
                    .Distinct()
                    .Count();

                var totalStudyTime = TimeSpan.FromMinutes(
                    videoViews.Sum(v => v.Duration.TotalMinutes)
                );



                var culture = new CultureInfo("ar-EG");

                var today = DateTime.UtcNow.Date;
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                var endOfWeek = startOfWeek.AddDays(7);

                // 1️⃣ نحسب الوقت للأيام اللي فيها مشاهدة
                var studyTimeByDay = videoViews
                    .Where(v =>
                        v.CreatedAt.HasValue &&
                        v.CreatedAt.Value.Date >= startOfWeek &&
                        v.CreatedAt.Value.Date < endOfWeek
                    )
                    .GroupBy(v => v.CreatedAt!.Value.DayOfWeek)
                    .ToDictionary(
                        g => g.Key,
                        g => TimeSpan.FromMinutes(g.Sum(v => v.Duration.TotalMinutes))
                    );

                // 2️⃣ نطلع كل أيام الأسبوع حتى اللي مفيهاش مشاهدة
                var dailyAchievements = Enum.GetValues<DayOfWeek>()
                    .Select(day => new StudentDailyAchievement
                    {
                        Day = culture.DateTimeFormat.GetDayName(day),
                        StudyTime = studyTimeByDay.TryGetValue(day, out var time)
                            ? time
                            : TimeSpan.Zero
                    })
                    .ToList();
                


                var profile = new StudentProfileDto
                {
                    StudentId = (student.Id).ToString(),
=======
                //// إنشاء الـ DTO
                //var profile = new StudentProfileDto
                //{
                //    ImagePath = student.ApplicationUser.Photo ?? string.Empty,
                //    Name = $"{student.ApplicationUser.FirstName} {student.ApplicationUser.LastName}".Trim(),
                //    Level = student.Level?.Title ?? "غير محدد",
                //    LevelId = student.LevelId,
                //    ViewsCount = student.VideoViews?.Count ?? 0,
                //    DownloadsCount = student.VideoDownloads?.Count ?? 0,
                //    TotalStudyTime = TimeSpan.FromHours(120), // مثال
                //    DailyAchievements = new List<StudentDailyAchievement>()
                //};
                var videoViews = student.VideoViews?.Where(v => v.Video != null).ToList() ?? new List<VideoView>();

                // 1. حساب الوقت الكلي
                //var totalStudyTime = TimeSpan.FromMinutes(
                //    videoViews.Sum(v => v.Video.Duration.Hour * 60 + v.Video.Duration.Minute)
                //);
                var totalStudyTime = TimeSpan.FromMinutes(
                    videoViews.Sum(v => v.Duration.TotalMinutes));
                var dailyAchievements = videoViews
                    .GroupBy(v => v.CreatedAt?.Date ?? DateTime.Now.Date) // لو CreatedAt nullable
                    .Select(g => new StudentDailyAchievement
                    {
                        Day = g.Key.ToString("yyyy-MM-dd"),
                        StudyTime = TimeSpan.FromMinutes(g.Sum(v => v.Duration.TotalMinutes))
                    })
                    .ToList();


                // 3. ملء DTO
                var profile = new StudentProfileDto
                {
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    ImagePath = student.ApplicationUser.Photo ?? string.Empty,
                    Name = $"{student.ApplicationUser.FirstName} {student.ApplicationUser.LastName}".Trim(),
                    Level = student.Level?.Title ?? "غير محدد",
                    LevelId = student.LevelId,
<<<<<<< HEAD
                    LessonCount = lessonCount,
                    ViewsCount = viewsCount,
                    Watchlatercount = watchLaterCount,
                    TotalStudyTime = totalStudyTime,
                    DailyAchievements = dailyAchievements,
                    IsSubscribed = student.IsSubscriped
                };


=======
                    ViewsCount = videoViews.Count,
                    DownloadsCount = student.VideoDownloads?.Count ?? 0,
                    TotalStudyTime = totalStudyTime,
                    DailyAchievements = dailyAchievements
                };

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب ملف الطالب");
                throw;
            }
        }

<<<<<<< HEAD
        public async Task<bool> DeleteStudentAsync(Guid studentId)
        {
            try
            {
                // جلب الطالب
                var student = await _unitOfWork.Repository<Student>().GetBYIdAsync(studentId);
                if (student == null)
                    return false;

                // 1️⃣ حذف كل الـ WatchLater المرتبطة
                var watchLaterList = await _unitOfWork.Repository<WatchLater>()
                    .GetAllAsync(q => q.Where(w => w.StudentId == studentId));

                foreach (var item in watchLaterList)
                {
                    _unitOfWork.Repository<WatchLater>().Delete(item);
                }

                // 2️⃣ حذف كل الـ VideoViews المرتبطة
                var videoViews = await _unitOfWork.Repository<VideoView>()
                    .GetAllAsync(q => q.Where(v => v.StudentId == studentId));

                foreach (var view in videoViews)
                {
                    _unitOfWork.Repository<VideoView>().Delete(view);
                }

                // 3️⃣ حذف الطالب نفسه
                _unitOfWork.Repository<Student>().Delete(student);

                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student: {StudentId}", studentId);
                throw;
            }
        }

=======

        //public async Task<Student> GetStudentProfileAsync(Guid userId)
        //{
        //    try
        //    {
        //        var students = await _unitOfWork.Repository<Student>().ListAllAsync();
        //        var student = students.FirstOrDefault(s => s.ApplicationUserId == userId);

        //        if (student == null)
        //        {
        //            // بدل ما يرجع 500، نرجع NotFoundException
        //            throw new NotFoundException($"No student found for user {userId}");
        //        }

        //        return student;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving student profile. UserId: {UserId}", userId);
        //        throw new StudentServiceException(
        //            "An error occurred while retrieving the student profile.", ex);
        //    }
        //}



        /// <summary>
        /// Retrieves a detailed student profile including image, level, view/download counts, and study time
        /// </summary>
        /// <param name="studentId">Unique identifier of the student</param>
        /// <returns>A <see cref="StudentProfileDto"/> containing profile details</returns>
        /// <exception cref="NotFoundException">Thrown when the student is not found</exception>
        /// <exception cref="StudentServiceException">Thrown for service-specific errors</exception>
        //public async Task<Student> GetStudentProfileAsync(Guid userId)
        //{
        //    //using var scope = _logger.BeginScope("{Service}.{Method}", ServiceName, nameof(GetStudentProfileAsync));
        //    try
        //    {
        //        // 1. Get the student's ID from the user
        //        //var studentId = await _sharedUserService.GetStudentIdByUserIdAsync(userId);
        //        //if (!studentId.HasValue)
        //        //{
        //        //    _logger.LogWarning("No student ID found for user. UserId: {UserId}", userId);
        //        //    throw new NotFoundException($"No student found for user {userId}");
        //        //}

        //        // 2. Query for the student, including related data
        //        //var studentSpec = new StudentProfileSpecs(studentId.Value);
        //        var student = await _unitOfWork.Repository<Student>().GetByIdAsync(userId);

        //        if (student is null)
        //        {
        //            _logger.LogWarning("Student not found. StudentId: {StudentId}", userId);
        //            throw new NotFoundException($"Student {userId} not found");
        //        }
        //        //var totalStudyTimeInMinutes = student.VideoViews
        //        //    .Where(vv => vv.Video != null)
        //        //    .Sum(vv => vv.Video.Duration.Hour * 60 + vv.Video.Duration.Minute);
        //        //var profile = new StudentProfileDto
        //        //{ 
        //        //    Name = student.ApplicationUser.UserName,
        //        //    ImagePath = student.ApplicationUser.Photo,
        //        //    ViewsCount = student.VideoViews.Count,
        //        //    DownloadsCount = student.VideoDownloads.Count,
        //        //    TotalStudyTime = TimeSpan.FromMinutes(totalStudyTimeInMinutes)
        //        //};
        //        return profile;
        //        // 3. Compute any custom fields (views, downloads, daily achievements, etc.)
        //        //var viewsCount = student.VideoViews.Count;
        //        //var downloadsCount = student.VideoDownloads.Count;



        //        //var totalStudyTime = TimeSpan.FromMinutes(totalStudyTimeInMinutes);

        //        //var dailyAchievements = student.VideoViews
        //        //    .GroupBy(vv => vv.CreatedAt.Date)
        //        //    .Select(group => new StudentDailyAchievement
        //        //    {
        //        //        Day = group.Key.ToString("yyyy-MM-dd"),
        //        //        StudyTime = TimeSpan.FromMinutes(
        //        //            group
        //        //                .Where(vv => vv.Video != null)
        //        //                .Sum(x => x.Video.Duration.Hour * 60 + x.Video.Duration.Minute)
        //        //        )
        //        //    })
        //        //    .ToList();

        //        //// 4. Build the final DTO. 
        //        //var profileDto = new StudentProfileDto
        //        //{
        //        //    ImagePath = student.ApplicationUser.Photo,
        //        //    Name = student.ApplicationUser?.UserName ?? string.Empty,
        //        //    Level = student.Level?.Title ?? string.Empty,
        //        //    ViewsCount = viewsCount,
        //        //    DownloadsCount = downloadsCount,
        //        //    TotalStudyTime = totalStudyTime,
        //        //    DailyAchievements = dailyAchievements
        //        //};

        //        //return profileDto;

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving student profile. UserId: {UserId}, Error: {Message}", userId, ex.Message);
        //        throw new StudentServiceException("An error occurred while retrieving the student profile.", ex);
        //    }
        //}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a




        #region Helper Methods

        /// <summary>
        /// Validates query parameters for student data retrieval
        /// </summary>
        /// <param name="parameters">Query parameters to validate</param>
        /// <exception cref="ArgumentNullException">Thrown if parameters are null</exception>
        /// <exception cref="ArgumentException">Thrown for negative pagination values</exception>
        private static void ValidateParameters(StudentQueryParameters parameters)
        {
            ArgumentNullException.ThrowIfNull(parameters);

            if (parameters.Skip < 0 || parameters.Take < 0)
                throw new ArgumentException("Pagination parameters must be non-negative");
        }

        /// <summary>
        /// Calculates safe pagination values based on input parameters
        /// </summary>
        /// <param name="parameters">Query parameters containing raw pagination values</param>
        /// <returns>
        /// Tuple containing:
        /// - PageSize: Clamped between 1 and system max page size
        /// - PageIndex: Calculated from skip/take values, minimum 1
        /// </returns>
        /// <remarks>
        /// Uses Math.Clamp to ensure page size stays within system limits
        /// </remarks>
        private static (int PageSize, int PageIndex) CalculatePagination(StudentQueryParameters parameters)
        {
            // Ensure page size is within system limits, defaults to system-defined page size if not specified.
            var pageSize = Math.Clamp(parameters.Take ?? SystemSettings.Pagination.DefaultPageSize, 1, SystemSettings.Pagination.MaxPageSize);

            // Calculate page index, ensuring it is at least 1.
            var pageIndex = Math.Max((parameters.Skip ?? 0) / pageSize + 1, 1);

            return (pageSize, pageIndex);
        }


        /// <summary>
        /// Executes parallel queries for total count and student data
        /// </summary>
        /// <param name="spec">Configured specification for filtering/sorting</param>
        /// <returns>
        /// Tuple containing:
        /// - TotalCount: Total matching records count
        /// - Students: Paginated list of student entities
        /// </returns>
        /// <remarks>
        /// Uses Task.WhenAll for parallel execution of count and data queries
        /// </remarks>
        private async Task<(int TotalCount, IReadOnlyList<Student> Students)> FetchPaginatedDataAsync(StudentSpecifications spec)
        {
            var countTask = _unitOfWork.Repository<Student>().GetCountWithSpecAsync(spec);
            var dataTask = _unitOfWork.Repository<Student>().ListAsync(spec);

            // Wait for both tasks to complete.
            await Task.WhenAll(countTask, dataTask);

            return (await countTask, await dataTask);
        }


        /// <summary>
        /// Constructs final paginated result DTO
        /// </summary>
        /// <param name="students">List of student entities</param>
        /// <param name="totalCount">Total number of matching records</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated result with mapped DTOs and pagination metadata</returns>
        /// <remarks>
        /// Uses AutoMapper to convert entities to DTOs
        /// Calculates total pages using ceiling division
        /// </remarks>
        private PaginatedResult<StudentListDto> CreatePaginatedResult(IReadOnlyList<Student> students, int totalCount, int pageIndex, int pageSize)
        {
            return new PaginatedResult<StudentListDto>
            {
                Data = _mapper.Map<IReadOnlyList<StudentListDto>>(students),
                Pagination = new PaginationMetadata
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                }
            };
        }
        #endregion
    }


}