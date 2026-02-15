using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;

public class StudentByUserIdSpecification : BaseSpecification<Student>
{
    public StudentByUserIdSpecification(Guid userId)
        : base(s => s.ApplicationUserId == userId)
    {
        AddInclude(s => s.ApplicationUser);
        AddInclude(s => s.Level);
        AddInclude(s => s.Level.Stage);
        AddInclude(s => s.VideoViews);
        AddInclude(s => s.WatchLater);
    }
}
