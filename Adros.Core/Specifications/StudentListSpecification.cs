using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class StudentListSpecification : BaseSpecification<Student>
    {
        public StudentListSpecification()
        {
            AddInclude(s => s.VideoViews);
            AddInclude(s => s.WatchLater);
            AddInclude(s => s.Level);
            AddInclude(s => s.Level.Stage);
        }
    }

}
