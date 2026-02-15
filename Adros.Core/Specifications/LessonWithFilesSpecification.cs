using Adros.Core.Entities.Course;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class LessonWithFilesSpecification : BaseSpecification<Lesson>
    {
        public LessonWithFilesSpecification()
        {
            AddInclude(l => l.Videos);
            AddInclude(l => l.Attachments);
            AddInclude(l => l.Teacher);
            ApplyOrderBy(l => l.Order);
        }
    }

}
