using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class TeacherSpecifications : BaseSpecification<Teacher>
    {
        public TeacherSpecifications(
            bool? isActive,
            string? sort,
            int page,
            int pageSize,
            Guid? teacherId = null)
            : base(t =>
                !t.Deleted &&
                (!teacherId.HasValue || t.Id == teacherId.Value))
        {
            // INCLUDE (مهم جدًا)
            AddInclude(t => t.ApplicationUser);
            AddInclude(t => t.Lessons);

            // FILTER
            if (isActive.HasValue)
            {
                AddCriteria(t =>
                    t.ApplicationUser != null &&
                    t.ApplicationUser.IsActive == isActive.Value);
            }

            // SORT
            switch (sort?.ToLower())
            {
                case "email":
<<<<<<< HEAD
                    ApplyOrderBy(t => t.Email);
=======
                    ApplyOrderBy(t => t.ApplicationUser!.Email);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    break;

                case "lessoncount":
                    ApplyOrderByDescending(t => t.Lessons.Count);
                    break;

                default:
<<<<<<< HEAD
                    ApplyOrderBy(t => t.FirstName + " " + t.LastName);
=======
                    ApplyOrderBy(t => t.ApplicationUser!.UserName);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    break;
            }

            // PAGINATION
            ApplyPaging((page - 1) * pageSize, pageSize);
        }

        // Client Teachers
        public TeacherSpecifications()
            : base(t =>
                !t.Deleted &&
                t.ApplicationUser != null &&
                t.ApplicationUser.IsActive)
        {
            AddInclude(t => t.ApplicationUser);
            AddInclude(t => t.Lessons);

            ApplyOrderBy(t => t.CreatedAt);
        }
    }
}

