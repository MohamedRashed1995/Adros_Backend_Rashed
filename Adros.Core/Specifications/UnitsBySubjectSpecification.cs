using Adros.Core.Entities.Course;
using Adros.Shared.Specifications;

public class UnitsBySubjectSpecification : BaseSpecification<Unit>
{
    public UnitsBySubjectSpecification(Guid subjectId)
        : base(t => t.SubjectId == subjectId)
    {
        
        // AddInclude(t => t.Lessons);
    }
}
