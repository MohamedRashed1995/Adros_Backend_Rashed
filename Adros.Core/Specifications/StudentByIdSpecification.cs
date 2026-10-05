using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    /// <summary>
    /// Specification to query a student by ID
    /// </summary>
    public class StudentByIdSpecification : BaseSpecification<Student>
    {
        public StudentByIdSpecification(Guid studentId)
        {
            AddCriteria(s => s.Id == studentId);

            Includes.Add(s => s.ApplicationUser);
            Includes.Add(s => s.Level);
            Includes.Add(s => s.Level.Stage);
            Includes.Add(s => s.VideoDownloads);
            Includes.Add(s => s.VideoViews);
        }
    }

}
