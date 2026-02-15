using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;

public class StudentByUserIdSpecification : BaseSpecification<Student>
{
    public StudentByUserIdSpecification(Guid userId)
<<<<<<< HEAD
        : base(s => s.ApplicationUserId == userId)
    {
        AddInclude(s => s.ApplicationUser);
        AddInclude(s => s.Level);
        AddInclude(s => s.Level.Stage);
        AddInclude(s => s.VideoViews);
        AddInclude(s => s.WatchLater);
=======
        : base(s => s.ApplicationUserId == userId && !s.Deleted)
    {
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
