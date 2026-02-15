using Adros.Core.Entities.Course;
using Adros.Shared.Specifications;

public class LessonByUnitSpecifications : BaseSpecification<Lesson>
{
    public LessonByUnitSpecifications(Guid unitId)
        : base(l => l.UnitId == unitId)
    {
        AddInclude(l => l.Videos);
        AddInclude(l => l.Attachments);
        AddInclude(l => l.Teacher);
        //AddInclude(l => l.Exam);
        //AddInclude("Exam.Questions.Question"); // لو عايز تجيب الأسئلة مع Assessment
        ApplyOrderBy(l => l.Order);
    }
}
