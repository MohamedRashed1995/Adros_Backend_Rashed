using Adros.Core.Entities.Course;
using Adros.Core.Specifications;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    public class LessonSpecifications : BaseSpecification<Lesson>
    {
        // جلب كل الدروس ل subject معين
        public LessonSpecifications(Guid unitId)
        : base(l => l.UnitId == unitId)
        {
<<<<<<< HEAD
            AddInclude(l => l.Videos);
=======
            //AddInclude(l => l.Videos);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            AddInclude(l => l.Attachments);
            AddInclude(l => l.Teacher);
            AddInclude(l => l.Unit);
            ApplyOrderBy(l => l.Order);
        }

        // جلب درس واحد بالتفاصيل
        public LessonSpecifications(Guid lessonId, bool includeDetails)
    : base(l => l.Id == lessonId)
        {
            if (includeDetails)
            {
                AddInclude(l => l.Attachments);
                AddInclude(l => l.Unit);
                AddInclude(l => l.Videos); // الفيديوهات الخاصة بالدرس
                AddInclude(l => l.Teacher);
               
            }
        }
<<<<<<< HEAD
        public LessonSpecifications(bool includeDetails)
: base(l => true) // كل الدروس
        {
            if (includeDetails)
            {
                AddInclude(l => l.Videos);
                AddInclude(l => l.Teacher);
                AddInclude(l => l.Unit);
                AddInclude(l => l.Attachments);
            }
            ApplyOrderBy(l => l.Order);
        }

=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

    }
}
