using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    /// <summary>
    /// Specification to query a student by ID
    /// </summary>
    public class StudentByIdSpecification : BaseSpecification<Student>
    {
        public StudentByIdSpecification(Guid studentId) : base(s => s.Id == studentId && !s.Deleted)
        {
            AddCriteria(s => s.Id == studentId);

            Includes.Add(s => s.ApplicationUser);
            Includes.Add(s => s.Level);
            Includes.Add(s => s.Level.Stage);
            Includes.Add(s => s.WatchLater);
            Includes.Add(s => s.VideoViews);
        }
    }

}
