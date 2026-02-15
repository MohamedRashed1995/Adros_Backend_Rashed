using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Subject : BaseEntity
    {
        public string Title { get; set; } = default!;
        public Guid LevelId { get; set; }
        public Level Level { get; set; }
<<<<<<< HEAD
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
=======
        public ICollection<Unit> topics { get; set; } = [];
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        //public ICollection<Lesson> Lessons { get; set; } = [];
    }
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid LevelId { get; set; }
        public int LessonCount { get; set; }
    }

    public class SubjectInputDto
    {
        
        public string Title { get; set; } = string.Empty;
        public Guid LevelId { get; set; }

    }

    public class SubjectSpecParams 
    {
        private const int MaxPageSize = 10;
        public int PageIndex { get; set; } = 1;

        private int pageSize = 5;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public Guid? LevelId { get; set; }
        public string? Sort { get; set; }

        public string? SearchVal { get; set; }
        //private string? searchVal;
        //public string? SearchVal
        //{
        //    get => searchVal;
        //    set => searchVal = value?.ToLower();
        //}
    }

}
