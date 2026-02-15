using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class StudentWithLevelSpec : BaseSpecification<Student>
    {
        public StudentWithLevelSpec(Guid studentId)
            : base(s => s.ApplicationUserId == studentId)
        {
            AddInclude(s => s.Level);
            AddInclude("Level.Stage");
        }
    }

}
